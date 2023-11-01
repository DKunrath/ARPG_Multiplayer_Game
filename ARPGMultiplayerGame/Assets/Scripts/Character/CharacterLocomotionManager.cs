using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DK
{
    public class CharacterLocomotionManager : MonoBehaviour
    {
        CharacterManager characterManager;

        [Header("Ground Check & Jumping")]
        [SerializeField] LayerMask groundLayer;
        [SerializeField] float groundCheckSphereRadius = 0.35f;
        [SerializeField] protected Vector3 yVelocity; // This is the force at which our character is pulled up or down (Jumping or Falling)
        [SerializeField] protected float groundedYVelocity = -20; // The force at which our character is sticking to the ground whilst they are grounded
        [SerializeField] protected float fallStartYVelocity = -5; // The force at which our character begins to fall when they become ungrounded (rises as they fall longer)
        protected bool fallingVelocityHasBeenSet = false;
        protected float inAirTimer = 0;

        protected virtual void Awake()
        { 
            characterManager = GetComponent<CharacterManager>();
        }

        protected virtual void Update()
        { 
            HandleGroundCheck();
        }

        protected void HandleGroundCheck()
        {
            characterManager.isGrounded = Physics.CheckSphere(characterManager.transform.position, groundCheckSphereRadius, groundLayer);
        }

        // Draws our ground check sphere in the scene view
        protected void OnDrawGizmosSelected()
        {
            Gizmos.DrawSphere(characterManager.transform.position, groundCheckSphereRadius);
        }
    }
}