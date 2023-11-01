using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DK
{
    public class PlayerManager : CharacterManager
    {
        [HideInInspector] public PlayerLocomotionManager playerLocomotionManager;
        [HideInInspector] public PlayerAnimatorManager playerAnimatorManager;
        [HideInInspector] public PlayerNetworkManager playerNetworkManager;
        [HideInInspector] public PlayerStatsManager playerStatsManager;

        [Header("Mana Regeneration Stats")]
        private int manaToDrainFromPlayer = 10; // Colocar funcao para levar em consideracao a acao a ser usada, e os itens que dominuem o gasto de mana
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

            // If this is the player object owned by this client
            if (IsOwner)
            {
                PlayerCamera.Instance.playerManager = this;
                PlayerInputManager.Instance.playerManager = this;
                WorldSaveGameManager.Instance.player = this;

                //playerNetworkManager.currentMana.OnValueChanged += PlayerUIManager.Instance.playerUIHUDManager.SetNewManaValue;
                //playerNetworkManager.currentMana.OnValueChanged += playerStatsManager.ResetManaRegenerationTimer;

                // This will be moved when saving and loading is added
                playerNetworkManager.maxMana.Value = playerStatsManager.CalculateManaBasedOnIntelligenceLevel(playerNetworkManager.intelligence.Value);
                playerNetworkManager.currentMana.Value = playerNetworkManager.maxMana.Value;
                PlayerUIManager.Instance.playerUIHUDManager.SetMaxManaValue(playerNetworkManager.maxMana.Value);
            }
        }

        public void SaveGameDataToCurrentCharacterData(ref CharacterSaveData currentCharacterData)
        {
            currentCharacterData.sceneIndex = SceneManager.GetActiveScene().buildIndex;

            currentCharacterData.characterName = playerNetworkManager.characterName.Value.ToString();
            currentCharacterData.xPosition = transform.position.x;
            currentCharacterData.yPosition = transform.position.y;
            currentCharacterData.zPosition = transform.position.z;
        }

        public void LoadGameDataFromCurrentCharacterData(ref CharacterSaveData currentCharacterData)
        {
            playerNetworkManager.characterName.Value = currentCharacterData.characterName;
            Vector3 myPosition = new Vector3(currentCharacterData.xPosition, currentCharacterData.yPosition, currentCharacterData.zPosition);
            transform.position = myPosition;
        }

        #region Mana Drain & Regeneration

        public void HandleManaDrainInput()
        {
            // Only owners can edit their network variable
            if (!IsOwner) return;

            if (isPerformingAction) return;

            if (playerNetworkManager.currentMana.Value <= 0)
            {
                playerNetworkManager.currentMana.Value = 0;
            }

            if (playerNetworkManager.currentMana.Value == 0) return;

            if (manaToDrainFromPlayer > playerNetworkManager.currentMana.Value) return;

            PlayerUIManager.Instance.playerUIHUDManager.RemoveMana(manaToDrainFromPlayer);
            playerNetworkManager.currentMana.Value -= manaToDrainFromPlayer;
        }

        public void HandleManaRegeneration()
        {
            // Only owners can edit their network variable
            if (!IsOwner) return;

            if (isPerformingAction) return;

            if (playerNetworkManager.currentMana.Value == playerNetworkManager.maxMana.Value) return;

            timeToWaitBeforeRegeneration += Time.deltaTime;
            
            if (playerNetworkManager.currentMana.Value < playerNetworkManager.maxMana.Value && timeToWaitBeforeRegeneration > manaRegenerationTimerDelay)
            {
                PlayerUIManager.Instance.playerUIHUDManager.RegenerateMana(manaForPlayerToRegenerate);
                playerNetworkManager.currentMana.Value += manaForPlayerToRegenerate;
                timeToWaitBeforeRegeneration = 0;
            }
        }

        public int GetManaToDrainFromPlayer()
        {
            return manaToDrainFromPlayer;
        }

        public void SetManaToDrainFromPlayer(int manaFromDifferentAttacks)
        { 
            manaToDrainFromPlayer = manaFromDifferentAttacks;
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