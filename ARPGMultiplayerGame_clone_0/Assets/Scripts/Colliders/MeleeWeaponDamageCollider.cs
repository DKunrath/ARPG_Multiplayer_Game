using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DK
{
    public class MeleeWeaponDamageCollider : DamageCollider
    {
        [Header("Attacking Character")]
        public CharacterManager characterCausingDamage; // When calculating damage this is used to check for attackers modifiers, effects, etc


    }
}