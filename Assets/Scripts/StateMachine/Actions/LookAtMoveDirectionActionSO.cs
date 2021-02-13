using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [CreateAssetMenu(menuName = "StateActions/LookAtMoveDirectionAction")]
    public class LookAtMoveDirectionActionSO : StateActionSO<LookAtMoveDirectionAction>
    {
    }

    public class LookAtMoveDirectionAction : StateAction
    {
        private EventVector3 lookDirection, moveDirection;

        protected override void OnInit(SystemBinder systemBinder)
        {
            lookDirection = systemBinder.GetSystem<MoveSystem>().LookDirection;
            moveDirection = systemBinder.GetSystem<MoveSystem>().MoveDirection;

            hasActivate = true;
            hasDeactivate = true;
        }

        public override void OnStateActivate()
        {
            lookDirection.Value = moveDirection.Value;
            moveDirection.ChangeEvent += lookDirection.SetValue;
        }

        public override void OnStateDeactivate()
        {
            moveDirection.ChangeEvent -= lookDirection.SetValue;
        }
    }
}
