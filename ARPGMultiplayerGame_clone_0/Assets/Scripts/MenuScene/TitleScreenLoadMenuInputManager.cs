using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DK
{
    public class TitleScreenLoadMenuInputManager : MonoBehaviour
    {
        PlayerControls playerControls;

        [Header("Title Screen Inputs")]
        [SerializeField] bool deleteCharacterSlot = false;

        private void Update()
        {
            if (deleteCharacterSlot)
            { 
                deleteCharacterSlot = false;
                TitleScreenManager.Instance.AttemptToDeleteCharacterSlot();
            }
        }

        private void OnEnable()
        {
            if (playerControls == null)
            { 
                playerControls = new PlayerControls();
                // CONSOLE
                playerControls.UI.XConsole.performed += i => deleteCharacterSlot = true;
                // PC
                playerControls.UI.MouseLeftButton.performed += i => deleteCharacterSlot = true;
            }

            playerControls.Enable();
        }

        private void OnDisable()
        {
            playerControls.Disable();
        }
    }
}