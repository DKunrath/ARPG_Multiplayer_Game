using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace DK
{
    [CreateAssetMenu(menuName = "Character Effects/Instant Effects/Take Mana Damage")]
    public class TakeManaDamageEffect : InstantCharacterEffect
    {
        public float manaDamage;

        public override void ProcessEffect(CharacterManager characterManager)
        {
            CalculateManaDamage(characterManager);
        }

        private void CalculateManaDamage(CharacterManager characterManager)
        {
            // Comparaed the base mana damage against other player effects/modifiers
            // Change the value before subtracting/adding it
            // Play sound FX or VFX during effect

            // Only owners can edit their network variable
            if (!characterManager.IsOwner) return;

            if (characterManager.isPerformingAction) return;

            if (characterManager.characterNetworkManager.currentMana.Value <= 0)
            {
                characterManager.characterNetworkManager.currentMana.Value = 0;
            }

            if (characterManager.characterNetworkManager.currentMana.Value == 0) return;

            if (manaDamage > characterManager.characterNetworkManager.currentMana.Value) return;

            PlayerUIManager.Instance.playerUIHUDManager.RemoveMana(manaDamage);
            characterManager.characterNetworkManager.currentMana.Value -= manaDamage;
        }
    }
}