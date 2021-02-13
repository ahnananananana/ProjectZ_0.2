using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [RequireComponent(typeof(SphereCollider))]
    public class DetectionSphereCollider : DetectionCollider<SphereCollider>
    {
        protected override ContactData GetContact(Collider other)
        {
            Vector3 point;
            Vector3 normal = (other.bounds.center - collider.bounds.center).normalized;
            if (collider.bounds.Contains(other.bounds.center))
            {
                point = other.bounds.center;
            }
            else
            {
                point = collider.bounds.center + normal * collider.radius;
            }

            return new ContactData(point, normal, other, collider);
        }
    }
}
