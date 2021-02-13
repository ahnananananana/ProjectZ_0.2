using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [CreateAssetMenu(menuName = "StateActions/QuaterViewMoveAction")]
    public class QuaterViewMoveActionSO : StateActionSO<QuaterViewMoveAction>
    {
    }

    public class QuaterViewMoveAction : StateAction
    {
        private EventVector2 moveInput;
        private EventVector3 screenYDirection;
        private MoveSystem moveSystem;
        private Stat moveSpeed;

        protected override void OnInit(SystemBinder systemBinder)
        {
            moveInput = systemBinder.GetSystem<InputSystem>().LastMoveInput;
            screenYDirection = systemBinder.GetSystem<PlayerUnit>().FollowingCamera.ScreenYDir;
            moveSystem = systemBinder.GetSystem<MoveSystem>();
            moveSpeed = systemBinder.GetSystem<Unit>().UnitData.Stats[StatType.MoveSpeed];

            hasDeactivate = true;
            hasFixedUpdate = true;
        }

        public override void OnStateDeactivate()
        {
            base.OnStateDeactivate();
            moveSystem.Move(Vector3.zero, 0f);
        }

        public override void OnStateFixedUpdate()
        {
            var angle = Vector2.Angle(moveInput.Value, Vector2.up);
            if (moveInput.Value.x < 0)
                angle = -angle;

            var moveDirection = (Quaternion.Euler(0, angle, 0) * screenYDirection.Value).normalized;

            var intensity = moveInput.Value.sqrMagnitude;
            intensity = intensity > .25f ? intensity : .25f;

            moveSystem.Move(moveDirection, moveSpeed.Value * intensity);
        }
    }
}
