using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class RagdollElement : MonoBehaviour
    {
        [SerializeField] private new Collider collider;
        [SerializeField] private new Rigidbody rigidbody;
        [SerializeField] private Joint joint;

        //private Rigidbody connectedBody;

        public Collider Collider => collider;
        public Rigidbody Rigidbody => rigidbody;
        public Joint Joint => joint;

        private void Awake()
        {
            collider = GetComponent<Collider>();
            rigidbody = GetComponent<Rigidbody>();
            joint = GetComponent<Joint>();
            //connectedBody = joint == null ? null : joint.connectedBody;
        }

        public void SetActive(bool set)
        {
            collider.enabled = set;
            rigidbody.isKinematic = !set;
            /*if (set) 
                rigidbody.WakeUp();
            else 
                rigidbody.Sleep();*/

            /*if (joint != null)
                joint.connectedBody = set ? connectedBody : null;*/
        }
    }
}
