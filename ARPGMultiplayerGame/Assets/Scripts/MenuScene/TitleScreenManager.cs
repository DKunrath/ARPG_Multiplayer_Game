using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace DK
{
    public class TitleScreenManager : MonoBehaviour
    {
        public static TitleScreenManager Instance;

        [Header("Menus")]
        [SerializeField] GameObject titleScreenMainMenu;
        [SerializeField] GameObject titleScreenLoadMenu;

        [Header("Buttons")]
        [SerializeField] Button mainMenuNewGameButton;
        [SerializeField] Button loadMenuReturnButton;
        [SerializeField] Button mainMenuLoadGameButton;
        [SerializeField] Button deleteCharacterPopUpConfirmButton;

        [Header("Pop Ups")]
        [SerializeField] GameObject noCharacterSlotsPopUp;
        [SerializeField] Button noCharacterSlotsOkayButton;
        [SerializeField] GameObject deleteCharacterSlotPopUp;

        [Header("Character Slots")]
        public CharacterSlot currentSelectedSlot = CharacterSlot.NO_SLOT;

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

        public void StartNetworkAsHost()
        {
            NetworkManager.Singleton.StartHost();
        }

        public void StartNewGame()
        {
            WorldSaveGameManager.Instance.AttemptToCreateNewGame();
        }

        public void OpenLoadGameMenu()
        { 
            // CLOSE MAIN MENU
            titleScreenMainMenu.SetActive(false);

            // OPEN LOAD MENU
            titleScreenLoadMenu.SetActive(true);

            // SELECT RETURN BUTTON
            loadMenuReturnButton.Select();
        }

        public void CloseLoadGameMenu() 
        {
            // OPEN MAIN MENU
            titleScreenMainMenu.SetActive(true);

            // CLOSE LOAD MENU
            titleScreenLoadMenu.SetActive(false);

            // SELECT LOAD GAME BUTTON
            mainMenuNewGameButton.Select();
        }

        public void DisplayNoFreeCharacterSlotsPopUp()
        { 
            noCharacterSlotsPopUp.SetActive(true);
            noCharacterSlotsOkayButton.Select();
        }

        public void CloseNoFreeCharacterSlotsPopUp()
        {
            noCharacterSlotsPopUp.SetActive(false);
            mainMenuNewGameButton.Select();
        }

        // Character Slots

        public void SelectCharacterSLot(CharacterSlot characterSlot)
        {
            currentSelectedSlot = characterSlot;  
        }

        public void SelectNoSlot()
        {
            currentSelectedSlot = CharacterSlot.NO_SLOT;
        }

        public void AttemptToDeleteCharacterSlot()
        {
            if (currentSelectedSlot != CharacterSlot.NO_SLOT)
            {
                deleteCharacterSlotPopUp.SetActive(true);
                deleteCharacterPopUpConfirmButton.Select();
            }
        }

        public void DeleteCharacterSlot()
        {
            deleteCharacterSlotPopUp.SetActive(false);
            WorldSaveGameManager.Instance.DeleteGame(currentSelectedSlot);

            // Refresh load menu screen, to refresh the slots
            titleScreenLoadMenu.SetActive(false);
            titleScreenLoadMenu.SetActive(true);

            loadMenuReturnButton.Select();
        }

        public void CloseDeleteCharacterPopUp()
        {
            deleteCharacterSlotPopUp.SetActive(false);
            loadMenuReturnButton.Select();
        }
    }
}