using System.Collections;
using System.Collections.Generic;
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

            if (characterManager.IsOwner)
            { 
                characterManager.characterNetworkManager.currentMana.Value -= manaDamage;
            }
        }
    }
}