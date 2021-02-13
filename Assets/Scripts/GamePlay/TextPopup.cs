using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public class TextPopup : MonoBehaviour
    {
        [SerializeField] private TMPro.TMP_Text popupText;
        private Animator animator;
        [SerializeField] private string popupAnimName, noneAnimName;
        private int popupAnimHash, noneAnimHash;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            popupAnimHash = Animator.StringToHash(popupAnimName);
            noneAnimHash = Animator.StringToHash(noneAnimName);
        }

        public void SetText(string text)
        {
            popupText.text = text;
            animator.Play(popupAnimHash);
        }

        public void OnAnimationEnd()
        {
            animator.Play(noneAnimHash);
            TextPopupObjectPool.current.ReturnObject(this);
        }
    }
}
