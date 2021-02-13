using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HDV
{
    [CreateAssetMenu]
    public class UIWindowHistoryManager : ScriptableObject
    {
        private static UIWindowHistoryManager instance;
        private Stack<UIWindow> windowHistory = new Stack<UIWindow>();
        [SerializeField] private InputReader inputReader;
        public static UIWindowHistoryManager Current
        {
            get
            {
                if (instance == null)
                {
                    instance = Resources.Load<UIWindowHistoryManager>("Singletons/UIWindowHistoryManager");
                }
                return instance;
            }
        }

#if UNITY_EDITOR
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Init()
        {
            if (instance != null)
            {
                instance.OnEnable();
            }
        }
#endif

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

        private void OnEnable()
        {
            if (instance == null)
                instance = this;
            windowHistory.Clear();
            inputReader.BackButtonEvent += CloseTopWindow;
        }

        private void OnDisable()
        {
            inputReader.BackButtonEvent -= CloseTopWindow;
        }

        public void PushWindow(UIWindow window)
        {
            if (window.IsHideLastWindow && windowHistory.Count > 0)
                windowHistory.Peek().Hide();
            windowHistory.Push(window);
        }

        public void PopWindow(UIWindow window)
        {
            if (windowHistory.Count > 0 && windowHistory.Peek() == window)
            {
                windowHistory.Pop();
                if (windowHistory.Count > 0)
                    windowHistory.Peek().Show();
            }
        }

        public void CloseTopWindow()
        {
            if(windowHistory.Count > 0)
            {
                windowHistory.Peek().Close();
            }
        }
    }
}
