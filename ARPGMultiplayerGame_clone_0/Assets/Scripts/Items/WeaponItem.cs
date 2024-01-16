using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DK
{
    public class WeaponItem : Item
    {
        // Animator controller override (Change attack animations based on the weapon you are currently using)

        [Header("Weapon Model")]
        public GameObject weaponModel;

        [Header("Weapon Requirements")]
        public int strenghtREQ = 0;
        public int intelligenceREQ = 0;
        public int dexterityREQ = 0;
        public int wisdomREQ = 0;

        [Header("Weapon Base Damage")]
        public int physicalDamage = 0;
        public int magicalDamage = 0;
        public int fireDamage = 0;
        public int iceDamage = 0;
        public int lightningDamage = 0;
        public int earthDamage = 0;
        public int poisonDamage = 0;
        public int holyDamage = 0;
        public int darkMagicDamage = 0;

        // Weapon Guard Absorptions (Blocking Power)

        [Header("Weapon Base Poise Damage")]
        public float poiseDamage = 10;
        // Offensive poise bonus when attacking

        // Weapon Modifiers
        [Header("Attack Modifiers")]
        public float light_Attack_01_Modifier = 1.1f;
        public float spell_Attack_01_Modifier = 1.1f;
        public float heavy_Attack_01_Modifier = 1.5f;
        // Heavy Attack Modifier
        // Critical Damage Modifier etc

        [Header("Light Attack Cost Modifiers")]
        public int baseLightAttackCost = 1;
        public float lightAttackSoulPowerCostMultiplier = 0.9f;

        [Header("Soul Power Cost Modifiers")]
        public int baseSoulPowerCost = 1;
        public float spellAttackSoulPowerCostMultiplier = 0.9f;

        // Item based actions (RB, RT, LB, LT) in Console
        // Item based actions (RightMouseButton (RMB), ALT+RMB, LeftMouseButton (LMB), ALT+LMB) in keyboard & mouse
        // Normal Attack: RB on Console or LMB on Mouse&Keyboard
        // Strong Attack: RT on Console or ALT+LMB on Mouse&Keyboard
        // Guard: LB on Console or RMB on Mouse&Keyboard
        // Skill: LT on Console or ALT+RMB on Mouse&Keyboard
        [Header("Actions")]
        public WeaponItemAction oh_RB_and_LMB_Action; // One hand Right bumper action and One hand LeftMouseButton
        public WeaponItemAction oh_RT_and_RMB_Action; // One hand Right trigger action   

        // Ash of War

        // Blocking Sounds
    }
}