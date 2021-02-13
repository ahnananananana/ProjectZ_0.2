using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [CreateAssetMenu(menuName = "StateActions/AimTargetAction")]
    public class AimTargetActionSO : StateActionSO<AimTargetAction>
    {
    }

    public class AimTargetAction : StateAction
    {
        private EventObject<ITargetable> target;
        private EventVector3 lookDirection;
        private Transform aimPoint;
        private WeaponRigSystem rigSystem;
        private Transform myTrasform;

        protected override void OnInit(SystemBinder systemBinder)
        {
            target = systemBinder.GetSystem<Unit>().Target;
            lookDirection = systemBinder.GetSystem<MoveSystem>().LookDirection;
            aimPoint = systemBinder.GetSystem<Unit>().AimPoint;
            rigSystem = systemBinder.GetSystem<WeaponRigSystem>();
            myTrasform = systemBinder.transform;

            hasActivate = true;
            hasUpdate = true;
            hasDeactivate = true;
        }

        public override void OnStateActivate()
        {
            CalculateLookDirection();
        }

        public override void OnStateDeactivate()
        {
            rigSystem.SetAimRig(0f);
            target.Value = null;
        }

        public override void OnStateUpdate()
        {
            CalculateLookDirection();
        }

        private void CalculateLookDirection()
        {
            if (target.Value != null)
            {
                rigSystem.SetAimRig(1f);
                aimPoint.position = target.Value.TargetPoint;
                var temp = aimPoint.position - myTrasform.position;
                temp.y = 0f;
                lookDirection.Value = temp.normalized;
            }
            else
            {
                rigSystem.SetAimRig(0f);
            }
        }

    }
}
