using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace HDV
{
    public class PointerUpPropagator : MonoBehaviour, IPointerUpHandler
    {
        [SerializeField] private PointerEvent pointerUpEvent;

        public void OnPointerUp(PointerEventData eventData)
        {
            pointerUpEvent?.Invoke(eventData);
        }
    }
}

