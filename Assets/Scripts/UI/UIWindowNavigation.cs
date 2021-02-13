using System;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [CreateAssetMenu]
    public class UIWindowNavigation : ScriptableObject
    {
        private Dictionary<string, UIWindow> registeredWindows = new Dictionary<string, UIWindow>();

        private static UIWindowNavigation instance;
        public static UIWindowNavigation Current => instance;

#if UNITY_EDITOR
        [RuntimeInitializeOnLoadMethod]
        static void Init()
        {
            if(instance != null)
                instance.OnEnable();
        }
#endif

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                DestroyImmediate(this, true);
        }

        private void OnEnable()
        {
            if (instance == null)
                instance = this;
            registeredWindows.Clear();
        }

        public void Register(UIWindow window)
        {
            Debug.AssertNotContainKey(registeredWindows, window.name);
            registeredWindows.Add(window.name, window);
        }

        public void UnRegister(UIWindow window)
        {
            registeredWindows.Remove(window.name);
        }

        public void Open(string windowName)
        {
            if (registeredWindows.TryGetValue(windowName, out var window))
                window.Open();
        }
    }
}
