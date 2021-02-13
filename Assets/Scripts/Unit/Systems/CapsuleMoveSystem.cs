using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public class CapsuleMoveSystem : MoveSystem
    {
        [SerializeField] private LayerMask walkableLayer, blockingLayer;
        [SerializeField] private float maxStepHeight, maxStepSlope;
        [SerializeField] private CapsuleCollider capsuleCollider;
        private Vector3 slopeVec;
        private Vector3 previousPos;
#if UNITY_EDITOR
        [SerializeField] private bool setGizmo;
#endif

        protected override void OnMove(Vector3 direction, float speed)
        {
            transform.position = CalculateMovePosition(direction, speed);
        }

        private Vector3 CalculateMovePosition(Vector3 direction, float speed)
        {
            previousPos = transform.position;

            var capsuleCenter = transform.position + capsuleCollider.center;
            var footPoint = capsuleCenter;
            footPoint.y = footPoint.y - capsuleCollider.height * .5f + maxStepHeight;
            //footPoint += capsuleCollider.radius * direction;
            f = footPoint;
            Ray ray = new Ray(footPoint, direction);

            float distance = speed * Time.fixedDeltaTime;

            if (Physics.Raycast(ray, out var hit, distance + capsuleCollider.radius, walkableLayer))
            {
                if (((1 << hit.collider.gameObject.layer) & walkableLayer) > 0)
                {
                    /*a = hit.normal.y >= 0f ?
                        Quaternion.AngleAxis(90f, Vector3.Cross(hit.normal, direction)) * hit.normal :
                        Quaternion.AngleAxis(90f, Vector3.Cross(direction, hit.normal)) * hit.normal;
                    b = hit.point;*/
                    h = hit;

                    slopeVec = hit.normal;
                    slopeVec.y = 0;
                    slopeVec *= -1;

                    slopeVec = Quaternion.AngleAxis(maxStepSlope, Vector3.Cross(slopeVec, hit.normal)) * slopeVec;
                    slopeVec.Normalize();

                    Debug.Log(hit.normal + " " + Vector3.Dot(slopeVec, hit.normal));
                    var dot = Vector3.Dot(slopeVec, hit.normal);
                    if (dot > 0f || Mathf.Approximately(dot, 0f))
                    {
                        Debug.Log(Vector3.Distance(transform.position, hit.point) + " " + distance);
                        if(Vector3.Distance(transform.position, hit.point) - distance < 0.05f)
                        {
                            return hit.point;
                        }
                        else
                        {
                            ray = new Ray(footPoint + direction * distance, Vector3.down);
                            if (Physics.Raycast(ray, out hit, float.MaxValue, walkableLayer))
                            {
                                return hit.point;
                            }
                        }
                    }
                }
            }
            else
            {
                ray = new Ray(footPoint + direction * distance, Vector3.down);
                if(Physics.Raycast(ray, out hit, float.MaxValue, walkableLayer))
                {
                    return hit.point;
                }
            }

            return transform.position;
        }

        private void OnTriggerStay(Collider other)
        {
            if(((1 << other.gameObject.layer) & blockingLayer) > 0)
            {
                transform.position = previousPos;
            }
        }

        RaycastHit h;
        Vector3 f;
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if(setGizmo)
            {
                Gizmos.color = Color.red;
                var capsuleCenter = transform.position + capsuleCollider.center;
                var footPoint = capsuleCenter;
                footPoint.y = footPoint.y - capsuleCollider.height * .5f + maxStepHeight;

                Gizmos.DrawLine(f, f + moveDirection.Value * moveSpeed.Value/* * Time.fixedDeltaTime*/);
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(h.point, h.point + h.normal);
                Gizmos.color = Color.green;
                Gizmos.DrawLine(h.point, h.point + slopeVec);
            }
        }
#endif
    }
}
