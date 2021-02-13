using System;

using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace HDV
{
    public abstract class MethodBinder : MonoBehaviour
    {
        [SerializeField] protected Object methodComponent;
        [SerializeField] private string methodName;
        protected MethodInfo methodInfo;

        private void Awake()
        {
            Debug.AssertIsNotNull(methodComponent);
            Debug.AssertIsNotEmpty(methodName);
            Bind(methodComponent, methodName);
        }

        public void Bind(Object methodComponent, string methodName)
        {
            /*var viewModelNames = viewModelName.Split('.');
            object root = viewModel;
            for (int i = 0; i < viewModelNames.Length; ++i)
            {
                var propertyInfo = root.GetType().GetProperty(viewModelNames[i], BindingFlags.Public | BindingFlags.Instance);
                Debug.AssertIsNotNull(propertyInfo);
                this.viewModel = propertyInfo.GetValue(root) as UIViewModel;
                Debug.AssertIsNotNull(this.viewModel);
                root = this.viewModel;
            }*/

            methodInfo = methodComponent.GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);
            Debug.AssertIsNotNull(methodInfo);
            OnBind(methodComponent);
        }

        protected virtual void OnBind(Object methodComponent) { }

        public abstract void ExecuteMethod();
    }
}
