using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [CreateAssetMenu(menuName = "Conditions/CompareFloatCondition")]
    public class CompareFloatConditionSO : StateDynamicConditionSO<CompareFloatCondition>
    {
        [SerializeField] Comparator comparator;
        [SerializeField] float threshold;

        public Comparator Comparator => comparator;
        public float Threshold => threshold;
    }

    public class CompareFloatCondition : StateValueCondition<EventFloat, float>
    {
        private CompareFloatConditionSO SO => so as CompareFloatConditionSO;

        protected override void OnChangeValue(float changedValue)
        {
            if (Calculator.Compare(changedValue, SO.Threshold, SO.Comparator))
                IsTrue = true;
            else
                IsTrue = false;
        }
    }
}
