using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;

namespace DK
{
    public class PlayerNetworkManager : CharacterNetworkManager
    {
        PlayerManager playerManager;

        protected override void Awake()
        {
            base.Awake();

            playerManager = GetComponent<PlayerManager>();
        }

        public NetworkVariable<FixedString64Bytes> characterName = new NetworkVariable<FixedString64Bytes>("Character", NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        public void SetNewMaxHealthValue(int oldVitalityValue, int newVitalityValue)
        {
            maxHealth.Value = playerManager.playerStatsManager.CalculateHealthBasedOnVitalityLevel(newVitalityValue);
            //currentHealth.Value = maxHealth.Value;
        }

        public void SetNewMaxManaValue(int oldIntelligenceValue, int newIntelligenceValue)
        {
            maxMana.Value = playerManager.playerStatsManager.CalculateManaBasedOnIntelligenceLevel(newIntelligenceValue);
            //currentMana.Value = maxMana.Value;
        }
    }
}