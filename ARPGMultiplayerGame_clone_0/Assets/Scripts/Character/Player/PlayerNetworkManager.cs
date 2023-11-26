using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;

namespace DK
{
    public class PlayerNetworkManager : CharacterNetworkManager
    {
        PlayerManager playerManager;

        public NetworkVariable<FixedString64Bytes> characterName = new NetworkVariable<FixedString64Bytes>("Character", NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        [Header("Equipment")]
        public NetworkVariable<int> currentWeaponBeingUsed = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<int> currentRightHandWeaponID = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<int> currentLeftHandWeaponID = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<bool> isUsingRightHand = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<bool> isUsingLeftHand = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);


        protected override void Awake()
        {
            base.Awake();

            playerManager = GetComponent<PlayerManager>();
        }

        public void SetCharacterActionHand(bool rightHandedAction)
        {
            if (rightHandedAction)
            {
                isUsingLeftHand.Value = false;
                isUsingRightHand.Value = true;
            }
            else
            {
                isUsingRightHand.Value = false;
                isUsingLeftHand.Value = true;
            }
        }

        public void SetNewMaxHealthValue(int oldVitalityValue, int newVitalityValue)
        {
            maxHealth.Value = playerManager.playerStatsManager.CalculateHealthBasedOnVitalityLevel(newVitalityValue);
            //currentHealth.Value = maxHealth.Value;
        }

        public void SetNewMaxManaValue(int oldIntelligenceValue, int newIntelligenceValue)
        {
            maxMana.Value = playerManager.playerStatsManager.CalculateManaBasedOnIntelligenceLevel(newIntelligenceValue);
            //currentMana.Value = maxMana.Value;
        }

        public void OnCurrentRightHandWeaponIDChange(int oldID, int newID)
        {
            WeaponItem newWeapon = Instantiate(WorldItemDatabase.Instance.GetWeaponByID(newID));
            playerManager.playerInventoryManager.currentRightHandWeapon = newWeapon;
            playerManager.playerEquipmentManager.LoadRightWeapon();
        }

        public void OnCurrentLeftHandWeaponIDChange(int oldID, int newID)
        {
            WeaponItem newWeapon = Instantiate(WorldItemDatabase.Instance.GetWeaponByID(newID));
            playerManager.playerInventoryManager.currentLeftHandWeapon = newWeapon;
            playerManager.playerEquipmentManager.LoadLeftWeapon();
        }

        public void OnCurrentWeaponBeingUsedIDChange(int oldID, int newID)
        {
            WeaponItem newWeapon = Instantiate(WorldItemDatabase.Instance.GetWeaponByID(newID));
            playerManager.playerCombatManager.currentWeaponBeingUsed = newWeapon;
        }

        // ITEM ACTIONS
        [ServerRpc]
        public void NotifyTheServerOfWeaponActionServerRpc(ulong clientID, int actionID, int weaponID)
        {
            if (IsServer)
            {
                NotifyTheServerOfWeaponActionClientRpc(clientID, actionID, weaponID);
            }
        }

        [ClientRpc]
        private void NotifyTheServerOfWeaponActionClientRpc(ulong clientID, int actionID, int weaponID)
        {
            // We do not play the action again for the character who called it, because they already played it locally
            if (clientID != NetworkManager.Singleton.LocalClientId)
            {
                PerformWeaponBasedAction(actionID, weaponID);
            }
        }

        private void PerformWeaponBasedAction(int actionID, int weaponID)
        { 
            WeaponItemAction weaponAction = WorldActionManager.Instance.GetWeaponItemActionByID(actionID);

            if (weaponAction != null)
            {
                weaponAction.AttemptToPerformAction(playerManager, WorldItemDatabase.Instance.GetWeaponByID(weaponID));
            }
            else
            {
                Debug.LogError("ACTION IS NULL, CANNOT BE PERFORMED");
            }
        }
    }
}