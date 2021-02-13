
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public abstract class StateConditionSO : ScriptableObject
    {
        [SerializeField] private bool retriggerable, autoReset;
        public bool Retriggerable => retriggerable;
        public bool AutoReset => autoReset;
        public abstract StateCondition CreateCondition(SystemBinder systemBinder);
    }

    public abstract class StateDynamicConditionSO : StateConditionSO
    {
        [SerializeField] protected string bindSystemName;
        [SerializeField] protected string bindPropertyName;

        public string BindSystemName => bindSystemName;
        public string BindPropertyName => bindPropertyName;
    }

    public abstract class StateDynamicConditionSO<T> : StateDynamicConditionSO where T : StateCondition, new()
    {
        public sealed override StateCondition CreateCondition(SystemBinder systemBinder)
        {
            T condition = new T();

            condition.Init(this, systemBinder);
            return condition;
        }
    }

    public abstract class StateConditionSO<T> : StateConditionSO where T : StateCondition, new()
    {
        public sealed override StateCondition CreateCondition(SystemBinder systemBinder)
        {
            T condition = new T();

            condition.Init(this, systemBinder);
            return condition;
        }
    }
}
