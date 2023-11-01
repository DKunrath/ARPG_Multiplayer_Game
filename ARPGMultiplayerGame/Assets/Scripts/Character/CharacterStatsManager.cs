using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace DK
{
    public class CharacterStatsManager : MonoBehaviour
    {
        CharacterManager characterManager;

        protected virtual void Awake()
        { 
            characterManager = GetComponent<CharacterManager>();
        }

        public int CalculateManaBasedOnIntelligenceLevel(int intelligence)
        {
            float mana = 0;

            // Create an equation for how I want the mana to be calculated

            mana = intelligence * 10;

            return Mathf.RoundToInt(mana);
        }

        /*public virtual void RegenerateMana()
        {
            // Only owners can edit their network variable
            if (!characterManager.IsOwner) return;

            if (characterManager.isPerformingAction) return;

            manaRegenerationTimer += Time.deltaTime;

            if (manaRegenerationTimer >= manaRegenerationDelay)
            {
                if (characterManager.characterNetworkManager.currentMana.Value < characterManager.characterNetworkManager.maxMana.Value)
                {
                    manaTickTimer += Time.deltaTime;

                    if (manaTickTimer >= 0.1f)
                    {
                        manaTickTimer = 0;
                        characterManager.characterNetworkManager.currentMana.Value += manaRegenerationAmount;
                    }
                }
            }
        }

        public virtual void ResetManaRegenerationTimer(int previousManaAmount, int currentManaAmount)
        {
            // We only want to reset the regeneration if the action used mana
            // We dont want to reset the regeneration if we are already regenerating mana
            if (currentManaAmount < previousManaAmount)
            {
                manaRegenerationTimer = 0;
            }
        }*/
    }
}