using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DK
{
    public class PlayerInputManager : MonoBehaviour
    {
        public static PlayerInputManager Instance;

        [SerializeField] public PlayerManager playerManager;

        PlayerControls playerControls;

        [Header("Camera Inputs")]
        [SerializeField] Vector2 cameraInputConsole;
        [SerializeField] Vector2 cameraInputMouse;
        public float cameraHorizontalInput;
        public float cameraVerticalInput;

        [Header("Player Movement Inputs")]
        [SerializeField] Vector2 movementInput;
        [SerializeField] public float verticalInput;
        [SerializeField] public float horizontalInput;
        [SerializeField] public float moveAmount;

        [Header("Player Actions Input")]
        [SerializeField] bool dodgeInput = false;
        [SerializeField] bool sprintInput = false;
        [SerializeField] bool jumpInput = false;
        [SerializeField] bool manaDrainInput = false;

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

        private void Start()
        {
            DontDestroyOnLoad(gameObject);

            // Quando a cena mudar, roda esta logica
            SceneManager.activeSceneChanged += OnSceneChange;

            Instance.enabled = false;
        }

        private void OnSceneChange(Scene oldScene, Scene newScene)
        {
            // Se estivermos na cena do mundo, libera os controles do jogador
            if (newScene.buildIndex == WorldSaveGameManager.Instance.GetWorldSceneIndex())
            {
                Instance.enabled = true;
            }
            // Se nao, estamos no menu do jogo, por isso desativa os controles do jogador
            // Isso e para o plater nao se movimentar enquanto o jogador esta em algum menu de criacao ou inventario, etc
            else
            {
                Instance.enabled = false;
            }
        }

        private void OnEnable()
        {
            if (playerControls == null)
            {
                playerControls = new PlayerControls();

                #region Keyboard & Mouse Inputs

                // KEYBOARD MOVEMENT
                playerControls.PlayerMovement.MovementKeyboard.performed += i => movementInput = i.ReadValue<Vector2>();
                // MOUSE CAMERA MOVEMENT
                playerControls.PlayerCamera.CameraControlsMouse.performed += i => cameraInputMouse = i.ReadValue<Vector2>();
                // KEYBOARD DODGE
                playerControls.PlayerActions.DodgeKeyboard.performed += i => dodgeInput = true;
                // KEYBOARD JUMP
                playerControls.PlayerActions.JumpKeyboard.performed += i => jumpInput = true;
                // KEYBOARD SPRINT, Holding the input, set the bool to true
                playerControls.PlayerActions.SprintKeyboard.performed += i => sprintInput = true;
                playerControls.PlayerActions.SprintKeyboard.canceled += i => sprintInput = false;
                // KEYBOARD TEST LETTER K
                playerControls.PlayerActions.ManaDrainTest.performed += i => manaDrainInput = true;

                #endregion

                #region Console Inputs

                // CONSOLE MOVEMENT
                playerControls.PlayerMovement.MovementConsole.performed += i => movementInput = i.ReadValue<Vector2>();
                // CONSOLE CAMERA MOVEMENT
                playerControls.PlayerCamera.CameraControlsConsole.performed += i => cameraInputConsole = i.ReadValue<Vector2>();
                // CONSOLE DODGE
                playerControls.PlayerActions.DodgeConsole.performed += i => dodgeInput = true;
                // KEYBOARD JUMP
                playerControls.PlayerActions.JumpConsole.performed += i => jumpInput = true;
                // CONSOLE SPRINT, Holding the input, set the bool to true
                playerControls.PlayerActions.SprintConsole.performed += i => sprintInput = true;
                playerControls.PlayerActions.SprintConsole.canceled += i => sprintInput = false;

                #endregion
            }

            playerControls.Enable();
        }

        private void OnDestroy()
        {
            // Se nos destruirmos este objeto, para de utilizar o evento
            SceneManager.activeSceneChanged -= OnSceneChange;
        }

        private void OnApplicationFocus(bool focus)
        {
            if (enabled)
            {
                if (focus)
                {
                    playerControls.Enable();
                }
                else
                { 
                    playerControls.Disable();
                }
            }
        }

        private void Update()
        {
            HandleAllInputs();
        }

        private void HandleAllInputs()
        {
            HandlePlayerMovementInput();
            HandleCameraMovementInput();
            HandleDodgeInput();
            HandleSprintInput();
            HandleJumpInput();
            HandleJumpInput();
        }

        // Movement

        private void HandlePlayerMovementInput()
        {
            verticalInput = movementInput.y;
            horizontalInput = movementInput.x;

            // Retorna o valor absoluto do numero
            moveAmount = Mathf.Clamp01(Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput));

            if (moveAmount <= 0.5f && moveAmount > 0f)
            {
                moveAmount = 0.5f;
            }
            else if (moveAmount > 0.5f && moveAmount <= 1f)
            {
                moveAmount = 1;
            }

            // Why do we pass 0 on the horizontal? Because we only want non-strafing movement
            // We use the horizontal when we are straffing or locked on

            if (playerManager == null) return;

            // If we are not locked on, only use the move amount
            playerManager.playerAnimatorManager.UpdateAnimatorMovementParameters(0, moveAmount, playerManager.playerNetworkManager.isSprinting.Value);

            // If we are locked on, pass the horizontal movement as well
        }

        private void HandleCameraMovementInput()
        {
            if (playerControls.PlayerCamera.CameraControlsMouse.IsInProgress())
            {
                cameraVerticalInput = cameraInputMouse.y;
                cameraHorizontalInput = cameraInputMouse.x;
            }
            else if (playerControls.PlayerCamera.CameraControlsConsole.IsInProgress())
            {
                cameraVerticalInput = cameraInputConsole.y;
                cameraHorizontalInput = cameraInputConsole.x;
            }
        }

        // Actions

        private void HandleDodgeInput()
        {
            if (dodgeInput)
            { 
                dodgeInput = false;

                // Future Note: Return if menu or UI window is open
                // Perform dodge
                playerManager.playerLocomotionManager.AttemptToPerformDodge();
            }
        }

        private void HandleSprintInput()
        {
            if (sprintInput)
            {
                // Handle Sprinting
                playerManager.playerLocomotionManager.HandleSprinting();
            }
            else
            {
                playerManager.playerNetworkManager.isSprinting.Value = false;
            }
        }

        private void HandleJumpInput()
        {
            if (jumpInput)
            { 
                jumpInput = false;

                // If we have a ui window open, simply return without doing anything

                // Attemp to perform jump
                playerManager.playerLocomotionManager.AttemptToPerformJump();
            }
        }
    }
}