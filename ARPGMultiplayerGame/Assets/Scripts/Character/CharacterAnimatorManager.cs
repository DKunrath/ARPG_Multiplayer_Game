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

        [Header("Damage Animations")]
        public string lastDamageAnimationPlayed;

        [SerializeField] string hit_Forward_Medium_01 = "Hit_Forward_Medium_01";
        [SerializeField] string hit_Forward_Medium_02 = "Hit_Forward_Medium_02";

        [SerializeField] string hit_Backward_Medium_01 = "Hit_Backward_Medium_01";
        [SerializeField] string hit_Backward_Medium_02 = "Hit_Backward_Medium_02";

        [SerializeField] string hit_Left_Medium_01 = "Hit_Left_Medium_01";
        [SerializeField] string hit_Left_Medium_02 = "Hit_Left_Medium_02";

        [SerializeField] string hit_Right_Medium_01 = "Hit_Right_Medium_01";
        [SerializeField] string hit_Right_Medium_02 = "Hit_Right_Medium_02";

        public List<string> forward_Medium_Damage = new List<string>();
        public List<string> backward_Medium_Damage = new List<string>();
        public List<string> left_Medium_Damage = new List<string>();
        public List<string> right_Medium_Damage = new List<string>();
        
        protected virtual void Awake()
        { 
            characterManager = GetComponent<CharacterManager>();

            horizontal = Animator.StringToHash("Horizontal");
            vertical = Animator.StringToHash("Vertical");
        }

        protected virtual void Start()
        {
            forward_Medium_Damage.Add(hit_Forward_Medium_01);
            forward_Medium_Damage.Add(hit_Forward_Medium_02);

            backward_Medium_Damage.Add(hit_Backward_Medium_01);
            backward_Medium_Damage.Add(hit_Backward_Medium_02);

            left_Medium_Damage.Add(hit_Left_Medium_01);
            left_Medium_Damage.Add(hit_Left_Medium_02);

            right_Medium_Damage.Add(hit_Right_Medium_01);
            right_Medium_Damage.Add(hit_Right_Medium_02);
        }

        public string GetRandomAnimationFromList(List<string> animationList)
        { 
            List<string> finalList = new List<string>();

            foreach (var item in animationList) 
            { 
                finalList.Add(item);
            }

            // Check if we have already played this damage animation so it doesnt repeat
            finalList.Remove(lastDamageAnimationPlayed);

            // Check the list for null entries, and remove them
            for (int i = finalList.Count - 1; i > -1; i--)
            {
                if (finalList[i] == null)
                {
                    finalList.RemoveAt(i);
                }
            }
            int randomValue = Random.Range(0, finalList.Count);

            return finalList[randomValue];
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
            Debug.Log("PLAYING ANIMATION : " + targetAnimation);

            characterManager.applyRootMotion = applyRootMotion;
            characterManager.animator.CrossFade(targetAnimation, 0.2f);
            // Can be used to stop character from attempting new actions
            characterManager.isPerformingAction = isPerformingAction;
            characterManager.canRotate = canRotate;
            characterManager.canMove = canMove;

            // Tell the server/host we played an animation, and to play that animation for everybody else present
            characterManager.characterNetworkManager.NotifyTheServerOfActionAnimationServerRpc(NetworkManager.Singleton.LocalClientId, targetAnimation, applyRootMotion);
        }

        public virtual void PlayTargetAttackActionAnimation(
            AttackType attackType,
            string targetAnimation,
            bool isPerformingAction,
            bool applyRootMotion = true,
            bool canRotate = false,
            bool canMove = false)
        {
            // Keep track of last attack performed (for combos)
            // Keep track of current attack type (Light, Heavy, Spell, ETC)
            // Update animation set to current weapons animations
            // Decide if our attack can be parried
            // Tell the network our "isAttacking" flag is active (for counter damage, etc)
            characterManager.characterCombatManager.currentAttackType = attackType;
            characterManager.applyRootMotion = applyRootMotion;
            characterManager.animator.CrossFade(targetAnimation, 0.2f);
            characterManager.isPerformingAction = isPerformingAction;
            characterManager.canRotate = canRotate;
            characterManager.canMove = canMove;

            // Tell the server/host we played an animation, and to play that animation for everybody else present
            characterManager.characterNetworkManager.NotifyTheServerOfAttackActionAnimationServerRpc(NetworkManager.Singleton.LocalClientId, targetAnimation, applyRootMotion);
        }
    }
}