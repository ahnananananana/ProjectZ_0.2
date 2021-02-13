using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public class TargetDetectSystem : DetectSystem<ITargetable>
    {
        private EventObject<ITargetable> target = new EventObject<ITargetable>();
        private EventFloat targetDistance = new EventFloat();
        public EventObject<ITargetable> Target => target;
        public EventFloat TargetDistance => targetDistance;
        private ITargetable lastTarget;

        [SerializeField] private TransformEvent targetDetectEvent, targetLoseEvent;

        protected override void OnAwake()
        {
            target.ChangeEvent += OnChangeTarget;
        }

        protected override void OnDetectIn(ITargetable obj)
        {
            obj.DisableEvent += OnDisableTarget;
        }

        protected override void OnDetectOut(ITargetable obj)
        {
            obj.DisableEvent -= OnDisableTarget;
            if (target.Value == obj)
            {
                target.Value = null;
                targetDistance.Value = float.MaxValue;
            }
        }

        private void OnChangeTarget(object obj)
        {
            if(obj != null)
            {
                lastTarget = obj as ITargetable;
                targetDetectEvent?.Invoke(lastTarget.Transform);
            }
            else
            {
                lastTarget = null;
                targetLoseEvent?.Invoke(null);
            }
        }


        private void OnDisableTarget(ITargetable targetable)
        {
            targetable.DisableEvent -= OnDisableTarget;
            if (detectedObjects.Remove(targetable) && target.Value == targetable)
            {
                target.Value = null;
                targetDistance.Value = float.MaxValue;
            }
        }

        private void OnDrawGizmos()
        {
            if(target.Value != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(target.Value.TargetPoint, .25f);
            }
        }
        /*[SerializeField] private float calculateInterval = .1f;
        private EventObject<Unit> currentTarget = new EventObject<Unit>();
        private EventFloat targetDistance = new EventFloat();
        private IEnumerator coroutine;

        public EventObject<Unit> CurrentTarget => currentTarget;
        public EventFloat TargetDistance => targetDistance;

        protected override void OnDetectIn(Unit obj)
        {
            if(coroutine == null)
            {
                coroutine = FindClosest();
                StartCoroutine(coroutine);
            }
        }

        protected override void OnDetectOut(Unit obj)
        {
            if(obj == currentTarget.Value)
            {
                currentTarget.Value = null;
                targetDistance.Value = float.MaxValue;
            }
        }

        private IEnumerator FindClosest()
        {
            while(detectedObjects.Count > 0)
            {
                float minDis = float.MaxValue;
                Unit newTarget = null;
                for(int i = 0; i < detectedObjects.Count; ++i)
                {
                    var unit = detectedObjects[i];
                    float dis = Vector3.Distance(transform.position, unit.transform.position);
                    if(dis < minDis)
                    {
                        minDis = dis;
                        newTarget = unit;
                    }
                }
                currentTarget.Value = newTarget;
                yield return new WaitForSeconds(calculateInterval);
            }
            currentTarget.Value = null;
            targetDistance.Value = float.MaxValue;
            coroutine = null;
        }*/
    }
}
