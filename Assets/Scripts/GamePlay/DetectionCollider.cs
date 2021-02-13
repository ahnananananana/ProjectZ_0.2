using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [RequireComponent(typeof(Collider))]
    public abstract class DetectionCollider<C> : MonoBehaviour
        where C : Collider
    {
        protected new C collider;
        [SerializeField] private ObjectContactEvent triggerEnterEvent;
        [SerializeField] private ContactEvent triggerExitEvent, triggerStayEvent;

        private void Awake()
        {
            collider = GetComponent<C>();
        }

        protected abstract ContactData GetContact(Collider other);

        private void OnTriggerEnter(Collider other)
        {
            triggerEnterEvent?.Invoke(this, GetContact(other));
        }

        private void OnTriggerExit(Collider other)
        {
            triggerExitEvent?.Invoke(GetContact(other));
        }

        private void OnTriggerStay(Collider other)
        {
            triggerStayEvent?.Invoke(GetContact(other));
        }

    }
}
