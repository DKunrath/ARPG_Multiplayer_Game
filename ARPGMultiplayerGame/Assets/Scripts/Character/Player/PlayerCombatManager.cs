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

        public void PerformWeaponBasedAction(WeaponItemAction weaponAction, WeaponItem weaponPerformingAction)
        {
            if (playerManager.IsOwner)
            {
                // Perform the action locally
                weaponAction.AttemptToPerformAction(playerManager, weaponPerformingAction);

                // Notify the server that an action is being performed, and perform the action on other clients
                playerManager.playerNetworkManager.NotifyTheServerOfWeaponActionServerRpc(NetworkManager.Singleton.LocalClientId, weaponAction.actionID, weaponPerformingAction.itemID);
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

            playerManager.playerNetworkManager.currentSoulPower.Value -= Mathf.RoundToInt(soulPower);
        }
    }
}