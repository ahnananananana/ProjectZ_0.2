using System;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public abstract class MoveSystem : MonoBehaviour, IStateComponent
    {
        private bool isActive = true;
        protected EventVector3 moveDirection = new EventVector3();
        protected EventFloat moveSpeed = new EventFloat();
        protected EventVector3 lookDirection = new EventVector3();
        protected EventVector2 lookingMoveDirection = new EventVector2();

        public EventVector3 LookDirection => lookDirection;
        public EventVector3 MoveDirection => moveDirection;
        public EventFloat MoveSpeed => moveSpeed;
        public bool IsActive { get => isActive; set => isActive = value; }
        public EventVector2 LookingMoveDirection => lookingMoveDirection;

        private void Awake()
        {
            moveDirection.ChangeEvent += (i) => lookingMoveDirection.Value = CalculateLookingMoveDirection(lookDirection.Value, i);
            lookDirection.ChangeEvent += (i) => lookingMoveDirection.Value = CalculateLookingMoveDirection(i, moveDirection.Value);
            lookingMoveDirection.Value = CalculateLookingMoveDirection(lookDirection.Value, moveDirection.Value);
            OnAwake();
        }

        protected virtual void OnAwake() { }

        public void Move(Vector3 direction, float speed)
        {
            if (!isActive)
                return;
            if(speed > 0f)
                OnMove(direction, speed);
            moveDirection.Value = direction;
            moveSpeed.Value = speed;
        }

        public Vector3 MoveTo(Vector3 targetPosition, float speed)
        {
            if (!isActive)
                return transform.position;
            var direction = (targetPosition - transform.position).normalized;
            OnMove(direction, speed);
            moveDirection.Value = direction;
            moveSpeed.Value = speed;
            return transform.position;
        }

        protected abstract void OnMove(Vector3 direction, float speed);

        private Vector2 CalculateLookingMoveDirection(Vector3 lookDirection, Vector3 moveDirection)
        {
            var offsetAngle = Vector3.Angle(lookDirection, Vector3.forward);
            if (Vector3.Dot(lookDirection, Vector3.right) < 0)
                offsetAngle = -offsetAngle;
            Vector3 temp = Quaternion.AngleAxis(-offsetAngle, Vector3.up) * moveDirection;
            return new Vector2(temp.x, temp.z);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + lookDirection.Value);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + moveDirection.Value);
            Gizmos.color = Color.yellow;
            var temp = new Vector3(lookingMoveDirection.Value.x, 0, lookingMoveDirection.Value.y);
            Gizmos.DrawLine(transform.position, transform.position + temp);

        }
    }
}

