using UnityEngine;
using UnityEngine.EventSystems;

namespace HDV
{
    //TODO: animator부분 따로 떼어내야
    public class SettingToggle : SettingUI
    {
        [SerializeField] private string onAnimName;

        [SerializeField] private BoolEvent valueChangeEvent;

        private string animDirectionParam = "Direction";
        private int animDirectionHash;

        private int onAnimHash;

        private bool isOn;

        protected override void OnAwake()
        {
            onAnimHash = Animator.StringToHash(onAnimName);
            animDirectionHash = Animator.StringToHash(animDirectionParam);
        }

        private void OnEnable()
        {
            isOn = PlayerPrefs.GetInt(settingName, 1) == 1 ? true : false;

            if (isOn)
            {
                animator.Play(onAnimHash, 0, 1f);
            }
            else
            {
                animator.SetFloat(animDirectionHash, -1f);
                animator.Play(onAnimHash, 0, 0f);
            }
        }

        public void OnClick(PointerEventData eventData)
        {
            isOn = !isOn;
            valueChangeEvent?.Invoke(isOn);
            if (isOn)
            {
                animator.SetFloat(animDirectionHash, 1f);
                animator.Play(onAnimHash, 0, Mathf.Clamp01(animator.GetCurrentAnimatorStateInfo(0).normalizedTime));
            }
            else
            {
                animator.SetFloat(animDirectionHash, -1f);
                animator.Play(onAnimHash, 0, Mathf.Clamp01(animator.GetCurrentAnimatorStateInfo(0).normalizedTime));
            }

            PlayerPrefs.SetInt(settingName, isOn ? 1 : 0);
        }

    }
}

