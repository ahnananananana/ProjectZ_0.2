using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public class CameraSystem : MonoBehaviour, IStateComponent
    {
        [SerializeField] private FollowingCamera followingCamera;
        public FollowingCamera FollowingCamera => followingCamera;

        public bool IsActive
        {
            get => followingCamera.FollowingObject == transform;
            set
            {

                if (value)
                    followingCamera?.SetTarget(transform);
                else
                    followingCamera?.ReleaseTarget(transform);
            }
        }


        private void Awake()
        {
            IsActive = true;
        }

    }
}
