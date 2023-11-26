using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DK
{
    [CreateAssetMenu(menuName = "Character Actions/Weapon Actions/Light Attack Action")]
    public class LightAttackWeaponItemAction : WeaponItemAction
    {
        // Main Light Attack 01 is the first type of attack that we have in our main hand (right hand)
        [SerializeField] string light_Attack_01 = "Main_Light_Attack_01";
        public override void AttemptToPerformAction(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            base.AttemptToPerformAction(playerPerformingAction, weaponPerformingAction);

            // Check for stops

            if (!playerPerformingAction.IsOwner) return;

            // If the player is dead, he cannot perform an light attack
            if (playerPerformingAction.isDead.Value) return;

            // If the player doesnt have mana, he cannot cast any spell - COLOCAR APENAS NA ACAO DE CASTAR SPELL
            //if (playerPerformingAction.playerNetworkManager.currentMana.Value <= 0) return;

            // If the player isnt in the ground, he cannot perform an light attack
            if (!playerPerformingAction.isGrounded) return;

            PerformLightAttack(playerPerformingAction, weaponPerformingAction);
        }

        private void PerformLightAttack(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            if (playerPerformingAction.playerNetworkManager.isUsingRightHand.Value)
            {
                playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(AttackType.LightAttack01 , light_Attack_01, true);
            }
            if (playerPerformingAction.playerNetworkManager.isUsingLeftHand.Value)
            {

            }
        }
    }
}