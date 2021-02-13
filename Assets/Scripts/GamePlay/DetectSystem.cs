using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [RequireComponent(typeof(Collider))]
    public abstract class DetectSystem<T> : MonoBehaviour, IStateComponent
        where T : class
    {
        //[SerializeField] private LayerMask targetLayerMask;
        //[SerializeField] private float calculateInterval = .2f;
        private Collider triggerCollider;
        public DetectEvent TriggerEnterEvent, TriggerExitEvent;
        //private int targetLayer;
        protected EventList<T> detectedObjects = new EventList<T>();
        //private Dictionary<ITargetable, EventFloat> distances = new Dictionary<ITargetable, EventFloat>();
        //protected List<EventFloat> distances = new List<EventFloat>();
        //private IEnumerator calculateCoroutine;

        public EventList<T> DetectedObjects => detectedObjects;
        //public List<EventFloat> Distances => distances;
        //public Dictionary<ITargetable, EventFloat> Distances => distances; 

        private void Awake()
        {
            triggerCollider = GetComponent<Collider>();
            Debug.AssertIsNotNull(triggerCollider);
            //targetLayer = 1 << targetLayerMask;
            OnAwake();
        }

        protected virtual void OnAwake() { }

        private void OnTriggerEnter(Collider other)
        {
            T obj = other.GetComponent<T>();
            Debug.AssertIsNotNull(obj);
            //distances.Add(obj, new EventFloat(CalculateDistance(obj.Transform)));
            detectedObjects.Add(obj);

            /*if(calculateCoroutine == null)
            {
                calculateCoroutine = CalculateDistances();
                StartCoroutine(calculateCoroutine);
            }

            int index = detectedObjects.Count - 1;
            obj.UntargetedEvent += () =>
            {
                distances.Remove(obj);
                detectedObjects.Remove(obj);
            };*/
            OnDetectIn(obj);
        }

        protected virtual void OnDetectIn(T obj) { }

        private void OnTriggerExit(Collider other)
        {
            T obj = other.GetComponent<T>();
            Debug.AssertIsNotNull(obj);
            detectedObjects.Remove(obj);
            //distances.Remove(obj);
            OnDetectOut(obj);
        }
        protected virtual void OnDetectOut(T obj) { }

        /*private float CalculateDistance(Transform target) => Vector3.Distance(transform.position, target.transform.position);

        private IEnumerator CalculateDistances()
        {
            while(detectedObjects.Count > 0)
            {
                yield return new WaitForSeconds(calculateInterval);
                for (int i = 0; i < detectedObjects.Count; ++i)
                {
                    var obj = detectedObjects[i];
                    distances[obj].Value = CalculateDistance(obj.Transform);
                }
            }
            calculateCoroutine = null;
        }*/

        /* private Vector3 CalculatePoint(Collider other)
         {
             Vector3 myCenter = triggerCollider.bounds.center;
             Vector3 otherCenter = other.bounds.center;
             Ray ray = new Ray(myCenter, otherCenter - myCenter);
             Vector3 triggeredPoint;
             if (Physics.Raycast(ray, out RaycastHit hit, Vector3.Distance(myCenter, otherCenter), targetLayer))
             {
                 triggeredPoint = hit.point;
             }
             else
             {
                 triggeredPoint = myCenter;
             }
             return triggeredPoint;
         }*/
    }
}