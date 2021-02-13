using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HDV
{
    public class KeyboardInput : MonoBehaviour
    {
        private Vector2 previousArrowInput;
        private bool isAttackToggled;

        [SerializeField] private Vector2Event arrowInputEvent;
        [SerializeField] private BoolEvent attackInputEvent;
        [SerializeField] private UnityEvent backInputEvent, interactInputEvent;

        private void Update()
        {
            ProcessArrowInput();
            ProcessAttackInput();
            ProcessBackInput();
            ProcessInteractInput();
        }

        private void ProcessArrowInput()
        {
            var newArrowInput = (new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))).normalized;
            if(previousArrowInput != newArrowInput)
            {
                arrowInputEvent?.Invoke(newArrowInput);
                previousArrowInput = newArrowInput;
            }
        }

        private void ProcessAttackInput()
        {
            if(Input.GetKeyDown(KeyCode.Z))
            {
                isAttackToggled = !isAttackToggled;
                attackInputEvent?.Invoke(isAttackToggled);
            }
        }

        private void ProcessBackInput()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                backInputEvent?.Invoke();
            }
        }

        private void ProcessInteractInput()
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                interactInputEvent?.Invoke();
            }
        }

    }
}
