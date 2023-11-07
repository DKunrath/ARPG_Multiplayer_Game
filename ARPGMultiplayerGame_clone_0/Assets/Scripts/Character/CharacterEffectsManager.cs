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
    }
}