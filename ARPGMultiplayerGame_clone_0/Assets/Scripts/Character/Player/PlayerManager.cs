using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;

namespace DK
{
    public class PlayerManager : CharacterManager
    {
        [Header("Debug Menu")]
        [SerializeField] bool respawnCharacter = false;
        [SerializeField] bool switchRightWeapon = false;
        [SerializeField] bool switchLeftWeapon = false;

        [HideInInspector] public PlayerLocomotionManager playerLocomotionManager;
        [HideInInspector] public PlayerAnimatorManager playerAnimatorManager;
        [HideInInspector] public PlayerNetworkManager playerNetworkManager;
        [HideInInspector] public PlayerStatsManager playerStatsManager;
        [HideInInspector] public PlayerInventoryManager playerInventoryManager;
        [HideInInspector] public PlayerEquipmentManager playerEquipmentManager;
        [HideInInspector] public PlayerCombatManager playerCombatManager;

        [Header("Mana Regeneration Stats")]
        private int manaForPlayerToRegenerate = 5; // Colocar funcao para levar em consideracao os itens utilizados pelo player
        private float manaRegenerationTimerDelay = 5f; // Tempo que deve ser esperado antes de regenerar uma porcentagem da mana
        float timeToWaitBeforeRegeneration = 0f;

        protected override void Awake()
        {
            base.Awake();

            // Faca mais coisas, apenas para o player
            playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
            playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
            playerNetworkManager = GetComponent<PlayerNetworkManager>();
            playerStatsManager = GetComponent<PlayerStatsManager>();
            playerInventoryManager = GetComponent<PlayerInventoryManager>();
            playerEquipmentManager = GetComponent<PlayerEquipmentManager>();
            playerCombatManager = GetComponent<PlayerCombatManager>();
        }

        protected override void Update()
        {
            base.Update();

            if (!IsOwner) return;

            // Lida com todos os movimentos do player
            playerLocomotionManager.HandleAllMovement();
            // Regen Mana
            HandleManaRegeneration();
            //playerStatsManager.RegenerateMana();

            DebugMenu();
        }

        protected override void LateUpdate()
        {
            if (!IsOwner) return;

            base.LateUpdate();

            PlayerCamera.Instance.HandleAllCameraActions();
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectedCallback;

            // If this is the player object owned by this client
            if (IsOwner)
            {
                PlayerCamera.Instance.playerManager = this;
                PlayerInputManager.Instance.playerManager = this;
                WorldSaveGameManager.Instance.player = this;

                // Update the total amount of health or mana when the stat linked to either changes
                playerNetworkManager.vitality.OnValueChanged += playerNetworkManager.SetNewMaxHealthValue;
                playerNetworkManager.intelligence.OnValueChanged += playerNetworkManager.SetNewMaxSoulPowerValue;
            }

            // All the things done here, counts to every character in the game

            // Stats
            playerNetworkManager.currentHealth.OnValueChanged += playerNetworkManager.CheckHP;

            // Equipment 
            playerNetworkManager.currentRightHandWeaponID.OnValueChanged += playerNetworkManager.OnCurrentRightHandWeaponIDChange;
            playerNetworkManager.currentLeftHandWeaponID.OnValueChanged += playerNetworkManager.OnCurrentLeftHandWeaponIDChange;
            playerNetworkManager.currentWeaponBeingUsed.OnValueChanged += playerNetworkManager.OnCurrentWeaponBeingUsedIDChange;

            // Upon connecting, if we are the owner of this character, but we are not the server, reload our character data to this newly instantiated character
            // We dont run this if we are the server, because since they are the host, they are already loaded in and dont need to reload their data
            if (IsOwner && !IsServer)
            {
                LoadGameDataFromCurrentCharacterData(ref WorldSaveGameManager.Instance.currentCharacterData);
            }
        }

        private void OnClientConnectedCallback(ulong clientID)
        {
            // Add this player to the players list on the server
            WorldGameSessionManager.Instance.AddPlayerToActivePlayersList(this);

            // If we are the server, we are the host, so we dont need to load players to sync them
            // You only need to load other players gear to sync it if you join a game thats already been active without you being present
            if (!IsServer && IsOwner)
            {
                foreach (var player in WorldGameSessionManager.Instance.players)
                {
                    if (player != this)
                    {
                        player.LoadOtherPlayerCharacterWhenJoiningServer();
                    }
                }
            }
        }

        public override IEnumerator ProcessDeathEvent(bool manuallySelectDeathAnimation = false)
        {
            if (IsOwner)
            {
                PlayerUIManager.Instance.playerUIPopUpManager.SendYouDiedPopUp();
            }

            return base.ProcessDeathEvent(manuallySelectDeathAnimation);

            // Check for players that are alive, if there are none alive (0) respawn characters
        }

        public override void ReviveCharacter()
        {
            base.ReviveCharacter();

            if (IsOwner)
            { 
                playerNetworkManager.currentHealth.Value = playerNetworkManager.maxHealth.Value;
                playerNetworkManager.currentSoulPower.Value = playerNetworkManager.maxSoulPower.Value;
                // Restore everything needed

                // Play rebirth effects

                playerAnimatorManager.PlayTargetActionAnimation("Empty", false);
            }
        }

        public void SaveGameDataToCurrentCharacterData(ref CharacterSaveData currentCharacterData)
        {
            currentCharacterData.sceneIndex = SceneManager.GetActiveScene().buildIndex;

            currentCharacterData.characterName = playerNetworkManager.characterName.Value.ToString();
            currentCharacterData.xPosition = transform.position.x;
            currentCharacterData.yPosition = transform.position.y;
            currentCharacterData.zPosition = transform.position.z;

            currentCharacterData.currentHealth = playerNetworkManager.currentHealth.Value;
            currentCharacterData.currentSoulPower = playerNetworkManager.currentSoulPower.Value;

            currentCharacterData.vitality = playerNetworkManager.vitality.Value;
            currentCharacterData.intelligence = playerNetworkManager.intelligence.Value;
        }

        public void SaveNewGameDataToCurrentCharacterData(ref CharacterSaveData currentCharacterData)
        {
            currentCharacterData.sceneIndex = 1;

            currentCharacterData.characterName = playerNetworkManager.characterName.Value.ToString();
            currentCharacterData.xPosition = transform.position.x;
            currentCharacterData.yPosition = transform.position.y;
            currentCharacterData.zPosition = transform.position.z;

            currentCharacterData.currentHealth = playerNetworkManager.currentHealth.Value;
            currentCharacterData.currentSoulPower = playerNetworkManager.currentSoulPower.Value;

            currentCharacterData.vitality = playerNetworkManager.vitality.Value;
            currentCharacterData.intelligence = playerNetworkManager.intelligence.Value;
        }

        public void LoadGameDataFromCurrentCharacterData(ref CharacterSaveData currentCharacterData)
        {
            playerNetworkManager.characterName.Value = currentCharacterData.characterName;
            Vector3 myPosition = new Vector3(currentCharacterData.xPosition, currentCharacterData.yPosition, currentCharacterData.zPosition);
            transform.position = myPosition;

            // Se os stats nao estartarem com valores prontos para o client, os valores ficam zerados
            // Provavelemnte por estarem sendo rodados na mesma maquina, o que pode gerar duplicidade dos arquivos
            // Testar novamente no futuro em computadores diferentes
            if (IsOwner && !IsServer)
            {
                playerNetworkManager.vitality.Value = 10;
                playerNetworkManager.intelligence.Value = 10;
            }
            else
            {
                playerNetworkManager.vitality.Value = currentCharacterData.vitality;
                playerNetworkManager.intelligence.Value = currentCharacterData.intelligence;
            }

            playerNetworkManager.maxHealth.Value = playerStatsManager.CalculateHealthBasedOnVitalityLevel(playerNetworkManager.vitality.Value);
            playerNetworkManager.maxSoulPower.Value = playerStatsManager.CalculateSoulPowerBasedOnIntelligenceLevel(playerNetworkManager.intelligence.Value);

            if (currentCharacterData.currentHealth == 0 && currentCharacterData.currentSoulPower == 0)
            {
                playerNetworkManager.currentHealth.Value = playerNetworkManager.maxHealth.Value;
                playerNetworkManager.currentSoulPower.Value = playerNetworkManager.maxSoulPower.Value;
            }
            else
            {
                playerNetworkManager.currentHealth.Value = currentCharacterData.currentHealth;
                playerNetworkManager.currentSoulPower.Value = currentCharacterData.currentSoulPower;
            }

            PlayerUIManager.Instance.playerUIHUDManager.SetMaxHealthValue(playerNetworkManager.maxHealth.Value);
            PlayerUIManager.Instance.playerUIHUDManager.SetMaxManaValue(playerNetworkManager.maxSoulPower.Value);

            PlayerUIManager.Instance.playerUIHUDManager.SetNewHealthValue(0, Mathf.RoundToInt(playerNetworkManager.currentHealth.Value));
            PlayerUIManager.Instance.playerUIHUDManager.SetNewManaValue(0, Mathf.RoundToInt(playerNetworkManager.currentSoulPower.Value));
            
        }

        public void LoadOtherPlayerCharacterWhenJoiningServer()
        {
            // Sync Weapons
            playerNetworkManager.OnCurrentRightHandWeaponIDChange(0, playerNetworkManager.currentRightHandWeaponID.Value);
            playerNetworkManager.OnCurrentLeftHandWeaponIDChange(0, playerNetworkManager.currentLeftHandWeaponID.Value);

            // Sync Armor
        }

        // DEBUG DELETE LATER

        private void DebugMenu()
        {
            if (respawnCharacter)
            {
                respawnCharacter = false;
                ReviveCharacter();
            }

            if (switchRightWeapon)
            {
                switchRightWeapon = false;
                playerEquipmentManager.SwitchRightWeapon();
            }

            if (switchLeftWeapon)
            {
                switchLeftWeapon = false;
                playerEquipmentManager.SwitchLeftWeapon();
            }
        }

        #region Mana Drain & Regeneration

        public void HandleManaRegeneration()
        {
            // Only owners can edit their network variable
            if (!IsOwner) return;

            if (isPerformingAction) return;

            if (playerNetworkManager.currentSoulPower.Value == playerNetworkManager.maxSoulPower.Value) return;

            timeToWaitBeforeRegeneration += Time.deltaTime;
            
            if (playerNetworkManager.currentSoulPower.Value < playerNetworkManager.maxSoulPower.Value && timeToWaitBeforeRegeneration > manaRegenerationTimerDelay)
            {
                PlayerUIManager.Instance.playerUIHUDManager.RegenerateMana(manaForPlayerToRegenerate);
                playerNetworkManager.currentSoulPower.Value += manaForPlayerToRegenerate;

                if (playerNetworkManager.currentSoulPower.Value > playerNetworkManager.maxSoulPower.Value)
                { 
                    playerNetworkManager.currentSoulPower.Value = playerNetworkManager.maxSoulPower.Value;
                }

                timeToWaitBeforeRegeneration = 0;
            }
        }

        public int GetManaForPlayerToRegenerate()
        {
            return manaForPlayerToRegenerate;
        }

        public void SetManaForPlayerToRegenerate(int manaFromDifferentItems)
        { 
            manaForPlayerToRegenerate = manaFromDifferentItems;
        }

        public float GetManaRegenerationTimerDelay()
        {
            return manaRegenerationTimerDelay;
        }

        public void SetManaRegenerationTimerDelay(float timeFromDifferentItems)
        { 
            manaRegenerationTimerDelay = timeFromDifferentItems;
        }

        #endregion
    }
}