using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [CreateAssetMenu(menuName = "StateActions/DetectPlayerAction")]
    public class TargetLastDetectedActionSO : StateActionSO<TargetLastDetectedAction>
    {
    }

    public class TargetLastDetectedAction : StateAction
    {
        private EventObject<ITargetable> target;
        private EventFloat targetDistance;
        private TargetDetectSystem detectSystem;
        private Transform myTransform;

        protected override void OnInit(SystemBinder systemBinder)
        {
            myTransform = systemBinder.GetSystem<Unit>().Transform;
            target = systemBinder.GetSystem<Unit>().Target;
            targetDistance = systemBinder.GetSystem<Unit>().TargetDistance;
            detectSystem = systemBinder.GetSystem<TargetDetectSystem>();

            hasActivate = true;
            hasDeactivate = true;
            hasUpdate = true;
        }

        public override void OnStateActivate()
        {
            detectSystem.DetectedObjects.AddEvent += OnDetectIn;
            detectSystem.DetectedObjects.RemoveEvent += OnDetectOut;
        }

        public override void OnStateDeactivate()
        {
            detectSystem.DetectedObjects.AddEvent -= OnDetectIn;
            detectSystem.DetectedObjects.RemoveEvent -= OnDetectOut;
        }

        public override void OnStateUpdate()
        {
            if (target.Value != null)
            {
                targetDistance.Value = Vector3.Distance(myTransform.position, target.Value.Transform.position);
            }
        }

        private void OnDetectIn(ITargetable targetable)
        {
            if(target.Value == null)
            {
                target.Value = targetable;
                targetDistance.Value = Vector3.Distance(myTransform.position, target.Value.Transform.position);
            }
        }

        private void OnDetectOut(ITargetable targetable)
        {
            if (target.Value.Transform == targetable.Transform)
            {
                target.Value = null;
                targetDistance.Value = float.PositiveInfinity;
            }
        }

    }
}
