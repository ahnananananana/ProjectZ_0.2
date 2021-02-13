using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace HDV
{
    public class PointerClickPropagator : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] PointerEvent pointerClickEvent;

        public void OnPointerClick(PointerEventData eventData)
        {
            pointerClickEvent?.Invoke(eventData);
        }
    }
}

