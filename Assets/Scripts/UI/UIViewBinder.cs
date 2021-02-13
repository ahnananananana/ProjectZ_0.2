using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace HDV
{
    public abstract class UIViewBinder : MonoBehaviour
    {
        [SerializeField] protected UIViewModel viewModel;
        [SerializeField] protected string bindPropertyName;
        protected abstract void Bind(UIViewModel viewModel);
    }

    public abstract class UIViewBinder<T> : UIViewBinder where T : Component
    {
        [SerializeField] protected T bindView;

        private void Start()
        {
            if(bindView == null)
            {
                bindView = GetComponent<T>();
                Debug.AssertIsNotNull(bindView);
            }

            /*var viewModelNames = viewModelName.Split('.');
            object root = viewModel;
            for(int i = 0; i < viewModelNames.Length; ++i)
            {
                var propertyInfo = root.GetType().UnderlyingSystemType.GetProperty(viewModelNames[i], BindingFlags.Public | BindingFlags.Instance);
                Debug.AssertIsNotNull(propertyInfo);
                viewModel = propertyInfo.GetValue(root) as UIViewModel;
                Debug.AssertIsNotNull(viewModel);
                root = viewModel;
            }*/
            Debug.AssertIsNotNull(viewModel);

            Bind(viewModel);
        }

    }
}
