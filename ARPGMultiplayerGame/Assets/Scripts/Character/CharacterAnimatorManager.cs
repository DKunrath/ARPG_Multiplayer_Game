using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace DK
{
    public class CharacterAnimatorManager : MonoBehaviour
    {
        CharacterManager characterManager;

        int horizontal;
        int vertical;

        protected virtual void Awake()
        { 
            characterManager = GetComponent<CharacterManager>();

            horizontal = Animator.StringToHash("Horizontal");
            vertical = Animator.StringToHash("Vertical");
        }

        public void UpdateAnimatorMovementParameters(float horizontalValue, float verticalValue, bool isSprinting)
        {
            float horizontalAmount = horizontalValue;
            float verticalAmount = verticalValue;

            if (isSprinting)
            {
                verticalAmount = 2;
            }

            characterManager.animator.SetFloat(horizontal, horizontalAmount, 0.1f, Time.deltaTime);
            characterManager.animator.SetFloat(vertical, verticalAmount, 0.1f, Time.deltaTime);
        }

        public virtual void PlayTargetActionAnimation(
            string targetAnimation, 
            bool isPerformingAction, 
            bool applyRootMotion = true, 
            bool canRotate = false, 
            bool canMove = false)
        {
            characterManager.applyRootMotion = applyRootMotion;
            characterManager.animator.CrossFade(targetAnimation, 0.2f);
            // Can be used to stop character from attempting new actions
            characterManager.isPerformingAction = isPerformingAction;
            characterManager.canRotate = canRotate;
            characterManager.canMove = canMove;

            // Tell the server/host we played an animation, and to play that animation for everybody else present
            characterManager.characterNetworkManager.NotifyTheServerOfActionAnimationServerRpc(NetworkManager.Singleton.LocalClientId, targetAnimation, applyRootMotion);
        }
    }
}