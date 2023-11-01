using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace DK
{
    public class CharacterManager : NetworkBehaviour
    {
        [SerializeField] public CharacterController characterController;
        [SerializeField] public Animator animator;
        [SerializeField] public CharacterNetworkManager characterNetworkManager;

        [Header("Flags")]
        public bool isPerformingAction = false;
        public bool isJumping = false;
        public bool isGrounded = true;
        public bool applyRootMotion = false;
        public bool canRotate = true;
        public bool canMove = true;

        protected virtual void Awake()
        {
            DontDestroyOnLoad(this);

            //characterNetworkManager = GetComponent<CharacterNetworkManager>();
        }

        protected virtual void Update()
        {
            animator.SetBool("IsGrounded", isGrounded);

            // If this character is being controlled from our side, then assign its network position and rotation to the position and rotation of our transform
            if (IsOwner)
            {
                characterNetworkManager.networkPosition.Value = transform.position;
                characterNetworkManager.networkRotation.Value = transform.rotation;
            }
            // If this character is being controlled from else where, then assign its position and rotation here locally by the position and rotation of its network transform 
            else
            {
                // Position
                transform.position = Vector3.SmoothDamp(transform.position, 
                    characterNetworkManager.networkPosition.Value, 
                    ref characterNetworkManager.networkPositionVelocity, 
                    characterNetworkManager.networkPositionSmoothTime);
                // Rotation
                transform.rotation = Quaternion.Slerp(transform.rotation, 
                    characterNetworkManager.networkRotation.Value, 
                    characterNetworkManager.networkRotationSmoothTime);
            }
        }

        protected virtual void LateUpdate()
        { 
            
        }
    }
}
