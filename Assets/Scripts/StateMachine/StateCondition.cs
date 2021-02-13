using System;
using System.Reflection;
using UnityEngine;

namespace HDV
{
    public abstract class StateCondition
    {
        protected StateConditionSO so;

        private bool isTrue;
        public bool IsTrue
        {
            get
            {
                bool result = isTrue;
                if (so.AutoReset && result)
                    isTrue = false;
                return result;
            }
            protected set
            {
                bool previous = isTrue;
                isTrue = value;

                if ((isTrue && !previous) ||
                    (isTrue && previous && so.Retriggerable))
                {
                    TriggerEvent?.Invoke(this);
                }
            }
        }

        public event Action<StateCondition> TriggerEvent;

        public void Init(StateConditionSO so, SystemBinder systemBinder)
        {
            this.so = so;

            OnInit(systemBinder);
        }

        protected abstract void OnInit(SystemBinder systemBinder);
    }

    public abstract class StateDynamicCondition : StateCondition
    {
        protected override void OnInit(SystemBinder systemBinder)
        {
            StateDynamicConditionSO SO = (StateDynamicConditionSO)so;

            var systemType = Type.GetType("HDV." + SO.BindSystemName);
            Debug.AssertIsNotNull(systemType);
            var system = systemBinder.GetSystem(systemType);
            PropertyInfo pi = systemType.GetProperty(SO.BindPropertyName);
            Debug.AssertIsNotNull(pi);
            object propertyObj = pi.GetValue(system);
            Debug.AssertIsNotNull(propertyObj);

            OnBindComponent(systemBinder);
            OnBindProperty(propertyObj);
        }

        protected abstract void OnBindProperty(object propertyObj);
        protected virtual void OnBindComponent(SystemBinder systemBinder) { }
    }

    public abstract class StateReferenceCondition<E, V> : StateDynamicCondition
        where E : EventReferenceType<V>
        where V : class, new()
    {
        protected sealed override void OnBindProperty(object propertyObj)
        {
            E property = propertyObj as E;
            Debug.AssertIsNotNull(property);
            property.ChangeEvent += OnChangeValue;
            OnChangeValue(property.Value);
        }

        protected abstract void OnChangeValue(V changedValue);
    }

    public abstract class StateValueCondition<E, V> : StateDynamicCondition
        where E : EventValueType<V>
        where V : struct
    {
        protected sealed override void OnBindProperty(object propertyObj)
        {
            E property = propertyObj as E;
            Debug.AssertIsNotNull(property);
            property.ChangeEvent += OnChangeValue;
            OnChangeValue(property.Value);
        }

        protected abstract void OnChangeValue(V changedValue);
    }
}
