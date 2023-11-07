using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace DK
{
    public class PlayerUIHUDManager : MonoBehaviour
    {
        [SerializeField] UIStatBar healthBar;
        [SerializeField] UIStatBar healthBarPlayerFrame;
        [SerializeField] UIStatBar manaBar;
        [SerializeField] UIStatBar manaBarPlayerFrame;

        public void SetNewHealthValue(int oldValue, int newValue)
        {
            healthBar.SetStat(newValue);
            healthBarPlayerFrame.SetStat(newValue);
        }

        public void SetMaxHealthValue(float maxHealth)
        {
            healthBar.SetMaxStat(maxHealth);
            healthBarPlayerFrame.SetMaxStat(maxHealth);
        }

        public void SetNewManaValue(int oldValue, int newValue)
        {
            manaBar.SetStat(newValue);
            manaBarPlayerFrame.SetStat(newValue);
        }

        public void SetMaxManaValue(float maxMana)
        {
            manaBar.SetMaxStat(maxMana);
            manaBarPlayerFrame.SetMaxStat(maxMana);
        }

        public void RegenerateMana(float manaToRegenerate)
        { 
            manaBar.RegenerateMana(manaToRegenerate);
            manaBarPlayerFrame.RegenerateMana(manaToRegenerate);
        }    

        public void RemoveMana(float manaToDrain)
        {
            manaBar.RemoveMana(manaToDrain);
            manaBarPlayerFrame.RemoveMana(manaToDrain);
        }
    }
}