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

        protected virtual void Start()
        { 
            
        }

        public int CalculateHealthBasedOnVitalityLevel(int vitality)
        {
            float health = 0;

            // Create an equation for how I want the mana to be calculated

            health = vitality * 10;

            return Mathf.RoundToInt(health);
        }

        public int CalculateSoulPowerBasedOnIntelligenceLevel(int intelligence)
        {
            float soulPower = 0;

            // Create an equation for how I want the mana to be calculated

            soulPower = intelligence * 10;

            return Mathf.RoundToInt(soulPower);
        }
    }
}