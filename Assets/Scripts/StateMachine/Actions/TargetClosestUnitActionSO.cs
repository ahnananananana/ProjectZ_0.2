
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [CreateAssetMenu(menuName = "StateActions/TargetClosestAction")]
    public class TargetClosestUnitActionSO : StateActionSO<TargetClosestUnitAction>
    {
        [SerializeField] private float calcualteInterval;
        [SerializeField] private bool onActivate, onDeactivate, onUpdate;

        public float CalcualteInterval => calcualteInterval;

        public bool OnActivate => onActivate;
        public bool OnDeactivate => onDeactivate;
        public bool OnUpdate => onUpdate;
    }

    public class TargetClosestUnitAction : StateAction
    {
        private TargetClosestUnitActionSO SO => so as TargetClosestUnitActionSO;

        private EventObject<ITargetable> target;
        private EventFloat targetDistance;
        private TargetDetectSystem detectSystem;
        private Transform myTransform;
        private float lastCalculatedTime;

        protected override void OnInit(SystemBinder systemBinder)
        {
            myTransform = systemBinder.GetSystem<Unit>().Transform;
            targetDistance = systemBinder.GetSystem<Unit>().TargetDistance;
            target = systemBinder.GetSystem<Unit>().Target;
            detectSystem = systemBinder.GetSystem<TargetDetectSystem>();

            hasActivate = SO.OnActivate;
            hasDeactivate = SO.OnDeactivate;
            hasUpdate = SO.OnUpdate;
        }

        public override void OnStateActivate()
        {
            FindClosestEnemy();
        }

        public override void OnStateDeactivate()
        {
            FindClosestEnemy();
        }

        public override void OnStateUpdate()
        {
            if (lastCalculatedTime + SO.CalcualteInterval <= Time.time)
            {
                lastCalculatedTime = Time.time;
                FindClosestEnemy();
            }
        }

        private void FindClosestEnemy()
        {
            ITargetable newTarget = null;
            float minDis = float.MaxValue;
            for (int i = 0; i < detectSystem.DetectedObjects.Count; ++i)
            {
                var obj = detectSystem.DetectedObjects[i];
                float dis = Vector3.Distance(myTransform.position, obj.Transform.position);
                if (dis < minDis)
                {
                    newTarget = obj;
                    minDis = dis;
                }
            }
            target.Value = newTarget;
            targetDistance.Value = minDis;
        }

    }
}
