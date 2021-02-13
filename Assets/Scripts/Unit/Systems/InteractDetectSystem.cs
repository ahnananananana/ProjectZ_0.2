using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public class InteractDetectSystem : DetectSystem<IInteractable>
    {
        [SerializeField] private float calculateInterval = .1f;
        private EventObject<IInteractable> currentTarget = new EventObject<IInteractable>();
        private IEnumerator coroutine;

        public EventObject<IInteractable> CurrentTarget => currentTarget;

        public void Interact(object interactor)
        {
            if(currentTarget.Value != null)
            {
                currentTarget.Value.Interact(interactor);
                detectedObjects.Remove(currentTarget.Value);
                currentTarget.Value = null;
            }
        }

        protected override void OnDetectIn(IInteractable obj)
        {
            if (coroutine == null)
            {
                coroutine = FindClosest();
                StartCoroutine(coroutine);
            }
        }

        protected override void OnDetectOut(IInteractable obj)
        {
            if (obj == currentTarget.Value)
            {
                currentTarget.Value.SetHighlight(false);
                currentTarget.Value = null;
            }
        }

        private IEnumerator FindClosest()
        {
            while (detectedObjects.Count > 0)
            {
                float minDis = float.MaxValue;
                IInteractable newTarget = null;
                for (int i = 0; i < detectedObjects.Count; ++i)
                {
                    var obj = detectedObjects[i];
                    float dis = Vector3.Distance(transform.position, obj.Position);
                    if (dis < minDis)
                    {
                        minDis = dis;
                        newTarget = obj;
                    }
                }

                if(currentTarget.Value != newTarget)
                {
                    if(currentTarget.Value != null)
                        currentTarget.Value.SetHighlight(false);
                    currentTarget.Value = newTarget;
                    currentTarget.Value.SetHighlight(true);
                }

                yield return new WaitForSeconds(calculateInterval);
            }
            currentTarget.Value = null;
            coroutine = null;
        }
    }
}
