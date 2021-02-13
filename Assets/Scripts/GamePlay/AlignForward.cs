using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public class AlignForward : MonoBehaviour
    {
        [SerializeField] private Transform target;

        private void Awake()
        {
            if (target == null)
                target = FindObjectOfType<FollowingCamera>().transform;
        }

        private void LateUpdate()
        {
            if (target != null)
                transform.forward = target.transform.forward;
        }
    }
}
