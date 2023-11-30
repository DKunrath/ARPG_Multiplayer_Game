using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DK
{
    public class WorldGameSessionManager : MonoBehaviour
    {
        public static WorldGameSessionManager Instance;

        [Header("Active Players In Session")]
        public List<PlayerManager> players = new List<PlayerManager>();
        // Criar tambem uma lista com o nome dos players = characterName
        // Para ficar melhor do que o ID da conta do jogador

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void AddPlayerToActivePlayersList(PlayerManager playerToBeAdded)
        {
            // Check the list, if it does not alreay contains the player, add them
            if (!players.Contains(playerToBeAdded))
            { 
                players.Add(playerToBeAdded);
            }
        }

        public void RemovePlayerFromActivePlayersList(PlayerManager playerToBeRemoved)
        {
            // Check the list, if it does contains the player, remove them
            if (players.Contains(playerToBeRemoved))
            {
                players.Remove(playerToBeRemoved);
            }

            // Check the list for null slots, and remove the null slots
            for (int i = players.Count - 1; i > -1; i--)
            {
                if (players[i] == null)
                {
                    players.RemoveAt(i);
                }
            }
        }
    }
}