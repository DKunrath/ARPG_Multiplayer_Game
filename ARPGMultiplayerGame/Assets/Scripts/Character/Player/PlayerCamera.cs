using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DK
{
    public class PlayerCamera : MonoBehaviour
    {
        public static PlayerCamera Instance;

        [SerializeField] public PlayerManager playerManager;
        [SerializeField] Transform cameraPivotTransform;

        public Camera cameraObject;

        // Change these settings to change the way how the camera interacts with the performance
        [Header("Camera Settings")]
        private float cameraSmoothSpeed = 1; // The biggest this number, the longer for the camera to reach its position during movement
        [SerializeField] private float leftAndRightRotationSpeed = 30;
        [SerializeField] private float upAndRightRotationSpeed = 15;
        [SerializeField] private float minimumPivot = -30; // The lowest angle we can look down
        [SerializeField] private float maximumPivot = 60; // The highest angle we can look up
        [SerializeField] private float cameraCollisionRadius = 0.2f;
        [SerializeField] private LayerMask collideWithLayers;

        [Header("Camera Values")]
        private Vector3 cameraVelocity;
        private Vector3 cameraObjectPosition; // Used for camera collisions (moves the camera object to this position)
        [SerializeField] float leftAndRightLookAngle;
        [SerializeField] float upAndDownLookAngle;
        private float cameraZPosition; // Values used for camera collisions
        private float targetCameraZPosition; // Values used for camera collisions

        [Header("Lock On")]
        [SerializeField] private float lockOnRadius = 20;
        [SerializeField] private float minimumViewableAngle = -50;
        [SerializeField] private float maximumViewableAngle = 50;
        [SerializeField] float lockOnTargetFollowSpeed = 0.2f;
        [SerializeField] float setCameraHeightSpeed = 0.05f;
        [SerializeField] float unlockedCameraHeight = 1.65f;
        [SerializeField] float lockedCameraHeight = 2.2f;
        private Coroutine cameraLockOnHeightCoroutine;
        private List<CharacterManager> availableTargets = new List<CharacterManager>();
        public CharacterManager nearestLockOnTarget;
        public CharacterManager leftLockOnTarget;
        public CharacterManager rightLockOnTarget;

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
            cameraZPosition = cameraObject.transform.localPosition.z;
        }

        public void HandleAllCameraActions()
        {
            if (playerManager != null)
            {
                // Follow the player
                HandleFollowPlayerTarget();
                // Rotate around the player
                HandleCameraRotations();
                // Collide with objets
                HandleCameraCollisions();
                // Lock On Camera
                //HandleLocatingLockOnTargets();
            }
        }

        private void HandleFollowPlayerTarget()
        {
            Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.position, playerManager.transform.position, ref cameraVelocity, cameraSmoothSpeed * Time.deltaTime);
            transform.position = targetCameraPosition;
        }

        private void HandleCameraRotations()
        {
            // If locked on, force rotation towards lock on target
            if (playerManager.playerNetworkManager.isLockedOn.Value)
            {
                // Main player camera object, this rotates this gameobject
                Vector3 rotationDirection = playerManager.playerCombatManager.currentTarget.characterCombatManager.lockOnTransform.position - transform.position;
                rotationDirection.Normalize();
                rotationDirection.y = 0;

                Quaternion targetRotation = Quaternion.LookRotation(rotationDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lockOnTargetFollowSpeed);

                // This rotates the pivot object
                rotationDirection = playerManager.playerCombatManager.currentTarget.characterCombatManager.lockOnTransform.position - cameraPivotTransform.position;
                rotationDirection.Normalize();

                targetRotation = Quaternion.LookRotation(rotationDirection);
                cameraPivotTransform.transform.rotation = Quaternion.Slerp(cameraPivotTransform.rotation, targetRotation, lockOnTargetFollowSpeed);

                // Save our rotations to our look angles, so when we unlock, it doesent snap too far away
                leftAndRightLookAngle = transform.eulerAngles.y;
                upAndDownLookAngle = transform.eulerAngles.x;
            }
            // Else totate normally
            else
            {
                // Normal Rotations
                // Rotate left and right based on horizontal movement on mouse or gamepad
                leftAndRightLookAngle += (PlayerInputManager.Instance.cameraHorizontalInput * leftAndRightRotationSpeed) * Time.deltaTime;
                // Rotate up and down based on vertical movement on mouse or gamepad
                upAndDownLookAngle -= (PlayerInputManager.Instance.cameraVerticalInput * upAndRightRotationSpeed) * Time.deltaTime;
                // Clamp the up and down look angle between a minimum and maximum look angle
                upAndDownLookAngle = Mathf.Clamp(upAndDownLookAngle, minimumPivot, maximumPivot);

                Vector3 cameraRotation = Vector3.zero;
                Quaternion targetRotation;

                // Rotate this gameobject left and right
                cameraRotation.y = leftAndRightLookAngle;
                targetRotation = Quaternion.Euler(cameraRotation);
                transform.rotation = targetRotation;

                // Rotate this gameobject up and down
                cameraRotation = Vector3.zero;
                cameraRotation.x = upAndDownLookAngle;
                targetRotation = Quaternion.Euler(cameraRotation);
                cameraPivotTransform.localRotation = targetRotation;
            }
        }

        private void HandleCameraCollisions()
        {
            targetCameraZPosition = cameraZPosition;

            RaycastHit hit;
            Vector3 direction = cameraObject.transform.position - cameraPivotTransform.position;
            direction.Normalize();

            // We check if there is an object in front of the camera
            if (Physics.SphereCast(cameraPivotTransform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetCameraZPosition), collideWithLayers))
            {
                // If there is, we get our distance from it
                float distanceFromHitObject = Vector3.Distance(cameraPivotTransform.position, hit.point);
                // We then equate our target z position to the following
                targetCameraZPosition = -(distanceFromHitObject - cameraCollisionRadius);
            }

            // If our target position is less than our collision radius, we subtract our collision radius
            if (Mathf.Abs(targetCameraZPosition) < cameraCollisionRadius)
            { 
                targetCameraZPosition = -cameraCollisionRadius;
            }

            // We then apply our final position using a lerp over a time of 0.2 s
            cameraObjectPosition.z = Mathf.Lerp(cameraObject.transform.localPosition.z, targetCameraZPosition, 0.2f);
            cameraObject.transform.localPosition = cameraObjectPosition;
        }

        public void HandleLocatingLockOnTargets()
        {
            float shortestDistance = Mathf.Infinity; // Will be used to determine the target closest to us
            float shortestDistanceOfRightTarget = Mathf.Infinity; // Will be used to determine the shortest distance on one axis to the right of current target (+)
            float shortestDistanceOfLeftTarget = -Mathf.Infinity; // Will be used to determine the shortest distance on one axis to the left of current target (-)

            // Using a layermask of just only players
            Collider[] colliders = Physics.OverlapSphere(playerManager.transform.position, lockOnRadius, WorldUtilityManager.Instance.GetCharacterLayers());

            for (int i = 0; i < colliders.Length; i++)
            {
                CharacterManager lockOnTarget = colliders[i].GetComponent<CharacterManager>();

                if (lockOnTarget != null)
                {
                    // Check if they are within our field of view
                    Vector3 lockOnTargetsDirection = lockOnTarget.transform.position - playerManager.transform.position;
                    float distanceFromTarget = Vector3.Distance(playerManager.transform.position, lockOnTarget.transform.position);
                    float viewableAngle = Vector3.Angle(lockOnTargetsDirection, cameraObject.transform.forward);

                    // If our target is dead, check the next potential target
                    if (lockOnTarget.isDead.Value) continue;

                    // If our target is us, check the next potential target
                    if (lockOnTarget.transform.root == playerManager.transform.root) continue;

                    // Lastly, if the target is outside field of view or is blocked by environment, check next potential target
                    if (viewableAngle > minimumViewableAngle && viewableAngle < maximumViewableAngle)
                    {
                        RaycastHit hit;

                        if (Physics.Linecast(playerManager.playerCombatManager.lockOnTransform.position, lockOnTarget.characterCombatManager.lockOnTransform.position, out hit, WorldUtilityManager.Instance.GetEnviroLayers()))
                        {
                            // We hit something, we cannot see our lock on target
                            continue;
                        }
                        else
                        {
                            // Otherwise, add them to potential targets list
                            availableTargets.Add(lockOnTarget);
                        }
                    }
                }
            }

            // We now sort through our potential targets to see which one we lock onto first
            for (int k = 0; k < availableTargets.Count; k++)
            {
                if (availableTargets[k] != null)
                {
                    float distanceFromTarget = Vector3.Distance(playerManager.transform.position, availableTargets[k].transform.position);

                    if (distanceFromTarget < shortestDistance)
                    {
                        shortestDistance = distanceFromTarget;
                        nearestLockOnTarget = availableTargets[k];
                    }

                    // If we are already locked on when searching for targets, search for our nearest left/right targets
                    if (playerManager.playerNetworkManager.isLockedOn.Value)
                    {
                        Vector3 relativeEnemyPosition = playerManager.transform.InverseTransformPoint(availableTargets[k].transform.position);

                        var distanceFromLeftTarget = relativeEnemyPosition.x;
                        var distanceFromRightTarget = relativeEnemyPosition.x;

                        if (availableTargets[k] == playerManager.playerCombatManager.currentTarget) continue;

                        // Check the left side for targets
                        if (relativeEnemyPosition.x <= 0.00 && distanceFromLeftTarget > shortestDistanceOfLeftTarget)
                        {
                            shortestDistanceOfLeftTarget = distanceFromLeftTarget;
                            leftLockOnTarget = availableTargets[k];
                        }
                        // Check the right side for targets
                        else if (relativeEnemyPosition.x >= 0.00 && distanceFromRightTarget < shortestDistanceOfRightTarget)
                        { 
                            shortestDistanceOfRightTarget = distanceFromRightTarget;
                            rightLockOnTarget = availableTargets[k];
                        }
                    }
                }
                else
                {
                    ClearLockOnTargets();
                    playerManager.playerNetworkManager.isLockedOn.Value = false;
                }
            }
        }

        public void SetLockCameraHeight()
        {
            if (cameraLockOnHeightCoroutine != null)
            { 
                StopCoroutine(cameraLockOnHeightCoroutine);
            }

            cameraLockOnHeightCoroutine = StartCoroutine(SetCameraHeight());
        }

        public void ClearLockOnTargets()
        {
            nearestLockOnTarget = null;
            leftLockOnTarget = null;
            rightLockOnTarget = null;
            availableTargets.Clear();
        }

        public IEnumerator WaitThenFindNewTarget()
        { 
            while (playerManager.isPerformingAction) 
            {
                yield return null;
            }

            ClearLockOnTargets();
            HandleLocatingLockOnTargets();

            if (nearestLockOnTarget != null)
            {
                playerManager.playerCombatManager.SetTarget(nearestLockOnTarget);
                playerManager.playerNetworkManager.isLockedOn.Value = true;
            }

            yield return null;
        }

        public IEnumerator SetCameraHeight()
        {
            float duration = 1;
            float timer = 0;

            Vector3 velocity = Vector3.zero;
            Vector3 newLockedCameraHeight = new Vector3(cameraPivotTransform.transform.localPosition.x, lockedCameraHeight);
            Vector3 newUnlockedCameraHeight = new Vector3(cameraPivotTransform.transform.localPosition.x, unlockedCameraHeight);

            while (timer < duration)
            {
                timer += Time.deltaTime;

                if (playerManager != null)
                {
                    if (playerManager.playerCombatManager.currentTarget != null)
                    {
                        cameraPivotTransform.transform.localPosition = Vector3.SmoothDamp(cameraPivotTransform.transform.localPosition, newLockedCameraHeight, ref velocity, setCameraHeightSpeed);
                        cameraPivotTransform.transform.localRotation = Quaternion.Slerp(cameraPivotTransform.transform.localRotation, Quaternion.Euler(0, 0, 0), lockOnTargetFollowSpeed);
                    }
                    else
                    {
                        cameraPivotTransform.transform.localPosition = Vector3.SmoothDamp(cameraPivotTransform.transform.localPosition, newUnlockedCameraHeight, ref velocity, setCameraHeightSpeed);
                        
                    }
                }

                yield return null;
            }

            if (playerManager != null)
            {
                if (playerManager.playerCombatManager.currentTarget != null)
                {
                    cameraPivotTransform.transform.localPosition = newLockedCameraHeight;
                    cameraPivotTransform.transform.localRotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    cameraPivotTransform.transform.localPosition = newUnlockedCameraHeight;
                }
            }

            yield return null;
        }
    }
}