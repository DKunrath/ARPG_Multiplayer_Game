using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DK
{
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField] public MeleeWeaponDamageCollider meleeDamageCollider;

        private void Awake()
        {
            meleeDamageCollider = GetComponentInChildren<MeleeWeaponDamageCollider>();
        }

        public void SetWeaponDamage(CharacterManager characterWieldingWeapon, WeaponItem weapon)
        {
            meleeDamageCollider.characterCausingDamage = characterWieldingWeapon;

            meleeDamageCollider.physicalDamage = weapon.physicalDamage;
            meleeDamageCollider.magicalDamage = weapon.magicalDamage;
            meleeDamageCollider.fireDamage = weapon.fireDamage;
            meleeDamageCollider.iceDamage = weapon.iceDamage;
            meleeDamageCollider.lightningDamage = weapon.lightningDamage;
            meleeDamageCollider.earthDamage = weapon.earthDamage;
            meleeDamageCollider.poisonDamage = weapon.poisonDamage;
            meleeDamageCollider.holyDamage = weapon.holyDamage;
            meleeDamageCollider.darkMagicDamage = weapon.darkMagicDamage;

            meleeDamageCollider.light_Attack_01_Modifier = weapon.light_Attack_01_Modifier;
            meleeDamageCollider.spell_Attack_01_Modifier = weapon.spell_Attack_01_Modifier;
        }
    }
}