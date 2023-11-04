using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DK
{
    public class DamageCollider : MonoBehaviour
    {
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

        [Header("Contact Point")]
        private Vector3 contactPoint;

        [Header("Characters Damaged")]
        protected List<CharacterManager> charactersDamaged = new List<CharacterManager>();

        private void OnTriggerEnter(Collider other)
        {
            CharacterManager damageTarget = other.GetComponent<CharacterManager>();

            if (damageTarget != null)
            {
                contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);

                // Check if we can damage this target based on friendly fire

                // Check if target is blocking

                // Check if target is invulnerable

                // Damage
                DamageTarget(damageTarget);
            }
        }

        protected virtual void DamageTarget(CharacterManager damageTarget)
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
            damageEffect.contactPoint = contactPoint;

            damageTarget.characterEffectsManager.ProcessInstantEffect(damageEffect);
        }
    }
}