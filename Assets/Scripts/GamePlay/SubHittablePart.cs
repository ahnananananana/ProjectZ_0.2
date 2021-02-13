using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public class SubHittablePart : MonoBehaviour, IHittable
    {
        [SerializeField] private IHittable mainHittable;
        [SerializeField] private bool vfxOverride;
        [SerializeField] private ParticleSystem hitVFX;
        private new Rigidbody rigidbody;

        public IHittable MainHittable { get => mainHittable; set => mainHittable = value; }
        public Rigidbody Rigidbody => rigidbody;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        public float TakeDamage(DamageData damageData, ContactData contactData)
        {
            if(vfxOverride && hitVFX != null)
            {
                Instantiate(hitVFX, contactData.Point, Quaternion.LookRotation(contactData.Normal));
            }
            return mainHittable.TakeDamage(damageData, contactData);
        }
    }
}
