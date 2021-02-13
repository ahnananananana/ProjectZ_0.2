using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HDV
{
    [System.Serializable]
    public class UIUnit : MonoBehaviour, IViewTemplate<PlayerUnitBaseData>, IToggle
    {
        [SerializeField] private Image image;
        [SerializeField] private TMPro.TMP_Text nameText;
        private PlayerUnitBaseData unitBaseData;
        private Animator animator;

        public PlayerUnitBaseData Model => unitBaseData;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void Init(PlayerUnitBaseData baseData)
        {
            image.sprite = baseData.Image;
            nameText.text = baseData.UnitName;
            unitBaseData = baseData;
            baseData.CanPlay.ChangeEvent += SetActive;
            SetActive(baseData.CanPlay.Value);
        }

        public void SetSelect(bool value)
        {
            if (value)
                animator.Play("UIUnitSelect");
            else
                animator.Play("None");
        }

        public void SetActive(bool value)
        {
            image.raycastTarget = value;
            nameText.raycastTarget = value;
            if (value)
            {
                image.CrossFadeAlpha(1f, 0f, true);
                nameText.CrossFadeAlpha(1f, 0f, true);
            }
            else
            {
                image.CrossFadeAlpha(.5f, 0f, true);
                nameText.CrossFadeAlpha(.5f, 0f, true);
            }
        }

        public void SetToggle(bool value)
        {
            if (value)
                animator.SetBool("Select", true);
            else
                animator.SetBool("Select", false);
        }
    }
}
