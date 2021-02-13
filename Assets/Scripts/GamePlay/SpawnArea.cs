using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public class SpawnArea : MonoBehaviour
    {
        public Vector3 GetRandomSpawnPosition()
        {
            Vector3 result = default;
            return result;
        }

        Vector3 test;
        Collider o;

        private void OnTriggerStay(Collider other)
        {
            o = other;
            test = other.ClosestPointOnBounds(transform.position);

        }

        private void OnDrawGizmos()
        {
            if(test != null && o != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(test, .2f);
                Gizmos.color = Color.yellow;
                if(Physics.Raycast(new Ray(test, (o.transform.position - test).normalized),out var hit, 10, LayerMask.GetMask("Player")))
                {
                    Gizmos.DrawSphere(hit.point, .2f);
                }
            }
        }
    }
}
