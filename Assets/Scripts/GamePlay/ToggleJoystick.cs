
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace HDV
{
    public class ToggleJoystick : Joystick, IPointerClickHandler
    {
        private bool isToggled;

        [SerializeField] private BoolEvent toggleEvent;

        public override void OnDrag(PointerEventData eventData)
        {
            if (isToggled)
            {
                isToggled = false;
                toggleEvent?.Invoke(false);
            }

            base.OnDrag(eventData);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(!isSet)
            {
                isToggled = !isToggled;
                toggleEvent?.Invoke(isToggled);
            }
        }
    }

}
