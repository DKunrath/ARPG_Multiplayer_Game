using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DK
{
    public class CharacterEffectsManager : MonoBehaviour
    {
        // Process instant effects (take damage, heal)

        // Process timed effects (poinson, build ups)

        // Process static effects (adding/removing buffs from talismans etc)

        CharacterManager characterManager;

        [Header("VFX")]
        [SerializeField] GameObject bloodSplatterVFX;

        protected virtual void Awake()
        {
            characterManager = GetComponent<CharacterManager>();
        }

        public virtual void ProcessInstantEffect(InstantCharacterEffect effect)
        {
            // Take in an effect
            // Process it

            effect.ProcessEffect(characterManager);
        }

        public void PlayBloodSplatterVFX(Vector3 contactPoint)
        {
            // If we manually have placed a blood splatter vfx on this model, play its version
            if (bloodSplatterVFX != null)
            {
                GameObject bloodSplatter = Instantiate(bloodSplatterVFX, contactPoint, Quaternion.identity);
            }
            // Else, use the generic (default version) we have elsewhere
            else
            {
                GameObject bloodSplatter = Instantiate(WorldCharacterEffectsManager.Instance.bloodSplatterVFX, contactPoint, Quaternion.identity);
            }
        }
    }
}