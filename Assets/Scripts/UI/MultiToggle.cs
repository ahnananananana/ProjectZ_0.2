using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace HDV
{
    [Serializable]
    public struct MultiToggleState
    {
        public UnityEvent ActiveEvent, DeactiveEvent;
    }

    public class MultiToggle : MonoBehaviour, IPointerClickHandler 
    {
        private bool isOn;
        private int currentIdx;
        [SerializeField] private MultiToggleGroup multiToggleGroup;
        [SerializeField] private MultiToggleState[] toggleEvents = new MultiToggleState[0];

        private void Awake()
        {
            if (multiToggleGroup != null)
                multiToggleGroup.RegisterToggle(this);
        }

        private void OnEnable()
        {
            Disable();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!isOn)
            {
                isOn = true;
                currentIdx = 0;
                multiToggleGroup.Toggled(this);
            }
            else
            {
                toggleEvents[currentIdx].DeactiveEvent?.Invoke();
                currentIdx = (currentIdx + 1) % toggleEvents.Length;
            }

            toggleEvents[currentIdx].ActiveEvent?.Invoke();
        }

        public void Disable()
        {
            isOn = false;
            for (int i = 0; i < toggleEvents.Length; ++i)
                toggleEvents[i].DeactiveEvent?.Invoke();
        }
    }
}

