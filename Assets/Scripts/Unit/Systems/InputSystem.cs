using System;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public class InputSystem : MonoBehaviour, IStateComponent
    {
        [SerializeField] private InputReader inputReader;
        private EventVector2 lastMoveInput = new EventVector2();
        private EventVector3 attackDirection = new EventVector3();
        private EventBool isAttackPressed = new EventBool();
        private EventBool interactInput = new EventBool();
        private FollowingCamera followingCamera;

        public EventVector2 LastMoveInput => lastMoveInput;
        public EventBool IsAttackPressed => isAttackPressed;
        public EventBool InteractInput => interactInput;
        public EventVector3 AttackDirection => attackDirection;

        private void Awake()
        {
            followingCamera = FindObjectOfType<FollowingCamera>();
        }

        private void OnEnable()
        {
            inputReader.MoveInputEvent += lastMoveInput.SetValue;
            inputReader.AttackToggleEvent += isAttackPressed.SetValue;
            inputReader.InteractEvent += () => interactInput.SetValue(true);
            inputReader.AttackJoystickEvent += (input) => attackDirection.SetValue(ProcessAttackDirection(input));
        }

        private void OnDisable()
        {
            inputReader.MoveInputEvent -= lastMoveInput.SetValue;
            inputReader.AttackToggleEvent -= isAttackPressed.SetValue;
            inputReader.InteractEvent -= () => interactInput.SetValue(true);
            inputReader.AttackJoystickEvent -= (input) => attackDirection.SetValue(ProcessAttackDirection(input));
        }

        private Vector3 ProcessAttackDirection(Vector2 input)
        {
            if (input == Vector2.zero)
                return Vector3.zero;

            var angle = Vector2.Angle(input, Vector2.up);
            if (input.x < 0)
                angle = -angle;

            return (Quaternion.Euler(0, angle, 0) * followingCamera.ScreenYDir.Value).normalized;
        }

    }
}
