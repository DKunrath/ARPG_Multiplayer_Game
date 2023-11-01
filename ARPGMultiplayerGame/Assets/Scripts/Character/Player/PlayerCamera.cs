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

            // Else totate normally

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
    }
}