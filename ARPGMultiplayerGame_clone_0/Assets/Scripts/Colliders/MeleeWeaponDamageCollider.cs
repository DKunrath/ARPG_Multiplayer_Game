using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DK
{
    public class MeleeWeaponDamageCollider : DamageCollider
    {
        [Header("Attacking Character")]
        public CharacterManager characterCausingDamage; // When calculating damage this is used to check for attackers modifiers, effects, etc

        [Header("Weapon Attack Modifiers")]
        public float light_Attack_01_Modifier;
        public float spell_Attack_01_Modifier;
    }
}