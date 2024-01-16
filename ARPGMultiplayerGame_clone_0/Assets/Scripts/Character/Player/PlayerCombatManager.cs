using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace DK
{
    public class PlayerCombatManager : CharacterCombatManager
    {
        PlayerManager playerManager;

        public WeaponItem currentWeaponBeingUsed;

        protected override void Awake()
        {
            base.Awake();

            playerManager = GetComponent<PlayerManager>();
        }

        private void Update()
        {
            if (playerManager.IsOwner)
            {
                RegenerateSoulPowerBasedOnItemsEquipped();
            }
        }

        public void PerformWeaponBasedAction(WeaponItemAction weaponAction, WeaponItem weaponPerformingAction)
        {
            if (playerManager.IsOwner)
            {
                if ((weaponPerformingAction.baseSoulPowerCost * weaponPerformingAction.lightAttackSoulPowerCostMultiplier) > playerManager.playerNetworkManager.currentSoulPower.Value)
                {
                    Debug.Log("You dont have soul power enough to do that!");
                    return;
                }
                else
                {
                    // Perform the action locally
                    weaponAction.AttemptToPerformAction(playerManager, weaponPerformingAction);

                    // Notify the server that an action is being performed, and perform the action on other clients
                    playerManager.playerNetworkManager.NotifyTheServerOfWeaponActionServerRpc(NetworkManager.Singleton.LocalClientId, weaponAction.actionID, weaponPerformingAction.itemID);
                }
            }
        }

        public void DrainSoulPowerBasedOnAttack()
        {
            if (!playerManager.IsOwner) return;

            if (currentWeaponBeingUsed == null) return;

            float soulPower = 0;

            switch (currentAttackType)
            {
                case AttackType.LightAttack01:
                    soulPower = currentWeaponBeingUsed.baseSoulPowerCost * currentWeaponBeingUsed.lightAttackSoulPowerCostMultiplier;
                    break;
                case AttackType.SpellAttack01:
                    soulPower = currentWeaponBeingUsed.baseSoulPowerCost * currentWeaponBeingUsed.spellAttackSoulPowerCostMultiplier;
                    break;
                default:
                    break;
            }

            if (playerManager.playerNetworkManager.currentSoulPower.Value < soulPower)
            {
                Debug.Log("You dont have soul power enough to do that!");
                return;
            }

            playerManager.playerNetworkManager.currentSoulPower.Value -= Mathf.RoundToInt(soulPower);
            PlayerUIManager.Instance.playerUIHUDManager.SetNewSoulPowerValue(0, Mathf.RoundToInt(playerManager.playerNetworkManager.currentSoulPower.Value));
        }

        public void RegenerateSoulPowerBasedOnItemsEquipped()
        {
            if (!playerManager.IsOwner) return;

            // Prevents us from regenerating soul power when its already full
            if (playerManager.playerNetworkManager.currentSoulPower.Value == playerManager.playerNetworkManager.maxSoulPower.Value) return;

            playerManager.playerNetworkManager.timeToWaitBeforeRegeneration += Time.deltaTime;

            if (playerManager.playerNetworkManager.currentSoulPower.Value < playerManager.playerNetworkManager.maxSoulPower.Value && 
                playerManager.playerNetworkManager.timeToWaitBeforeRegeneration > playerManager.playerNetworkManager.soulPowerRegenerationTimerDelay)
            {
                playerManager.playerNetworkManager.currentSoulPower.Value += playerManager.playerNetworkManager.soulPowerForCharacterToRegenerate.Value;
                
                PlayerUIManager.Instance.playerUIHUDManager.SetNewSoulPowerValue(0, Mathf.RoundToInt(playerManager.playerNetworkManager.currentSoulPower.Value));

                if (playerManager.playerNetworkManager.currentSoulPower.Value > playerManager.playerNetworkManager.maxSoulPower.Value)
                {
                    playerManager.playerNetworkManager.currentSoulPower.Value = playerManager.playerNetworkManager.maxSoulPower.Value;
                }

                playerManager.playerNetworkManager.timeToWaitBeforeRegeneration = 0;
            }
        }

        public override void SetTarget(CharacterManager newTarget)
        {
            base.SetTarget(newTarget);

            if (playerManager.IsOwner)
            {
                PlayerCamera.Instance.SetLockCameraHeight();
            }
        }
    }
}