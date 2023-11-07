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
        // Light Attack Modifier
        // Heavy Attack Modifier
        // Critical Damage Modifier etc

        // Item based actions (RB, RT, LB, LT)

        // Ash of War

        // Blocking Sounds
    }
}