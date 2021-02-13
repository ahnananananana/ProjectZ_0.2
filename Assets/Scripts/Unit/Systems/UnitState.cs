using System;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    /*public class UnitState : MonoBehaviour, IStateComponent
    {
        private bool isActive = true;
        [SerializeField] private EventObject<ITargetable> currentTarget = new EventObject<ITargetable>();
        private EventFloat targetDistance = new EventFloat();

        public EventObject<ITargetable> CurrentTarget => currentTarget;
        public EventFloat TargetDistance => targetDistance;

        public bool IsActive { get => isActive; set => isActive = value; }

        private void OnDrawGizmos()
        {
            if(currentTarget.Value != null)
                Gizmos.DrawSphere(currentTarget.Value.TargetPoint, .5f);
        }
    }*/
}
