using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace DK
{
    public class CharacterNetworkManager : NetworkBehaviour
    {
        CharacterManager characterManager;

        [Header("Position")]
        public NetworkVariable<Vector3> networkPosition = new NetworkVariable<Vector3>(Vector3.zero, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public Vector3 networkPositionVelocity;
        public float networkPositionSmoothTime = 0.1f;

        [Header("Rotation")]
        public NetworkVariable<Quaternion> networkRotation = new NetworkVariable<Quaternion>(Quaternion.identity, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public float networkRotationSmoothTime = 0.1f;

        [Header("Animator")]
        public NetworkVariable<float> horizontalMovement = new NetworkVariable<float>(0f, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<float> verticalMovement = new NetworkVariable<float>(0f, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<float> networkMoveAmount = new NetworkVariable<float>(0f, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        [Header("Target")]
        public NetworkVariable<ulong> currentTargetNetworkObjectID = new NetworkVariable<ulong>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        [Header("Flags")]
        public NetworkVariable<bool> isLockedOn = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<bool> isSprinting = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<bool> isJumping = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        [Header("Stats")]
        public NetworkVariable<int> vitality = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<int> intelligence = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        [Header("Resources")]
        public NetworkVariable<float> currentHealth = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<float> currentSoulPower = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<float> maxHealth = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<float> maxSoulPower = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        [Header("Soul Power Regeneration Stats")]
        public NetworkVariable<float> soulPowerForCharacterToRegenerate = new NetworkVariable<float>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public float soulPowerRegenerationTimerDelay = 1f; // Tempo que deve ser esperado antes de regenerar uma porcentagem da mana
        public float timeToWaitBeforeRegeneration = 0f;

        protected virtual void Awake()
        { 
            characterManager = GetComponent<CharacterManager>();
        }

        public void CheckHP(float oldValue, float newValue)
        {
            if (currentHealth.Value <= 0) 
            {
                StartCoroutine(characterManager.ProcessDeathEvent());
            }

            // Prevents us from over healing
            if (characterManager.IsOwner)
            {
                if (currentHealth.Value > maxHealth.Value)
                { 
                    currentHealth.Value = maxHealth.Value;
                }
            }
        }

        public void OnLockOnTargetIDChange(ulong oldID, ulong newID)
        {
            if (!IsOwner)
            {
                characterManager.characterCombatManager.currentTarget = NetworkManager.Singleton.SpawnManager.SpawnedObjects[newID].gameObject.GetComponent<CharacterManager>();
            }
        }

        public void OnIsLockedOnChanged(bool old, bool isLockedOn)
        {
            if (!isLockedOn)
            {
                characterManager.characterCombatManager.currentTarget = null;
            }
        }

        #region NOTIFY SERVER OF ACTION ANIMATION

        // A server rpc is a function called from a client, to the server (in our case, the host)
        [ServerRpc]
        public void NotifyTheServerOfActionAnimationServerRpc(ulong clientID, string animationID, bool applyRootMotion)
        {
            // If this character is the host/server, then activate the client rpc
            if (IsServer)
            {
                PlayActionAnimationForAllClientesClientRpc(clientID, animationID, applyRootMotion);
            }
        }

        // A client rpc is sent to all clients present, from the server 
        [ClientRpc]
        public void PlayActionAnimationForAllClientesClientRpc(ulong clientID, string animationID, bool applyRootMotion)
        {
            // We make sure to not run the function on the character who sent it (so we dont play the animation twice)
            if (clientID != NetworkManager.Singleton.LocalClientId)
            {
                PerformActionAnimationFromServer(animationID, applyRootMotion);
            }
        }

        private void PerformActionAnimationFromServer(string animationID, bool applyRootMotion)
        {
            characterManager.applyRootMotion = applyRootMotion;
            characterManager.animator.CrossFade(animationID, 0.2f);
        }

        #endregion

        #region NOTIFY SERVER OF ATTACK ANIMATION

        // The ServerRpc for the attack action animation
        [ServerRpc]
        public void NotifyTheServerOfAttackActionAnimationServerRpc(ulong clientID, string animationID, bool applyRootMotion)
        {
            // If this character is the host/server, then activate the client rpc
            if (IsServer)
            {
                PlayAttackActionAnimationForAllClientesClientRpc(clientID, animationID, applyRootMotion);
            }
        }

        // A client rpc is sent to all clients present, from the server 
        [ClientRpc]
        public void PlayAttackActionAnimationForAllClientesClientRpc(ulong clientID, string animationID, bool applyRootMotion)
        {
            // We make sure to not run the function on the character who sent it (so we dont play the animation twice)
            if (clientID != NetworkManager.Singleton.LocalClientId)
            {
                PerformAttackActionAnimationFromServer(animationID, applyRootMotion);
            }
        }

        private void PerformAttackActionAnimationFromServer(string animationID, bool applyRootMotion)
        {
            characterManager.applyRootMotion = applyRootMotion;
            characterManager.animator.CrossFade(animationID, 0.2f);
        }

        #endregion

        #region NOTIFY SERVER OF CHARACTER DAMAGE

        [ServerRpc(RequireOwnership = false)]
        public void NotifyTheServerOfCharacterDamageServerRpc(
            ulong damagedCharacterID,
            ulong characterCausingDamageID,
            float physicalDamage,
            float magicalDamage,
            float fireDamage,
            float iceDamage,
            float lightningDamage,
            float earthDamage,
            float poisonDamage,
            float holyDamage, 
            float darkMagicDamage,
            float poiseDamage,
            float angleHitFrom,
            float contactPointX,
            float contactPointY,
            float contactPointZ)
        {
            if (IsServer)
            {
                NotifyTheServerOfCharacterDamageClientRpc(
                    damagedCharacterID,
                    characterCausingDamageID,
                    physicalDamage,
                    magicalDamage,
                    fireDamage,
                    iceDamage,
                    lightningDamage,
                    earthDamage,
                    poisonDamage,
                    holyDamage,
                    darkMagicDamage,
                    poiseDamage,
                    angleHitFrom,
                    contactPointX,
                    contactPointY,
                    contactPointZ);
            }
        }

        [ClientRpc]
        public void NotifyTheServerOfCharacterDamageClientRpc(
            ulong damagedCharacterID,
            ulong characterCausingDamageID,
            float physicalDamage,
            float magicalDamage,
            float fireDamage,
            float iceDamage,
            float lightningDamage,
            float earthDamage,
            float poisonDamage,
            float holyDamage,
            float darkMagicDamage,
            float poiseDamage,
            float angleHitFrom,
            float contactPointX,
            float contactPointY,
            float contactPointZ)
        {
            ProcessCharacterDamageFromServer(
                    damagedCharacterID,
                    characterCausingDamageID,
                    physicalDamage,
                    magicalDamage,
                    fireDamage,
                    iceDamage,
                    lightningDamage,
                    earthDamage,
                    poisonDamage,
                    holyDamage,
                    darkMagicDamage,
                    poiseDamage,
                    angleHitFrom,
                    contactPointX,
                    contactPointY,
                    contactPointZ);
        }

        public void ProcessCharacterDamageFromServer(
            ulong damagedCharacterID,
            ulong characterCausingDamageID,
            float physicalDamage,
            float magicalDamage,
            float fireDamage,
            float iceDamage,
            float lightningDamage,
            float earthDamage,
            float poisonDamage,
            float holyDamage,
            float darkMagicDamage,
            float poiseDamage,
            float angleHitFrom,
            float contactPointX,
            float contactPointY,
            float contactPointZ)
        {
            CharacterManager damagedCharacter = NetworkManager.Singleton.SpawnManager.SpawnedObjects[damagedCharacterID].gameObject.GetComponent<CharacterManager>();
            CharacterManager characterCausingDamage = NetworkManager.Singleton.SpawnManager.SpawnedObjects[characterCausingDamageID].gameObject.GetComponent<CharacterManager>();

            TakeDamageEffect damageEffect = Instantiate(WorldCharacterEffectsManager.Instance.takeDamageEffect);
            damageEffect.physicalDamage = physicalDamage;
            damageEffect.magicalDamage = magicalDamage;
            damageEffect.fireDamage = fireDamage;
            damageEffect.iceDamage = iceDamage;
            damageEffect.lightningDamage = lightningDamage;
            damageEffect.earthDamage = earthDamage;
            damageEffect.poisonDamage = poisonDamage;
            damageEffect.holyDamage = holyDamage;
            damageEffect.darkMagicDamage = darkMagicDamage;
            damageEffect.poiseDamage = poiseDamage;
            damageEffect.angleHitFrom = angleHitFrom;
            damageEffect.contactPoint = new Vector3(contactPointX, contactPointY, contactPointZ);
            damageEffect.characterCausingDamage = characterCausingDamage;

            damagedCharacter.characterEffectsManager.ProcessInstantEffect(damageEffect);
        }

        #endregion
    }
}