using System;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public class SystemBinder : MonoBehaviour
    {
        private Dictionary<Type, IStateComponent> systems = new Dictionary<Type, IStateComponent>();

        public void Init()
        {
            systems.Clear();
            FindSystems(transform);
        }

        private void FindSystems(Transform root)
        {
            var comps = root.GetComponents<IStateComponent>();
            for (int i = 0; i < comps.Length; ++i)
            {
                Type type = comps[i].GetType();
                systems.Add(type, comps[i]);
                Type baseType = type.BaseType;
                while(baseType != typeof(MonoBehaviour))
                {
                    if (!baseType.IsGenericType)
                        systems[baseType] = comps[i];
                    baseType = baseType.BaseType;
                }
            }

            for (int i = 0; i < root.childCount; ++i)
                FindSystems(root.GetChild(i));
        }

        public IStateComponent GetSystem(Type type)
        {
            Debug.AssertContainKey(systems, type);
            return systems[type];
        }

        public T GetSystem<T>() where T : class, IStateComponent
        {
            Debug.AssertContainKey(systems, typeof(T)); 
            return systems[typeof(T)] as T;
        }
    }
}
