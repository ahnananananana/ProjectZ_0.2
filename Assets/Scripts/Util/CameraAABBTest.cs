using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HDV
{
    public class CameraAABBTest : MonoBehaviour
    {
        [SerializeField] private bool enable;
        [SerializeField] private Collider[] colliders;

        private void OnDrawGizmos()
        {
            if (!enable)
                return;
            var camera = GetComponent<Camera>();
            Gizmos.color = Color.blue;
            int count = 0;
            for (int i = 0; i < colliders.Length; ++i)
            {
                if (GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(camera), colliders[i].bounds))
                {
                    GizmoUtil.DrawCollider(colliders[i].transform, colliders[i]);
                    ++count;
                }
            }
            Debug.Log(count);
        }
    }
}
