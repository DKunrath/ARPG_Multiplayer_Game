using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DK
{
    public class UIStatBar : MonoBehaviour
    {
        [SerializeField] private Slider slider;

        public virtual void SetStat(int newValue)
        { 
            slider.value = newValue;
        }

        public virtual void SetMaxStat(int maxValue)
        {
            slider.maxValue = maxValue;
            slider.value = maxValue;
        }

        public virtual void RegenerateMana(int manaToRegeneratePerSecond)
        {
            slider.value += manaToRegeneratePerSecond;
        }

        public virtual void RemoveMana(int manaToDrain)
        { 
            slider.value -= manaToDrain;
        }
    }
}