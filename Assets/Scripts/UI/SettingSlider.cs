using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HDV
{
    public class SettingSlider : SettingUI
    {
        [SerializeField] private Slider slider;
        [SerializeField] private string onAnimStateName, offAnimStateName;
        [SerializeField] private float offThreshhold;

        [SerializeField] private FloatEvent valueChangeEvent;

        private int onAnimHash, offAnimHash;
        private float value;

        protected override void OnAwake()
        {
            Debug.AssertIsNotNull(slider);
            slider.onValueChanged.AddListener(OnValueChanged);

            onAnimHash = Animator.StringToHash(onAnimStateName);
            offAnimHash = Animator.StringToHash(offAnimStateName);

            value = PlayerPrefs.GetFloat(settingName, 1f);
            slider.SetValueWithoutNotify(value);


            if (value <= offThreshhold)
                animator.Play(offAnimHash, 0, 1f);
            else
                animator.Play(onAnimHash, 0, 1f);
        }

        private void OnValueChanged(float newValue)
        {
            if (value > offThreshhold && newValue <= offThreshhold)
            {
                animator.Play(offAnimHash);
            }
            else if(value <= offThreshhold && newValue > offThreshhold)
            {
                animator.Play(onAnimHash);
            }

            value = newValue;
            valueChangeEvent?.Invoke(newValue);
        }

        public void OnSet(PointerEventData eventData)
        {
            PlayerPrefs.SetFloat(settingName, value);
        }

    }
}
