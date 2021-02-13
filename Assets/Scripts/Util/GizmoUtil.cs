using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoUtil : MonoBehaviour
{
    public static void DrawCollider(Transform transform, Collider collider)
    {
        if(collider is BoxCollider)
        {
            DrawBoxCollider(transform, collider as BoxCollider);
        }
    }

    private static void DrawBoxCollider(Transform transform, BoxCollider collider)
    {
        Vector3 center = transform.position + collider.center;
        var min = center - collider.size / 2;
        var max = center + collider.size / 2;
        Vector3 p1, p2, p3;
        p1 = min + new Vector3(collider.size.x, 0, 0);
        p2 = min + new Vector3(0, collider.size.y, 0);
        p3 = min + new Vector3(0, 0, collider.size.z);
        Gizmos.DrawLine(min, p1);
        Gizmos.DrawLine(min, p2);
        Gizmos.DrawLine(min, p3);

        Vector3 p4, p5, p6;
        p4 = max - new Vector3(collider.size.x, 0, 0);
        p5 = max - new Vector3(0, collider.size.y, 0);
        p6 = max - new Vector3(0, 0, collider.size.z);
        Gizmos.DrawLine(max, p4);
        Gizmos.DrawLine(max, p5);
        Gizmos.DrawLine(max, p6);

        Gizmos.DrawLine(p4, p2);
        Gizmos.DrawLine(p4, p3);
        Gizmos.DrawLine(p5, p1);
        Gizmos.DrawLine(p5, p3);
        Gizmos.DrawLine(p6, p1);
        Gizmos.DrawLine(p6, p2);
    }
}
