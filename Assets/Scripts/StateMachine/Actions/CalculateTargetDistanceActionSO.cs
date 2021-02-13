using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [CreateAssetMenu(menuName = "StateActions/CalculateTargetDistanceAction")]
    public class CalculateTargetDistanceActionSO : StateActionSO<CalculateTargetDistanceAction>
    {
        [SerializeField] private float calculateInterval = .1f;

        public float CalculateInterval => calculateInterval;
    }

    public class CalculateTargetDistanceAction : StateAction
    {
        private CalculateTargetDistanceActionSO SO => so as CalculateTargetDistanceActionSO;

        private Unit unit;
        private float lastCalculateTime;

        protected override void OnInit(SystemBinder systemBinder)
        {
            unit = systemBinder.GetSystem<Unit>();

            hasUpdate = true;
        }

        public override void OnStateUpdate()
        {
            if (lastCalculateTime + SO.CalculateInterval <= Time.time && unit.Target.Value != null)
            {
                lastCalculateTime = Time.time;
                unit.TargetDistance.Value = Vector3.Distance(unit.Transform.position, unit.Target.Value.Transform.position);
            }
        }

    }
}
