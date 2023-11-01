using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace DK
{
    public class UICharacterSaveSlot : MonoBehaviour
    {
        SaveFileDataWriter saveFileDataWriter;

        [Header("Game Slot")]
        public CharacterSlot characterSlot;

        [Header("Character Information")]
        public TextMeshProUGUI characterName;
        public TextMeshProUGUI timePlayed;

        private void OnEnable()
        {
            LoadSaveSlots();
        }

        private void LoadSaveSlots()
        {
            saveFileDataWriter = new SaveFileDataWriter();
            saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;

            // SAVE SLOT 01
            if (characterSlot == CharacterSlot.CharacterSlot_01)
            {
                saveFileDataWriter.saveFileName = WorldSaveGameManager.Instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                // IF THE FILE EXISTS, GET INFORMATION FROM IT
                if (saveFileDataWriter.CheckToSeeIfFileExists())
                {
                    characterName.text = WorldSaveGameManager.Instance.characterSlot01.characterName;
                }
                // IF IT DOES NOT, DISABLE THIS GAMEOBJECT
                else
                {
                    gameObject.SetActive(false);
                }
            }
            // SAVE SLOT 02
            else if (characterSlot == CharacterSlot.CharacterSlot_02)
            {
                saveFileDataWriter.saveFileName = WorldSaveGameManager.Instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                // IF THE FILE EXISTS, GET INFORMATION FROM IT
                if (saveFileDataWriter.CheckToSeeIfFileExists())
                {
                    characterName.text = WorldSaveGameManager.Instance.characterSlot02.characterName;
                }
                // IF IT DOES NOT, DISABLE THIS GAMEOBJECT
                else
                {
                    gameObject.SetActive(false);
                }
            }
            // SAVE SLOT 03
            else if (characterSlot == CharacterSlot.CharacterSlot_03)
            {
                saveFileDataWriter.saveFileName = WorldSaveGameManager.Instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                // IF THE FILE EXISTS, GET INFORMATION FROM IT
                if (saveFileDataWriter.CheckToSeeIfFileExists())
                {
                    characterName.text = WorldSaveGameManager.Instance.characterSlot03.characterName;
                }
                // IF IT DOES NOT, DISABLE THIS GAMEOBJECT
                else
                {
                    gameObject.SetActive(false);
                }
            }
            // SAVE SLOT 04
            else if (characterSlot == CharacterSlot.CharacterSlot_04)
            {
                saveFileDataWriter.saveFileName = WorldSaveGameManager.Instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                // IF THE FILE EXISTS, GET INFORMATION FROM IT
                if (saveFileDataWriter.CheckToSeeIfFileExists())
                {
                    characterName.text = WorldSaveGameManager.Instance.characterSlot04.characterName;
                }
                // IF IT DOES NOT, DISABLE THIS GAMEOBJECT
                else
                {
                    gameObject.SetActive(false);
                }
            }
            // SAVE SLOT 05
            else if (characterSlot == CharacterSlot.CharacterSlot_05)
            {
                saveFileDataWriter.saveFileName = WorldSaveGameManager.Instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                // IF THE FILE EXISTS, GET INFORMATION FROM IT
                if (saveFileDataWriter.CheckToSeeIfFileExists())
                {
                    characterName.text = WorldSaveGameManager.Instance.characterSlot05.characterName;
                }
                // IF IT DOES NOT, DISABLE THIS GAMEOBJECT
                else
                {
                    gameObject.SetActive(false);
                }
            }
            // SAVE SLOT 06
            else if (characterSlot == CharacterSlot.CharacterSlot_06)
            {
                saveFileDataWriter.saveFileName = WorldSaveGameManager.Instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                // IF THE FILE EXISTS, GET INFORMATION FROM IT
                if (saveFileDataWriter.CheckToSeeIfFileExists())
                {
                    characterName.text = WorldSaveGameManager.Instance.characterSlot06.characterName;
                }
                // IF IT DOES NOT, DISABLE THIS GAMEOBJECT
                else
                {
                    gameObject.SetActive(false);
                }
            }
            // SAVE SLOT 07
            else if (characterSlot == CharacterSlot.CharacterSlot_07)
            {
                saveFileDataWriter.saveFileName = WorldSaveGameManager.Instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                // IF THE FILE EXISTS, GET INFORMATION FROM IT
                if (saveFileDataWriter.CheckToSeeIfFileExists())
                {
                    characterName.text = WorldSaveGameManager.Instance.characterSlot07.characterName;
                }
                // IF IT DOES NOT, DISABLE THIS GAMEOBJECT
                else
                {
                    gameObject.SetActive(false);
                }
            }
            // SAVE SLOT 08
            else if (characterSlot == CharacterSlot.CharacterSlot_08)
            {
                saveFileDataWriter.saveFileName = WorldSaveGameManager.Instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                // IF THE FILE EXISTS, GET INFORMATION FROM IT
                if (saveFileDataWriter.CheckToSeeIfFileExists())
                {
                    characterName.text = WorldSaveGameManager.Instance.characterSlot08.characterName;
                }
                // IF IT DOES NOT, DISABLE THIS GAMEOBJECT
                else
                {
                    gameObject.SetActive(false);
                }
            }
            // SAVE SLOT 09
            else if (characterSlot == CharacterSlot.CharacterSlot_09)
            {
                saveFileDataWriter.saveFileName = WorldSaveGameManager.Instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                // IF THE FILE EXISTS, GET INFORMATION FROM IT
                if (saveFileDataWriter.CheckToSeeIfFileExists())
                {
                    characterName.text = WorldSaveGameManager.Instance.characterSlot09.characterName;
                }
                // IF IT DOES NOT, DISABLE THIS GAMEOBJECT
                else
                {
                    gameObject.SetActive(false);
                }
            }
            // SAVE SLOT 10
            else if (characterSlot == CharacterSlot.CharacterSlot_10)
            {
                saveFileDataWriter.saveFileName = WorldSaveGameManager.Instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                // IF THE FILE EXISTS, GET INFORMATION FROM IT
                if (saveFileDataWriter.CheckToSeeIfFileExists())
                {
                    characterName.text = WorldSaveGameManager.Instance.characterSlot10.characterName;
                }
                // IF IT DOES NOT, DISABLE THIS GAMEOBJECT
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }

        public void LoadGameFromCharacterSlot()
        {
            WorldSaveGameManager.Instance.currentCharacterSlotBeingUsed = characterSlot;
            WorldSaveGameManager.Instance.LoadGame();
        }

        public void SelectCurrentSlot()
        { 
            TitleScreenManager.Instance.SelectCharacterSLot(characterSlot);
        }
    }
}