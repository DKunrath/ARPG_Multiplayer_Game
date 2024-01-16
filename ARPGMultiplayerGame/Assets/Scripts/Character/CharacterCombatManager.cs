using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace DK
{
    public class CharacterCombatManager : NetworkBehaviour
    {
        CharacterManager characterManager;

        [Header("Attack Target")]
        public CharacterManager currentTarget;

        [Header("Attack Type")]
        public AttackType currentAttackType;

        [Header("Lock On Transform")]
        public Transform lockOnTransform;

        protected virtual void Awake()
        { 
            characterManager = GetComponent<CharacterManager>();
        }

        public virtual void SetTarget(CharacterManager newTarget)
        {
            if (characterManager.IsOwner)
            {
                if (newTarget != null)
                {
                    currentTarget = newTarget;
                    // Tell the network we have a target, and tell the network who it is
                    characterManager.characterNetworkManager.currentTargetNetworkObjectID.Value = newTarget.GetComponent<NetworkObject>().NetworkObjectId;
                }
                else
                {
                    currentTarget = null;
                }
            }
        }
    }
}