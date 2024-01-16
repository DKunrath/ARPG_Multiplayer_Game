using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace DK
{
    public class PlayerUIHUDManager : MonoBehaviour
    {
        [SerializeField] UIStatBar healthBar;
        [SerializeField] UIStatBar healthBarPlayerFrame;
        [SerializeField] TextMeshProUGUI currentHealthBarText;
        [SerializeField] TextMeshProUGUI maxHealthBarText;
        [SerializeField] UIStatBar soulPowerBar;
        [SerializeField] UIStatBar manaBarPlayerFrame;
        [SerializeField] TextMeshProUGUI currentSoulPowerBarText;
        [SerializeField] TextMeshProUGUI maxSoulPowerBarText;

        public void SetNewHealthValue(int oldValue, int newValue)
        {
            healthBar.SetStat(newValue);
            healthBarPlayerFrame.SetStat(newValue);
            currentHealthBarText.text = newValue.ToString(); 
        }

        public void SetMaxHealthValue(float maxHealth)
        {
            healthBar.SetMaxStat(maxHealth);
            healthBarPlayerFrame.SetMaxStat(maxHealth);
            maxHealthBarText.text = maxHealth.ToString();
        }

        public void SetNewSoulPowerValue(int oldValue, int newValue)
        {
            soulPowerBar.SetStat(newValue);
            manaBarPlayerFrame.SetStat(newValue);
            currentSoulPowerBarText.text = newValue.ToString();
        }

        public void SetMaxSoulPowerValue(float maxMana)
        {
            soulPowerBar.SetMaxStat(maxMana);
            manaBarPlayerFrame.SetMaxStat(maxMana);
            maxSoulPowerBarText.text = soulPowerBar.GetMaxStat().ToString();
        }

        public void RegenerateSoulPower(float soulPowerToRegenerate, float currentSoulPower)
        { 
            soulPowerBar.RegenerateMana(soulPowerToRegenerate);
            currentSoulPowerBarText.text = currentSoulPower.ToString();
        }

        public void RemoveMana(float manaToDrain)
        {
            soulPowerBar.RemoveMana(manaToDrain);
            manaBarPlayerFrame.RemoveMana(manaToDrain);
        }
    }
}