using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HDV
{
    [RequireComponent(typeof(Canvas), typeof(GraphicRaycaster))]
    public class UIWindow : MonoBehaviour
    {
        [SerializeField] private bool isRoot, isHideLastWindow;
        private Canvas canvas;
        private GraphicRaycaster graphicRaycaster;

        private UIAnimator uiAnimator;

        public UIWindowEvent OpenEvent, CloseEvent, ShowEvent, HideEvent;

        public bool IsHideLastWindow => isHideLastWindow;

        private void Awake()
        {
            canvas = GetComponent<Canvas>();
            graphicRaycaster = GetComponent<GraphicRaycaster>();

            Debug.AssertIsNotNull(canvas);
            Debug.AssertIsNotNull(graphicRaycaster);

            uiAnimator = GetComponent<UIAnimator>();
            
            if (uiAnimator != null)
            {
                uiAnimator.OpenEvent += OnOpenWindow;
                uiAnimator.CloseEvent += OnCloseWindow;
            }
        }

        private void Start()
        {
            if (isRoot)
                Open();
        }

        private void OnDestroy()
        {
            if (uiAnimator != null)
            {
                uiAnimator.OpenEvent -= OnOpenWindow;
                uiAnimator.CloseEvent -= OnCloseWindow;
            }
        }

        public void Open()
        {
            UIWindowHistoryManager.Current.PushWindow(this);
            OpenEvent?.Invoke(this);
            canvas.enabled = true;
            graphicRaycaster.enabled = true;
            if (uiAnimator != null)
            {
                uiAnimator.PlayOpen();
            }
            else
            {
                OnOpenWindow();
            }
        }

        public void Close()
        {
            CloseEvent?.Invoke(this);
            if (uiAnimator != null)
            {
                uiAnimator.PlayClose();
            }
            else
            {
                OnCloseWindow();
            }
        }

        public void Show()
        {
            canvas.enabled = true;
            graphicRaycaster.enabled = true;
            ShowEvent?.Invoke(this);
        }

        public void Hide()
        {
            canvas.enabled = false;
            graphicRaycaster.enabled = false;
            HideEvent?.Invoke(this);
        }

        private void OnOpenWindow()
        {
        }

        private void OnCloseWindow()
        {
            if (!isRoot)
            {
                UIWindowHistoryManager.Current.PopWindow(this);
                canvas.enabled = false;
                graphicRaycaster.enabled = false;
            }
        }

    }
}
