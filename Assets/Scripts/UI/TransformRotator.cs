using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public class TransformRotator : MonoBehaviour
    {

        public void Rotate(Vector3 targetRotation)
        {

        }

        public void RotateZ(float angle)
        {
            var rot = transform.localEulerAngles;
            rot.z += angle;
            transform.localEulerAngles = rot;
        }

        public void SetRotationZ(float angle)
        {
            var rot = transform.localEulerAngles;
            rot.z = angle;
            transform.localEulerAngles = rot;
        }

    }
}
