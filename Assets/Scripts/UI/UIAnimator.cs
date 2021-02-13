using System;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public enum UIAnimationState
    {
        Open,
        Opening,
        Close,
        Closing,
    }

    [RequireComponent(typeof(Animator))]
    public class UIAnimator : MonoBehaviour
    {
        private Animator animator;
        private string openAnimName = "Open", closeAnimName = "Close";
        private int openAnimHash, closeAnimHash;
        private UIAnimationState state;
        private List<UIAnimator> childAnimators = new List<UIAnimator>();

        public UIAnimationState State => state;
        public event Action OpenEvent, CloseEvent;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            openAnimHash = Animator.StringToHash(openAnimName);
            closeAnimHash = Animator.StringToHash(closeAnimName);

            animator.speed = 0f;
            animator.Play(openAnimHash, 0, 0f);
        }

        private void OnEnable()
        {
            if(state == UIAnimationState.Opening)
            {
                animator.Play(openAnimHash, 0, 1f);
                state = UIAnimationState.Open;
            }
        }

        public void SetActive(bool value)
        {
            animator.enabled = value;
        }

        public void PlayOpen()
        {
            animator.speed = 1f;
            animator.Play(openAnimHash);
            state = UIAnimationState.Opening;
        }

        public void PlayClose()
        {
            animator.speed = 1f;
            animator.Play(closeAnimHash);
            state = UIAnimationState.Closing;
        }

        public void OnOpen()
        {
            for(int i = 0; i < childAnimators.Count; ++i)
                if (childAnimators[i].state != UIAnimationState.Open)
                    return;

            state = UIAnimationState.Open;
            OpenEvent?.Invoke();
        }

        public void OnClose()
        {
            for (int i = 0; i < childAnimators.Count; ++i)
                if (childAnimators[i].state != UIAnimationState.Close)
                    return;

            state = UIAnimationState.Close;
            CloseEvent?.Invoke();
        }

        public void AddChild(IUIAnimator obj)
        {
            obj.UIAnimator.OpenEvent += OnOpen;
            obj.UIAnimator.CloseEvent += OnClose;
            childAnimators.Add(obj.UIAnimator);
        }

        public void RemoveChild(IUIAnimator obj)
        {
            if(childAnimators.Remove(obj.UIAnimator))
            {
                obj.UIAnimator.OpenEvent -= OnOpen;
                obj.UIAnimator.CloseEvent -= OnClose;
            }
        }

    }
}
