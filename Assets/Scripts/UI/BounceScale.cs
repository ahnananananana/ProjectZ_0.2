using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public class BounceScale : MonoBehaviour 
    {
        [SerializeField] private Vector3 maxScale, minScale;
        [SerializeField] private float transitionTime;
        private float scaleSpeed;
        private IEnumerator coroutine;

        private void Awake()
        {
            float dis = Vector3.Distance(maxScale, transform.localScale) + Vector3.Distance(maxScale, minScale);
            scaleSpeed = dis / transitionTime;
        }

        public void StartBounce()
        {
            if (coroutine != null)
                return;
            coroutine = Bounce();
            StartCoroutine(coroutine);
        }

        private IEnumerator Bounce()
        {
            while (Vector3.SqrMagnitude(maxScale - transform.localScale) > .001f)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, maxScale, scaleSpeed * Time.deltaTime);
                yield return null;
            }

            while (Vector3.SqrMagnitude(minScale - transform.localScale) > .001f)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, minScale, scaleSpeed * Time.deltaTime);
                yield return null;
            }
            transform.localScale = minScale;

            coroutine = null;
        }

    }
}
