// File: CameraFollow.cs
// Author: Richard Kneale
// Date created: 30th October 2017
// Description: Allows the camera to follow a Transform

using UnityEngine;

namespace RolePlayingGame
{
    public class CameraFollow : MonoBehaviour
    {
        // The position that that camera will be following
        [SerializeField]
        private Transform target;

        // The speed with which the camera will be following
        [SerializeField]
        private float smoothing = 1f;

        // A vector representing the cameras initial position
        private Vector3 transformPosition;

        // The offset from the target
        private Vector3 offset;

        // Use this for initialization
        private void Start()
        {
            // Initialise the offset vector
            offset = new Vector3(0f, 0f, 0f);
        }

        // Update is called once per frame
        private void Update()
        {
            // If a target has been assigned
            if (target != null)
            {
                // Set the next position for the camera
                SetTransformPosition();

                // Interpolate towards the new position for the camera
                if(smoothing > 0f)
                {
                    // Smoothly interpolate between the current position and target position of the camera
                    transform.position = Vector3.Lerp(transform.position, transformPosition, smoothing * Time.deltaTime);
                }
                else
                {
                    // Move the camera to the next position
                    transform.position = transformPosition;
                }
            }
        }

        // Assign a target to the camera
        public void SetTarget(GameObject _followMe, Vector3 _offsetVector)
        {
            // Set the target and the offset of the camera from the target
            target = _followMe.transform;
            offset = _offsetVector;

            // Set the next position for the camera
            SetTransformPosition();

            // Move the camera to the next position
            transform.position = transformPosition;
        }

        // Sets the next position for the camera
        private void SetTransformPosition()
        {
            // (player's x position + offset x, player's y position + offset y, camera's z position)
            transformPosition.Set((target.position.x + offset.x), (target.position.y + offset.y), offset.z);
        }
    }
}
