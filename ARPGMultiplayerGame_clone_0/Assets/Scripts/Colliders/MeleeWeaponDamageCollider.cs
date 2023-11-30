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

        protected override void Awake()
        {
            base.Awake();

            if (damageCollider == null)
            { 
                damageCollider = GetComponent<Collider>();
            }

            damageCollider.enabled = false; // Melle weapon colliders should be disabled at start, only enabled when animations allow
        }

        protected override void OnTriggerEnter(Collider other)
        {
            CharacterManager damageTarget = other.GetComponentInParent<CharacterManager>();

            if (damageTarget != null)
            {
                // We do not want to damage ourselves
                if (damageTarget == characterCausingDamage) return;

                contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);

                // Check if we can damage this target based on friendly fire

                // Check if target is blocking

                // Check if target is invulnerable

                // Damage
                DamageTarget(damageTarget);
            }

        }

        protected override void DamageTarget(CharacterManager damageTarget)
        {
            // We dont want to damage the same target more than once in a single attack
            // So we add them to a list that checks before applying damage

            if (charactersDamaged.Contains(damageTarget)) return;

            charactersDamaged.Add(damageTarget);

            TakeDamageEffect damageEffect = Instantiate(WorldCharacterEffectsManager.Instance.takeDamageEffect);
            damageEffect.physicalDamage = physicalDamage;
            damageEffect.magicalDamage = magicalDamage;
            damageEffect.fireDamage = fireDamage;
            damageEffect.iceDamage = iceDamage;
            damageEffect.lightningDamage = lightningDamage;
            damageEffect.earthDamage = earthDamage;
            damageEffect.poisonDamage = poisonDamage;
            damageEffect.holyDamage = holyDamage;
            damageEffect.darkMagicDamage = darkMagicDamage;
            damageEffect.poiseDamage = poiseDamage;
            damageEffect.contactPoint = contactPoint;
            damageEffect.angleHitFrom = Vector3.SignedAngle(characterCausingDamage.transform.forward, damageTarget.transform.forward, Vector3.up);

            switch (characterCausingDamage.characterCombatManager.currentAttackType) 
            {
                case AttackType.LightAttack01:
                    ApplyAttackDamageModifiers(light_Attack_01_Modifier, damageEffect);
                    break;
                case AttackType.SpellAttack01:
                    ApplyAttackDamageModifiers(spell_Attack_01_Modifier, damageEffect);
                    break;
                default: 
                    break;
            }

            //damageTarget.characterEffectsManager.ProcessInstantEffect(damageEffect);

            if (characterCausingDamage.IsOwner)
            {
                // Send a damage request to the server
                damageTarget.characterNetworkManager.NotifyTheServerOfCharacterDamageServerRpc(
                    damageTarget.NetworkObjectId,
                    characterCausingDamage.NetworkObjectId,
                    damageEffect.physicalDamage,
                    damageEffect.magicalDamage,
                    damageEffect.fireDamage,
                    damageEffect.iceDamage,
                    damageEffect.lightningDamage,
                    damageEffect.earthDamage,
                    damageEffect.poisonDamage,
                    damageEffect.holyDamage,
                    damageEffect.darkMagicDamage,
                    damageEffect.poiseDamage,
                    damageEffect.angleHitFrom,
                    damageEffect.contactPoint.x,
                    damageEffect.contactPoint.y,
                    damageEffect.contactPoint.z);
            }
        }

        private void ApplyAttackDamageModifiers(float modifier, TakeDamageEffect damage)
        {
            damage.physicalDamage *= modifier;
            damage.magicalDamage *= modifier;
            damage.fireDamage *= modifier;
            damage.iceDamage *= modifier;
            damage.lightningDamage *= modifier;
            damage.earthDamage *= modifier;
            damage.poisonDamage *= modifier;
            damage.holyDamage *= modifier;
            damage.darkMagicDamage *= modifier;
            damage.poiseDamage *= modifier;

            // If attack is a fully charged heavy, MULTIPLY by full charge attack midifier after normal modifier have been calculated
        }
    }
}