using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace HDV
{
    public abstract class UIViewModel : MonoBehaviour
    {
        public abstract object BindProperty(string propertyName, Delegate notifyDelegate = null);
        public abstract object BindCollectionProperty(string propertyName, Delegate addDelegate, Delegate removeDelegate);
    }

    public abstract class UIViewModel<M> : UIViewModel
        where M : class
    {
        [SerializeField] protected M model;
        public M Model => model;


        private void Awake()
        {
            if (model != null)
                BindModel(model);
        }

        public void BindModel(M model)
        {
            Debug.AssertIsNotNull(model);
            ClearPreviousBind();
            this.model = model;
            OnBindModel(model);
        }

        protected virtual void ClearPreviousBind() { }

        protected abstract void OnBindModel(M model);

        public override object BindProperty(string propertyName, Delegate notifyDelegate = null)
        {
            var p = GetType().UnderlyingSystemType.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            Debug.AssertIsNotNull(p);

            if(notifyDelegate != null)
            {
                var e = p.PropertyType.GetEvent("ChangeEvent", BindingFlags.Public | BindingFlags.Instance);
                Debug.AssertIsNotNull(e);
                e.AddEventHandler(p.GetValue(this), notifyDelegate);
            }

            return p.GetValue(this);
        }

        public override object BindCollectionProperty(string propertyName, Delegate addNotifyDelegate, Delegate removeNotifyDelegate)
        {
            var p = GetType().UnderlyingSystemType.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            Debug.AssertIsNotNull(p);

            var t = p.PropertyType;
            var ae = t.GetEvent("AddEvent", BindingFlags.Public | BindingFlags.Instance);
            Debug.AssertIsNotNull(ae);
            var re = t.GetEvent("RemoveEvent", BindingFlags.Public | BindingFlags.Instance);
            Debug.AssertIsNotNull(re);

            ae.AddEventHandler(p.GetValue(this), addNotifyDelegate);
            re.AddEventHandler(p.GetValue(this), removeNotifyDelegate);
            /*ae.SetValue(p.GetValue(this), addNotifyDelegate);
            re.SetValue(p.GetValue(this), removeNotifyDelegate);*/
            return p.GetValue(this);
        }

    }
}
