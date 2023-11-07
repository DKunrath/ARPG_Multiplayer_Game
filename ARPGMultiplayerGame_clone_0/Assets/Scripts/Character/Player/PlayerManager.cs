using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DK
{
    public class PlayerManager : CharacterManager
    {
        [Header("Debug Menu")]
        [SerializeField] bool respawnCharacter = false;
        [SerializeField] bool switchRightWeapon = false;

        [HideInInspector] public PlayerLocomotionManager playerLocomotionManager;
        [HideInInspector] public PlayerAnimatorManager playerAnimatorManager;
        [HideInInspector] public PlayerNetworkManager playerNetworkManager;
        [HideInInspector] public PlayerStatsManager playerStatsManager;
        [HideInInspector] public PlayerInventoryManager playerInventoryManager;
        [HideInInspector] public PlayerEquipmentManager playerEquipmentManager;

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

            // If this is the player object owned by this client
            if (IsOwner)
            {
                PlayerCamera.Instance.playerManager = this;
                PlayerInputManager.Instance.playerManager = this;
                WorldSaveGameManager.Instance.player = this;

                // Update the total amount of health or mana when the stat linked to either changes
                playerNetworkManager.vitality.OnValueChanged += playerNetworkManager.SetNewMaxHealthValue;
                playerNetworkManager.intelligence.OnValueChanged += playerNetworkManager.SetNewMaxManaValue;
            }

            // All the things done here, counts to every character in the game

            // Stats
            playerNetworkManager.currentHealth.OnValueChanged += playerNetworkManager.CheckHP;

            // Equipment 
            playerNetworkManager.currentRightHandWeaponID.OnValueChanged += playerNetworkManager.OnCurrentRightHandWeaponIDChange;
            playerNetworkManager.currentLeftHandWeaponID.OnValueChanged += playerNetworkManager.OnCurrentLeftHandWeaponIDChange;
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
                playerNetworkManager.currentMana.Value = playerNetworkManager.maxMana.Value;
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
            currentCharacterData.currentMana = playerNetworkManager.currentMana.Value;

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
            currentCharacterData.currentMana = playerNetworkManager.currentMana.Value;

            currentCharacterData.vitality = playerNetworkManager.vitality.Value;
            currentCharacterData.intelligence = playerNetworkManager.intelligence.Value;
        }

        public void LoadGameDataFromCurrentCharacterData(ref CharacterSaveData currentCharacterData)
        {
            playerNetworkManager.characterName.Value = currentCharacterData.characterName;
            Vector3 myPosition = new Vector3(currentCharacterData.xPosition, currentCharacterData.yPosition, currentCharacterData.zPosition);
            transform.position = myPosition;

            playerNetworkManager.vitality.Value = currentCharacterData.vitality;
            playerNetworkManager.intelligence.Value = currentCharacterData.intelligence;

            playerNetworkManager.maxHealth.Value = playerStatsManager.CalculateHealthBasedOnVitalityLevel(playerNetworkManager.vitality.Value);
            playerNetworkManager.maxMana.Value = playerStatsManager.CalculateManaBasedOnIntelligenceLevel(playerNetworkManager.intelligence.Value);

            if (currentCharacterData.currentHealth == 0 && currentCharacterData.currentMana == 0)
            {
                playerNetworkManager.currentHealth.Value = playerNetworkManager.maxHealth.Value;
                playerNetworkManager.currentMana.Value = playerNetworkManager.maxMana.Value;
            }
            else
            {
                playerNetworkManager.currentHealth.Value = currentCharacterData.currentHealth;
                playerNetworkManager.currentMana.Value = currentCharacterData.currentMana;
            }

            PlayerUIManager.Instance.playerUIHUDManager.SetMaxHealthValue(playerNetworkManager.maxHealth.Value);
            PlayerUIManager.Instance.playerUIHUDManager.SetMaxManaValue(playerNetworkManager.maxMana.Value);

            PlayerUIManager.Instance.playerUIHUDManager.SetNewHealthValue(0, Mathf.RoundToInt(playerNetworkManager.currentHealth.Value));
            PlayerUIManager.Instance.playerUIHUDManager.SetNewManaValue(0, Mathf.RoundToInt(playerNetworkManager.currentMana.Value));
            
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
        }

        #region Mana Drain & Regeneration

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