using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HDV
{
    public class Gold : MonoBehaviour
    {
        [SerializeField] private int value = 1;
        [SerializeField] private IntEvent collectEvent;
        [SerializeField] private UnityEvent dropOnGroundEvent;
        private new Rigidbody rigidbody;

        public int Value => value;

        public Rigidbody Rigidbody => rigidbody; 

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            rigidbody.maxAngularVelocity = Mathf.Infinity;
        }

        public void FollowPlayer(Collider collider)
        {
            StartCoroutine(Follow(collider.GetComponent<Unit>()));
        }

        private IEnumerator Follow(Unit unit)
        {
            while(Vector3.Distance(transform.position, unit.TargetPoint) > 1f)
            {
                transform.position = Vector3.Lerp(transform.position, unit.TargetPoint, Time.deltaTime * 10f);
                yield return null;
            }
            collectEvent?.Invoke(value);
            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            dropOnGroundEvent?.Invoke();
        }
    }
}
