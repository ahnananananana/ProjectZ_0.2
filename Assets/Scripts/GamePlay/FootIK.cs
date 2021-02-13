using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [RequireComponent(typeof(Animator))]
    public class FootIK : MonoBehaviour
    {
        private Animator animator;
        [SerializeField] [Range(0f, 1f)] 
        private float distanceToGround;
        [SerializeField] private LayerMask targetLayer;
        [SerializeField] Transform leftFootForward, rightFootForward;
        private int leftFootWeightHash, rightFootWeightHash;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            leftFootWeightHash = Animator.StringToHash("LeftFootIKWeight");
            rightFootWeightHash = Animator.StringToHash("RightFootIKWeight");
        }

        private void OnAnimatorIK(int layerIndex)
        {
            SetFootIK(AvatarIKGoal.LeftFoot, leftFootWeightHash, leftFootForward);
            SetFootIK(AvatarIKGoal.RightFoot, rightFootWeightHash, rightFootForward);
        }

        private void SetFootIK(AvatarIKGoal footSide, int weightHash, Transform footTF)
        {
            animator.SetIKPositionWeight(footSide, animator.GetFloat(weightHash));
            animator.SetIKRotationWeight(footSide, animator.GetFloat(weightHash));

            Ray ray = new Ray(animator.GetIKPosition(footSide) + Vector3.up, Vector3.down);
            if (Physics.Raycast(ray, out var hit, distanceToGround + 1f, targetLayer))
            {
                var footPos = hit.point;
                footPos.y += distanceToGround;

                animator.SetIKPosition(footSide, footPos);
                animator.SetIKRotation(footSide, Quaternion.LookRotation(footTF.forward, hit.normal));
            }
        }
    }
}
