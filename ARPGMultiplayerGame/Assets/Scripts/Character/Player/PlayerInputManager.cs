using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.DefaultInputActions;

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

        [Header("Camera Lock On Input")]
        [SerializeField] bool lock_On_Input = false;
        private Coroutine lockOnCoroutine;

        [Header("Camera Zoom")]
        [SerializeField] private float zoom = 60f;
        [SerializeField] public float zoomChangeAmount = 60f;

        [Header("Player Movement Inputs")]
        [SerializeField] Vector2 movement_Input;
        [SerializeField] public float vertical_Input;
        [SerializeField] public float horizontal_Input;
        [SerializeField] public float moveAmount;

        [Header("Player Actions Input")]
        [SerializeField] bool dodge_Input = false;
        [SerializeField] bool sprint_Input = false;
        [SerializeField] bool jump_Input = false;
        [SerializeField] bool RB_and_LMB_Input = false;
        [SerializeField] bool RT_and_RMB_Input = false;
        [SerializeField] bool ALT_Input = false;

        [SerializeField] bool testKey = false;

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

            if (playerControls != null)
            {
                playerControls.Disable();
            }
        }

        private void OnSceneChange(Scene oldScene, Scene newScene)
        {
            // Se estivermos na cena do mundo, libera os controles do jogador
            if (newScene.buildIndex == WorldSaveGameManager.Instance.GetWorldSceneIndex())
            {
                Instance.enabled = true;

                if (playerControls != null)
                {
                    playerControls.Enable();
                }
            }
            // Se nao, estamos no menu do jogo, por isso desativa os controles do jogador
            // Isso e para o plater nao se movimentar enquanto o jogador esta em algum menu de criacao ou inventario, etc
            else
            {
                Instance.enabled = false;

                if (playerControls != null)
                {
                    playerControls.Disable();
                }
            }
        }

        private void OnEnable()
        {
            if (playerControls == null)
            {
                playerControls = new PlayerControls();

                #region Keyboard & Mouse Inputs

                #region Player and Camera Movement

                // KEYBOARD MOVEMENT
                playerControls.PlayerMovement.MovementKeyboard.performed += i => movement_Input = i.ReadValue<Vector2>();
                // MOUSE CAMERA MOVEMENT
                playerControls.PlayerCamera.CameraControlsMouse.performed += i => cameraInputMouse = i.ReadValue<Vector2>();
                // KEYBOARD LOCK ON CAMERA
                playerControls.PlayerActions.LockOnKeyboard.performed += i => lock_On_Input = true;

                #endregion
                
                #region Player Movement Actions
                // KEYBOARD DODGE
                playerControls.PlayerActions.DodgeKeyboard.performed += i => dodge_Input = true;
                // KEYBOARD JUMP
                playerControls.PlayerActions.JumpKeyboard.performed += i => jump_Input = true;
                // KEYBOARD SPRINT, Holding the input, set the bool to true
                playerControls.PlayerActions.SprintKeyboard.performed += i => sprint_Input = true;
                playerControls.PlayerActions.SprintKeyboard.canceled += i => sprint_Input = false;
                #endregion

                #region Player Attacks
                // KEYBOARD Left Mouse Button Input for weapon Attack Action
                playerControls.PlayerActions.LMBAttackKeyboard.performed += i => RB_and_LMB_Input = true;
                // KEYBOARD Right Mouse Button Input for weapon Heavy Attack Action
                playerControls.PlayerActions.RMBAttackKeyboard.performed += i => RT_and_RMB_Input = true;

                #endregion
                // KEYBOARD TEST LETTER K
                playerControls.PlayerActions.ManaDrainTest.performed += i => testKey = true;

                #endregion

                #region Console Inputs

                // CONSOLE MOVEMENT
                playerControls.PlayerMovement.MovementConsole.performed += i => movement_Input = i.ReadValue<Vector2>();
                // CONSOLE CAMERA MOVEMENT
                playerControls.PlayerCamera.CameraControlsConsole.performed += i => cameraInputConsole = i.ReadValue<Vector2>();
                // CONSOLE LOCK ON CAMERA
                playerControls.PlayerActions.LockOnConsole.performed += i => lock_On_Input = true;
                // CONSOLE DODGE
                playerControls.PlayerActions.DodgeConsole.performed += i => dodge_Input = true;
                // KEYBOARD JUMP
                playerControls.PlayerActions.JumpConsole.performed += i => jump_Input = true;
                // CONSOLE SPRINT, Holding the input, set the bool to true
                playerControls.PlayerActions.SprintConsole.performed += i => sprint_Input = true;
                playerControls.PlayerActions.SprintConsole.canceled += i => sprint_Input = false;
                // CONSOLE RB Input for weapon Attack Action
                playerControls.PlayerActions.RBAttackConsole.performed += i => RB_and_LMB_Input = true;
                // CONSOLE RT Input for weapon Heavy Attack Action
                playerControls.PlayerActions.RTHeavyAttackConsole.performed += i => RT_and_RMB_Input = true;

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
            HandleLockOnInput();
            HandleLockOnSwitchTargetInput();
            HandlePlayerMovementInput();
            HandleCameraMovementInput();
            HandleDodgeInput();
            HandleSprintInput();
            HandleJumpInput();
            HandleRBandLMBInput();
            HandleRTandRMBInput();
        }

        // Lock On
        private void HandleLockOnInput()
        {
            if (playerManager.playerNetworkManager.isLockedOn.Value)
            {
                if (playerManager.playerCombatManager.currentTarget == null) return;
                
                if (playerManager.playerCombatManager.currentTarget.isDead.Value)
                {
                    lockOnCoroutine = StartCoroutine(PlayerCamera.Instance.WaitThenFindNewTarget());
                    playerManager.playerNetworkManager.isLockedOn.Value = false;
                }

                // Attempt to find new target or unlock completly

                // This assures us that the coroutine never runs multiple times overlapping itself
                if (lockOnCoroutine != null)
                { 
                    StopCoroutine(lockOnCoroutine);
                }
            }

            if (lock_On_Input && playerManager.playerNetworkManager.isLockedOn.Value)
            { 
                lock_On_Input = false;
                PlayerCamera.Instance.ClearLockOnTargets();
                playerManager.playerNetworkManager.isLockedOn.Value = false;
                // Disable lock on
                return;
            }

            if (lock_On_Input && !playerManager.playerNetworkManager.isLockedOn.Value)
            {
                lock_On_Input = false;

                // If we are aiming using ranged weapons, return (DO NOT ALLOW LOCK WHILST AIMING)

                // Enable lock on
                PlayerCamera.Instance.HandleLocatingLockOnTargets();

                if (PlayerCamera.Instance.nearestLockOnTarget != null)
                {
                    // Set the target as our current target
                    playerManager.playerCombatManager.SetTarget(PlayerCamera.Instance.nearestLockOnTarget);
                    playerManager.playerNetworkManager.isLockedOn.Value = true;
                }
            }
        }

        private void HandleLockOnSwitchTargetInput()
        {
            if (playerManager.playerNetworkManager.isLockedOn.Value)
            {
                if (Input.mouseScrollDelta.y > 0)
                {
                    PlayerCamera.Instance.HandleLocatingLockOnTargets();

                    if (PlayerCamera.Instance.leftLockOnTarget != null)
                    {
                        playerManager.playerCombatManager.SetTarget(PlayerCamera.Instance.leftLockOnTarget);
                    }
                }

                if (Input.mouseScrollDelta.y < 0)
                {
                    PlayerCamera.Instance.HandleLocatingLockOnTargets();

                    if (PlayerCamera.Instance.rightLockOnTarget != null)
                    {
                        playerManager.playerCombatManager.SetTarget(PlayerCamera.Instance.rightLockOnTarget);
                    }
                }
            }
            else
            {
                // ZOOM IN
                if (Input.mouseScrollDelta.y > 0)
                {
                    if (PlayerCamera.Instance.cameraObject.fieldOfView <= 30)
                    {
                        PlayerCamera.Instance.cameraObject.fieldOfView = 30;
                        return;
                    }

                    PlayerCamera.Instance.cameraObject.fieldOfView -= 1;
                }

                if (Input.mouseScrollDelta.y < 0)
                {
                    // ZOOM OUT
                    if (PlayerCamera.Instance.cameraObject.fieldOfView >= 90)
                    {
                        PlayerCamera.Instance.cameraObject.fieldOfView = 90;
                        return;
                    }

                    PlayerCamera.Instance.cameraObject.fieldOfView += 1;
                }
            }
        }

        // Movement

        private void HandlePlayerMovementInput()
        {
            vertical_Input = movement_Input.y;
            horizontal_Input = movement_Input.x;

            // Retorna o valor absoluto do numero
            moveAmount = Mathf.Clamp01(Mathf.Abs(vertical_Input) + Mathf.Abs(horizontal_Input));

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
            if (!playerManager.playerNetworkManager.isLockedOn.Value || playerManager.playerNetworkManager.isSprinting.Value)
            {
                playerManager.playerAnimatorManager.UpdateAnimatorMovementParameters(0, moveAmount, playerManager.playerNetworkManager.isSprinting.Value);
            }
            // If we are locked on, pass the horizontal movement as well
            else
            {
                playerManager.playerAnimatorManager.UpdateAnimatorMovementParameters(horizontal_Input, vertical_Input, playerManager.playerNetworkManager.isSprinting.Value);
            }
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
            if (dodge_Input)
            { 
                dodge_Input = false;

                // Future Note: Return if menu or UI window is open
                // Perform dodge
                playerManager.playerLocomotionManager.AttemptToPerformDodge();
            }
        }

        private void HandleSprintInput()
        {
            if (sprint_Input)
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
            if (jump_Input)
            { 
                jump_Input = false;

                // If we have a ui window open, simply return without doing anything

                // Attemp to perform jump
                playerManager.playerLocomotionManager.AttemptToPerformJump();
            }
        }

        private void HandleRBandLMBInput()
        { 
            if(RB_and_LMB_Input)
            {
                RB_and_LMB_Input = false;

                // TO DO: If we have a UI window open, return and do nothing

                playerManager.playerNetworkManager.SetCharacterActionHand(true);

                // TO DO: If we are two handing the weapon, use the two handed action

                playerManager.playerCombatManager.PerformWeaponBasedAction(playerManager.playerInventoryManager.currentRightHandWeapon.oh_RB_and_LMB_Action, playerManager.playerInventoryManager.currentRightHandWeapon);
            }
        }

        private void HandleRTandRMBInput()
        {
            if (RT_and_RMB_Input)
            {
                Debug.Log("ATAQUE FORTE");
                RT_and_RMB_Input = false;

                // TO DO: If we have a UI window open, return and do nothing

                playerManager.playerNetworkManager.SetCharacterActionHand(true);

                // TO DO: If we are two handing the weapon, use the two handed action

                playerManager.playerCombatManager.PerformWeaponBasedAction(playerManager.playerInventoryManager.currentRightHandWeapon.oh_RT_and_RMB_Action, playerManager.playerInventoryManager.currentRightHandWeapon);
            }
        }
    }
}