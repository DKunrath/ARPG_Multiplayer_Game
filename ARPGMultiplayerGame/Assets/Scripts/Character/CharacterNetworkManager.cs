using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace DK
{
    public class CharacterNetworkManager : NetworkBehaviour
    {
        CharacterManager characterManager;

        [Header("Position")]
        public NetworkVariable<Vector3> networkPosition = new NetworkVariable<Vector3>(Vector3.zero, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public Vector3 networkPositionVelocity;
        public float networkPositionSmoothTime = 0.1f;
        [Header("Rotation")]
        public NetworkVariable<Quaternion> networkRotation = new NetworkVariable<Quaternion>(Quaternion.identity, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public float networkRotationSmoothTime = 0.1f;
        [Header("Animator")]
        public NetworkVariable<float> horizontalMovement = new NetworkVariable<float>(0f, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<float> verticalMovement = new NetworkVariable<float>(0f, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<float> networkMoveAmount = new NetworkVariable<float>(0f, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        [Header("Flags")]
        public NetworkVariable<bool> isSprinting = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        [Header("Stats")]
        public NetworkVariable<int> intelligence = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<int> currentMana = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<int> maxMana = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        protected virtual void Awake()
        { 
            characterManager = GetComponent<CharacterManager>();
        }

        // A server rpc is a function called from a client, to the server (in our case, the host)
        [ServerRpc]
        public void NotifyTheServerOfActionAnimationServerRpc(ulong clientID, string animationID, bool applyRootMotion)
        {
            // If this character is the host/server, then activate the client rpc
            if (IsServer)
            {
                PlayActionAnimationForAllClientesClientRpc(clientID, animationID, applyRootMotion);
            }
        }

        // A client rpc is sent to all clients present, from the server 
        [ClientRpc]
        public void PlayActionAnimationForAllClientesClientRpc(ulong clientID, string animationID, bool applyRootMotion)
        {
            // We make sure to not run the function on the character who sent it (so we dont play the animation twice)
            if (clientID != NetworkManager.Singleton.LocalClientId)
            {
                PerformActionAnimationFromServer(animationID, applyRootMotion);
            }
        }

        private void PerformActionAnimationFromServer(string animationID, bool applyRootMotion)
        {
            characterManager.applyRootMotion = applyRootMotion;
            characterManager.animator.CrossFade(animationID, 0.2f);
        }
    }
}