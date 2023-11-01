using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace DK
{
    public class PlayerUIManager : MonoBehaviour
    {
        public static PlayerUIManager Instance;

        [Header("Network Join")]
        [SerializeField] bool startGameAsClient;

        [HideInInspector] public PlayerUIHUDManager playerUIHUDManager;

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

            playerUIHUDManager = GetComponentInChildren<PlayerUIHUDManager>();
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (startGameAsClient)
            { 
                startGameAsClient = false;
                // E preciso, primeiramente, desligar, pois nos iniciamos como um host na tela de main menu
                NetworkManager.Singleton.Shutdown();
                // Entao nos reiniciamos, como um client
                NetworkManager.Singleton.StartClient();
            }
        }
    }
}
