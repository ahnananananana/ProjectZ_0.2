using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HDV
{
    public class ClickScaler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Vector3 pointerDownScale, pointerUpScale;
        [SerializeField] private float downSpeed, upSpeed;
        private IEnumerator coroutine;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (coroutine != null)
                StopCoroutine(coroutine);
            coroutine = ScaleTo(pointerDownScale, downSpeed);
            StartCoroutine(coroutine);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (coroutine != null)
                StopCoroutine(coroutine);
            coroutine = ScaleTo(pointerUpScale, upSpeed);
            StartCoroutine(coroutine);
        }

        private IEnumerator ScaleTo(Vector3 targetScale, float speed)
        {
            while(Vector3.Distance(transform.localScale, targetScale) > .001f)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, targetScale, speed * Time.deltaTime);
                yield return null;
            }
            transform.localScale = targetScale;
            coroutine = null;
        }

    }

}