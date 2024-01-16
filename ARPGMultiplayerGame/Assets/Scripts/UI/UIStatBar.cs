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

        public virtual void SetMaxStat(float maxValue)
        {
            slider.maxValue = maxValue;
            slider.value = maxValue;
        }

        public float GetStat()
        { 
            return slider.value;
        }

        public float GetMaxStat()
        {
            return slider.maxValue;
        }

        public virtual void RegenerateMana(float manaToRegeneratePerSecond)
        {
            slider.value += manaToRegeneratePerSecond;
        }

        public virtual void RemoveMana(float manaToDrain)
        { 
            slider.value -= manaToDrain;
        }
    }
}