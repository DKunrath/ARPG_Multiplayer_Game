using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DK
{
    [CreateAssetMenu(menuName = "Character Actions/Weapon Actions/Spell Attack Action")]
    public class SpellAttackWeaponItemAction : WeaponItemAction
    {
        // Main Light Attack 01 is the first type of attack that we have in our main hand (right hand)
        [SerializeField] string spell_Attack_01 = "Main_Spell_Attack_01";
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

            PerformSpellAttack(playerPerformingAction, weaponPerformingAction);
        }

        private void PerformSpellAttack(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            if (playerPerformingAction.playerNetworkManager.isUsingRightHand.Value)
            {
                playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(AttackType.SpellAttack01, spell_Attack_01, true);
            }
            if (playerPerformingAction.playerNetworkManager.isUsingLeftHand.Value)
            {

            }
        }
    }
}