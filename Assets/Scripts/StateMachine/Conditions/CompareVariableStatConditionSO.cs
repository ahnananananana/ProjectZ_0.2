using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace HDV
{
    [CreateAssetMenu(menuName = "Conditions/CompareVariableStatCondition")]
    public class CompareVariableStatConditionSO : StateDynamicConditionSO<CompareVariableStatCondition>
    {
        [SerializeField] private StatType type;
        [SerializeField] private Comparator comparator;

        public StatType Type => type;
        public Comparator Comparator => comparator;
    }

    public class CompareVariableStatCondition : StateValueCondition<EventFloat, float>
    {
        private CompareVariableStatConditionSO SO => so as CompareVariableStatConditionSO;

        private Stat stat;

        protected override void OnBindComponent(SystemBinder systemBinder)
        {
            stat = systemBinder.GetSystem<Unit>().UnitData.Stats[SO.Type];
        }

        protected override void OnChangeValue(float changedValue)
        {
            if (Calculator.Compare(stat.Value, changedValue, SO.Comparator))
                IsTrue = true;
            else
                IsTrue = false;
        }
    }
}

