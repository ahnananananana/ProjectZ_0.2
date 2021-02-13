using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [CreateAssetMenu(menuName = "StateActions/LookAtTargetDirectionAction")]
    public class LookAtTargetDirectionActionSO : StateActionSO<LookAtTargetDirectionAction>
    {
    }

    public class LookAtTargetDirectionAction : StateAction
    {
        private EventObject<ITargetable> target;
        private Transform myTransform;
        private EventVector3 lookDirection;

        protected override void OnInit(SystemBinder systemBinder)
        {
            target = systemBinder.GetSystem<Unit>().Target;
            myTransform = systemBinder.transform;
            lookDirection = systemBinder.GetSystem<MoveSystem>().LookDirection;

            hasActivate = true;
        }

        public override void OnStateActivate()
        {
            if(target.Value != null)
                lookDirection.Value = (target.Value.Transform.position - myTransform.position).normalized;
        }
    }
}
