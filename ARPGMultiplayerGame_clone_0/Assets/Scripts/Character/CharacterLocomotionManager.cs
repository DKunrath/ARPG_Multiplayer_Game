using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DK
{
    public class CharacterLocomotionManager : MonoBehaviour
    {
        CharacterManager characterManager;

        [Header("Ground Check & Jumping")]
        [SerializeField] protected float gravityForce = -5.55f;
        [SerializeField] LayerMask groundLayer;
        [SerializeField] float groundCheckSphereRadius = 0.35f;
        [SerializeField] protected Vector3 yVelocity; // This is the force at which our character is pulled up or down (Jumping or Falling)
        [SerializeField] protected float groundedYVelocity = -20f; // The force at which our character is sticking to the ground whilst they are grounded
        [SerializeField] protected float fallStartYVelocity = -5f; // The force at which our character begins to fall when they become ungrounded (rises as they fall longer)
        protected bool fallingVelocityHasBeenSet = false;
        protected float inAirTimer = 0f;

        [Header("Flags")]
        public bool isRolling = false;

        protected virtual void Awake()
        { 
            characterManager = GetComponent<CharacterManager>();
        }

        protected virtual void Update()
        { 
            HandleGroundCheck();

            if (characterManager.isGrounded)
            {
                // If we are not attempting to jump or move upward
                if (yVelocity.y < 0)
                {
                    inAirTimer = 0f;
                    fallingVelocityHasBeenSet = false;
                    yVelocity.y = groundedYVelocity;
                }
            }
            else
            {
                // If we are not jumping, and our falling velocity has not been set
                if (!characterManager.characterNetworkManager.isJumping.Value && !fallingVelocityHasBeenSet)
                {
                    fallingVelocityHasBeenSet = true;
                    yVelocity.y = fallStartYVelocity;
                }

                inAirTimer += Time.deltaTime;

                yVelocity.y += gravityForce * Time.deltaTime;
            }

            characterManager.animator.SetFloat("InAirTimer", inAirTimer);
            // There should always be some force applied to the y velocity
            characterManager.characterController.Move(yVelocity * Time.deltaTime);
        }

        protected void HandleGroundCheck()
        {
            characterManager.isGrounded = Physics.CheckSphere(characterManager.transform.position, groundCheckSphereRadius, groundLayer);
        }

        // Draws our ground check sphere in the scene view
        protected void OnDrawGizmosSelected()
        {
            //Gizmos.DrawSphere(characterManager.transform.position, groundCheckSphereRadius);
        }
    }
}