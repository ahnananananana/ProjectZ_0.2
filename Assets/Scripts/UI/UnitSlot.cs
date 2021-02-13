using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace HDV
{
    public class UnitSlot : MonoBehaviour, IPointerClickHandler, IViewTemplate<PlayerUnitDataViewModel>, IMovableUI, IUIAnimator
    {
        [SerializeField] private RectTransform rectTransform, container;
        private PlayerUnitDataViewModel viewModel;
        private UIAnimator uiAnimator;

        [SerializeField] private PlayerUnitDataEvent clickEvent;

        public PlayerUnitDataViewModel Model => viewModel;
        public RectTransform RectTransform => rectTransform;
        public RectTransform Container => container;

        public UIAnimator UIAnimator => uiAnimator;

        public void Init(PlayerUnitDataViewModel model)
        {
            rectTransform = GetComponent<RectTransform>();
            uiAnimator = GetComponent<UIAnimator>();
            viewModel = model;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            //clickEvent?.Invoke(viewModel.Model);
        }
    }
}
