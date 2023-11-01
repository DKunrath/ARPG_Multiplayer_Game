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

        public void SetMaxManaValue(int maxMana)
        {
            manaBar.SetMaxStat(maxMana);
        }

        public void RegenerateMana(int manaToRegenerate)
        { 
            manaBar.RegenerateMana(manaToRegenerate);
        }    

        public void RemoveMana(int manaToDrain)
        {
            manaBar.RemoveMana(manaToDrain);
        }
    }
}