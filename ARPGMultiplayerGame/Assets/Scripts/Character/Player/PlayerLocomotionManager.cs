using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DK
{
    public class PlayerLocomotionManager : CharacterLocomotionManager
    {
        [SerializeField] private PlayerManager player;

        [HideInInspector] public float verticalMovement;
        [HideInInspector] public float horizontalMovement;
        [HideInInspector] public float moveAmount;

        [Header("Movement Settings")]
        private Vector3 moveDirection;
        private Vector3 targetRotationDirection;
        [SerializeField] float walkingSpeed = 2f;
        [SerializeField] float runningSpeed = 5f;
        [SerializeField] float sprintingSpeed = 8f;
        [SerializeField] float rotationSpeed = 15f;

        [Header("Dodge Settings")]
        private Vector3 rollDirection;

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Update()
        {
            base.Update();

            if (player.IsOwner)
            {
                player.characterNetworkManager.verticalMovement.Value = verticalMovement;
                player.characterNetworkManager.horizontalMovement.Value = horizontalMovement;
                player.characterNetworkManager.networkMoveAmount.Value = moveAmount;
            }
            else
            {
                verticalMovement = player.characterNetworkManager.verticalMovement.Value;
                horizontalMovement = player.characterNetworkManager.horizontalMovement.Value;
                moveAmount = player.characterNetworkManager.networkMoveAmount.Value;

                // If not locked on, pass move amount
                player.playerAnimatorManager.UpdateAnimatorMovementParameters(0, moveAmount, player.playerNetworkManager.isSprinting.Value);

                // If locked on, pass horizontal and vertical
            }
        }

        public void HandleAllMovement()
        {
            // Grounded Movement
            HandleGroundedMovement();
            // Jumping Movement

            // Rotation
            HandleRotation();
            // Falling
        }

        private void GetVerticalAndHorizontalInputs()
        {
            verticalMovement = PlayerInputManager.Instance.verticalInput;
            horizontalMovement = PlayerInputManager.Instance.horizontalInput;
            moveAmount = PlayerInputManager.Instance.moveAmount;

            // Clamp the movements
        }

        private void HandleGroundedMovement()
        {
            if (!player.canMove) return;

            GetVerticalAndHorizontalInputs();

            // Nossa direcao de movimento e baseada onde nossa camerda esta focada e nos inputs de movimento
            moveDirection = PlayerCamera.Instance.transform.forward * verticalMovement;
            moveDirection = moveDirection + PlayerCamera.Instance.transform.right * horizontalMovement;
            moveDirection.Normalize();
            moveDirection.y = 0;

            if (player.playerNetworkManager.isSprinting.Value)
            {
                player.characterController.Move(moveDirection * sprintingSpeed * Time.deltaTime);
            }
            else
            {
                if (PlayerInputManager.Instance.moveAmount > 0.5f)
                {
                    // Move at running speed
                    player.characterController.Move(moveDirection * runningSpeed * Time.deltaTime);

                }
                else if (PlayerInputManager.Instance.moveAmount <= 0.5)
                {
                    // Move at walking speed
                    player.characterController.Move(moveDirection * walkingSpeed * Time.deltaTime);
                }
            }
        }

        private void HandleRotation()
        {
            if (!player.canRotate) return;

            targetRotationDirection = Vector3.zero;
            targetRotationDirection = PlayerCamera.Instance.cameraObject.transform.forward * verticalMovement;
            targetRotationDirection = targetRotationDirection + PlayerCamera.Instance.cameraObject.transform.right * horizontalMovement;
            targetRotationDirection.Normalize();
            targetRotationDirection.y = 0;

            if (targetRotationDirection == Vector3.zero)
            {
                targetRotationDirection = transform.forward;
            }

            Quaternion newRotation = Quaternion.LookRotation(targetRotationDirection);
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = targetRotation;
        }

        public void HandleSprinting()
        {
            if (player.isPerformingAction)
            {
                // Set sprinting to false
                player.playerNetworkManager.isSprinting.Value = false;
            }

            // If we are out of stamina, set sprinting to false - Im not going to use Stamina in this game, only health and mana

            // If we are moving, set sprinting to true
            if (moveAmount >= 0.5)
            {
                player.playerNetworkManager.isSprinting.Value = true;
            }
            // If we are stationary, set sprinting to false
            else
            {
                player.playerNetworkManager.isSprinting.Value = false;
            }
        }

        public void AttemptToPerformDodge()
        {
            if (player.isPerformingAction) return;

            // If we are moving when we attempt to dodge, we perform a roll
            if (PlayerInputManager.Instance.moveAmount > 0)
            {
                rollDirection = PlayerCamera.Instance.cameraObject.transform.forward * PlayerInputManager.Instance.verticalInput;
                rollDirection += PlayerCamera.Instance.cameraObject.transform.right * PlayerInputManager.Instance.horizontalInput;
                rollDirection.y = 0;
                rollDirection.Normalize();

                Quaternion playerRotation = Quaternion.LookRotation(rollDirection);
                player.transform.rotation = playerRotation;

                player.playerAnimatorManager.PlayTargetActionAnimation("Roll_Forward_01", true, true);
            }
            // If we are not moving, we perform a backstep
            else
            {
                // Perform a backstep animation
                player.playerAnimatorManager.PlayTargetActionAnimation("OH_Backstep_01", true, true);
            }
        }

        public void AttemptToPerformJump()
        {
            // If we are performing a general action, we do not wanto to allow a jump (will change when combat is added)
            if (player.isPerformingAction) return;

            // If we are already in a jump, we do not want to allow a jump again until the current jump has finished
            if (player.isJumping) return;

            // If we are not grounded, we do not want to allow a jump
            if (!player.isGrounded) return;

            // If we are two handing our weapon, play the two handed jump animation, otherwise play the one handed animation (TO DO)
            player.playerAnimatorManager.PlayTargetActionAnimation("Main_Jump_Start", false);

            player.isJumping = true;
        }

        public void ApplyJumpingVelocity()
        { 
            // Apply an upward velocity depending on forces in our game
        }
    }
}