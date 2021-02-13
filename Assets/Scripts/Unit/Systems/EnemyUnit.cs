using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public class EnemyUnit : Unit<EnemyUnitData, EnemyUnitModel>
    {
        private EventObject<ITargetable> target = new EventObject<ITargetable>();
        private EventFloat targetDistance = new EventFloat(float.MaxValue);

        public override EventObject<ITargetable> Target => target;
        public override EventFloat TargetDistance => targetDistance;

        protected override void OnInit()
        {
        }
    }
}
