using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DK
{
    [CreateAssetMenu(menuName = "Character Effects/Instant Effects/Take Damage")]
    public class TakeDamageEffect : InstantCharacterEffect
    {
        [Header("Character Causing Damage")]
        public CharacterManager characterCausingDamage; // If the damage is caused by another characters attack it will be stored here

        [Header("Damage")]
        public float physicalDamage = 0; // (In the future will besplit into "Standard", "Strike", "Slash" and "Pierce")
        public float magicalDamage = 0;
        public float fireDamage = 0;
        public float iceDamage = 0;
        public float lightningDamage = 0;
        public float earthDamage = 0;
        public float poisonDamage = 0;
        public float holyDamage = 0;
        public float darkMagicDamage = 0;

        [Header("Final Damage")]
        private float finalDamageDealt = 0; // The damage the character takes after ALL calculations have been made

        [Header("Poise")]
        public float poiseDamage = 0;
        public bool poiseIsBroken = false; // If a characters poise is broken, they will be "Stunned" and play a damage animation

        // 
        // (TO DO) Build Ups
        // Build up effect amounts

        [Header("Animation")]
        public bool playDamageAnimation = true;
        public bool manuallySelectDamageAnimation = false;
        public string damageAnimation;

        [Header("Sound FX")]
        public bool willPlayDamageSFX = true;
        public AudioClip elementalDamageSoundFX; // Used on top of regular SFX if there is elemental damage present (Magical/Fire/Ice/Lightning/Earth/Poison/Holy/DarkMagic)

        [Header("Direction Damage Taken From")]
        public float angleHitFrom; // Used to determine what damage animation to play (Move backwards, to the left, to the right, etc)
        public Vector3 contactPoint; // Used ti determine where the blood FX instantiate

        public override void ProcessEffect(CharacterManager characterManager)
        {
            base.ProcessEffect(characterManager);

            // If the character is dead, no additional damage effects should be processed
            if (characterManager.isDead.Value) return;

            // Check for "Invulnerability"

            // Calculate Damage
            CalculateDamage(characterManager);
            // Check which direction the damage came from
            // Play a damage animation
            // Check for build ups (poison, bleed, etc)
            // Play damage sound FX
            // Play damage VFX (Blood)

            // If character is A.I, check for new target if character causing damage is present
        }

        private void CalculateDamage(CharacterManager characterManager)
        {
            if (!characterManager.IsOwner) return;

            if (characterCausingDamage != null)
            { 
                // Check for damage modifiers and modify base damage (Physical/Elemental damage buff)
                // (physical *= physicalModifier, etc)
            }

            // Check character for flat defenses and subtract them from the damage

            // Check character items for elemental defenses, and subtract from the type especified

            // Check character for armor absorptions, and subtract the percentage from the damage

            // Add all damage types togheter, and apply final damage
            finalDamageDealt = physicalDamage + magicalDamage + fireDamage + iceDamage + lightningDamage + earthDamage + poisonDamage + holyDamage + darkMagicDamage;

            if (finalDamageDealt <= 0)
            {
                // Maybe turn the damageDealt equals to 0 or 1, need to think about it
                finalDamageDealt = 1;
            }

            Debug.Log("Final damage dealt = " + finalDamageDealt);
            characterManager.characterNetworkManager.currentHealth.Value -= finalDamageDealt;
            PlayerUIManager.Instance.playerUIHUDManager.SetNewHealthValue(0, Mathf.RoundToInt(characterManager.characterNetworkManager.currentHealth.Value));

            // Calculate poise damage to determine if the character will be stunned
        }
    }
}