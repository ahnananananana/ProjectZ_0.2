using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public class RagdollSystem : MonoBehaviour, IStateComponent
    {
        private bool isActive;
        [SerializeField] private bool activeOnAwake;

        [SerializeField] private RagdollElement[] ragdollElements;

        public bool IsActive 
        {
            get => isActive; 
            set
            {
                isActive = value;
                for (int i = 0; i < ragdollElements.Length; ++i)
                    ragdollElements[i].SetActive(value);
            }
        }

        private void Awake()
        {
            IsActive = activeOnAwake;
        }

    }
}

/*TODO: 공부해야할 것들
 * Rigidbody를 sleep후 wakeup시 갑작스러운 튕김현상
 * Animator를 enable false했을 때 메쉬의 튕김현상
 * joint.connectbody를 재설정 시 튕김현상
 */