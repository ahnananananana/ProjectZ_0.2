using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HDV
{
    public class DestructableObject : MonoBehaviour, IHittable, IDestructable
    {
        [SerializeField] private bool isDestructed;
        [SerializeField] private float hp;
        [SerializeField] private ParticleSystem hitVFX, destructVFX;
        [SerializeField] private SubHittablePart[] subHittableParts;
        [SerializeField] private UnityEvent destructEvent;
        private new Rigidbody rigidbody;

        public float HP { get => hp; set => hp = value; }

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();

            for (int i = 0; i < subHittableParts.Length; ++i)
                subHittableParts[i].MainHittable = this;
        }

        public void Destruct()
        {
            rigidbody.isKinematic = false;
            for (int i = 0; i < subHittableParts.Length; ++i)
                subHittableParts[i].Rigidbody.isKinematic = false;
            destructEvent?.Invoke();
            Instantiate(destructVFX, transform.position, transform.rotation);

            isDestructed = true;
        }

        public float TakeDamage(DamageData damageData, ContactData contactData)
        {
            Instantiate(hitVFX, contactData.Point, Quaternion.LookRotation(contactData.Normal));
            hp -= damageData.Damage;
            if (!isDestructed && hp - damageData.Damage <= 0)
            {
                Destruct();
            }

            return damageData.Damage;
        }
    }
}
