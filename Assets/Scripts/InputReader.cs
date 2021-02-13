using System;
using UnityEngine;

namespace HDV
{
    [CreateAssetMenu]
    public class InputReader : ScriptableObject
    {
        public event Action<Vector2> MoveInputEvent, AttackJoystickEvent;
        public event Action<bool> AttackToggleEvent;
        public event Action SwitchWeaponEvent, InteractEvent;
        public event Action BackButtonEvent;

        public void OnMoveInput(Vector2 direction)
        {
            MoveInputEvent.Invoke(direction);
        }

        public void OnAttackToggle(bool set)
        {
            AttackToggleEvent?.Invoke(set);
        }

        public void OnAttackJoystick(Vector2 direction)
        {
            AttackJoystickEvent?.Invoke(direction);
        }
        
        public void OnSwitchWeaponButton()
        {
            SwitchWeaponEvent?.Invoke();
        }

        public void OnInteractButton()
        {
            InteractEvent?.Invoke();
        }

        public void OnBackButton()
        {
            BackButtonEvent?.Invoke();
        }
    }
}
