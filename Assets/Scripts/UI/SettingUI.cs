using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [RequireComponent(typeof(Animator))]
    public abstract class SettingUI : MonoBehaviour
    {
        [SerializeField] protected string settingName;
        protected Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            Debug.AssertIsNotNull(animator);
            Debug.AssertIsNotNull(animator.runtimeAnimatorController);
            OnAwake();
        }

        protected abstract void OnAwake();
    }
}
