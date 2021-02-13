using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace HDV
{
    public class VoidMethodBinder : MethodBinder
    {
        public override void ExecuteMethod()
        {
            methodInfo.Invoke(methodComponent, new object[] { });
        }
    }
}
