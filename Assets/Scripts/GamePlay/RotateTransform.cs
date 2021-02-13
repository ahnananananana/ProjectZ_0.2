using System.Collections;
using System;
using UnityEngine;

namespace HDV
{
    [Serializable]
    public class RotateTransform : MonoBehaviour
    {
        [SerializeField] private bool startOnAwake = true;
        [SerializeField] private float speed;
        [SerializeField] private Vector3 direction;
        private IEnumerator coroutine;

        public float Speed { get => speed; set => speed = value; }
        public Vector3 Direction { get => direction; set => direction = value; }
        public bool StopRotate
        { 
            set
            {
                if (coroutine != null)
                    StopCoroutine(coroutine);

                if (value)
                    transform.localRotation = Quaternion.identity;
            }
        }

        private void Awake()
        {
            if(startOnAwake)
                StartRotate();
        }

        public void StartRotate()
        {
            if (coroutine != null)
                StopCoroutine(coroutine);
            coroutine = Rotate();
            StartCoroutine(coroutine);
        }


        private IEnumerator Rotate()
        {
            while (true)
            {
                transform.Rotate(direction * speed * Time.deltaTime);
                yield return null;
            }
        }
    }
}
