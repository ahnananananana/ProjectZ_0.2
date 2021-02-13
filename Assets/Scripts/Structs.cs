using UnityEngine;

namespace HDV
{
    public struct DamageData
    {
        public object Source { get; private set; }
        public float Damage { get; private set; }

        public DamageData(object source, float damage)
        {
            Source = source;
            Damage = damage;
        }
    }

    public struct HitData
    {
        public DamageData DamageData { get; private set; }
        public Vector3 HitPosition { get; private set; }
        public Quaternion HitRotation { get; private set; }

        public HitData(DamageData damageData, Vector3 hitPosition, Quaternion hitRotation)
        {
            DamageData = damageData;
            HitPosition = hitPosition;
            HitRotation = hitRotation;
        }
    }

    public struct ContactData
    {
        public Vector3 Point { get; private set; }
        public Vector3 Normal { get; private set; }
        public Collider OtherCollider { get; private set; }
        public Collider ThisCollider { get; private set; }

        public ContactData(Vector3 point, Vector3 normal, Collider otherCollider, Collider thisCollider = null)
        {
            Point = point;
            Normal = normal;
            OtherCollider = otherCollider;
            ThisCollider = thisCollider;
        }
    }
}