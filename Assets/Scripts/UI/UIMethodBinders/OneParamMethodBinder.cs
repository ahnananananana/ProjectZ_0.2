using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public class OneParamMethodBinder : MethodBinder
    {
        [SerializeField] private MonoBehaviour paramComponent;
        [SerializeField] private string paramName;

        private object[] param = new object[1];

        protected override void OnBind(Object methodComponent)
        {
            Debug.AssertIsNotNull(paramComponent);
            Debug.AssertIsNotEmpty(paramName);

            if(paramName.Equals("this"))
            {
                param[0] = paramComponent;
            }
            else
            {
                var pi = paramComponent.GetType().GetProperty(paramName);
                Debug.AssertIsNotNull(pi);
                param[0] = pi.GetValue(paramComponent);
                Debug.AssertIsNotNull(param[0]);
            }
        }

        public override void ExecuteMethod()
        {
            methodInfo.Invoke(methodComponent, param);
        }
    }
}
