using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace DK
{
    public class PlayerUIHUDManager : MonoBehaviour
    {
        [SerializeField] UIStatBar manaBar;

        public void SetNewManaValue(int oldValue, int newValue)
        {
            manaBar.SetStat(newValue);
        }

        public void SetMaxManaValue(float maxMana)
        {
            manaBar.SetMaxStat(maxMana);
        }

        public void RegenerateMana(float manaToRegenerate)
        { 
            manaBar.RegenerateMana(manaToRegenerate);
        }    

        public void RemoveMana(float manaToDrain)
        {
            manaBar.RemoveMana(manaToDrain);
        }
    }
}