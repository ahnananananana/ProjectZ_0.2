using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [RequireComponent(typeof(Animator))]
    public class AnimationSystem : MonoBehaviour, IStateComponent
    {
        private Animator animator;
        private EventInt currentAnimationHash = new EventInt();
        private Dictionary<int, bool> triggers = new Dictionary<int, bool>();

        public EventInt CurrentAnimationHash => currentAnimationHash;

        public bool IsActive { get => animator.enabled; set => animator.enabled = value; }

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void SetBool(int hashKey, bool value) => animator.SetBool(hashKey, value);
        public void SetInteger(int hashKey, int value) => animator.SetInteger(hashKey, value);
        public void SetFloat(int hashKey, float value) => animator.SetFloat(hashKey, value);
        public void SetTrigger(int hashKey)
        {
            if (!triggers.TryGetValue(hashKey, out bool isTriggered))
            {
                isTriggered = false;
                triggers.Add(hashKey, isTriggered);
            }

            if (!isTriggered)
            {
                Debug.Log("test");
                animator.SetTrigger(hashKey);
                triggers[hashKey] = true;
            }
        }

        public void ResetTrigger(int hashKey)
        {
            if (triggers[hashKey])
            {
                animator.ResetTrigger(hashKey);
                triggers[hashKey] = false;
            }
        }

        public void SetRootMotion(bool value) => animator.applyRootMotion = value;

        public void Play(int hashKey) => animator.Play(hashKey);

        public void OnAnimationStart()
        {
            int hash;
            if (animator.IsInTransition(0))
                hash = animator.GetNextAnimatorStateInfo(0).shortNameHash;
            else
                hash = animator.GetCurrentAnimatorStateInfo(0).shortNameHash;
            CurrentAnimationHash.Value = hash;
        }

        public void OnAnimationEnd()
        {
            CurrentAnimationHash.Value = 0;
        }
    }
}
