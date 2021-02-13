using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [CreateAssetMenu(menuName = "Conditions/CompareVector2Condition")]
    public class CompareVector2ConditionSO : StateDynamicConditionSO<CompareVector2Condition>
    {
        [SerializeField] Comparator comparatorX;
        [SerializeField] float thresholdX;
        [SerializeField] Comparator comparatorY;
        [SerializeField] float thresholdY;

        public Comparator ComparatorX => comparatorX;
        public float ThresholdX => thresholdX;
        public Comparator ComparatorY => comparatorY;
        public float ThresholdY => thresholdY;
    }

    public class CompareVector2Condition : StateValueCondition<EventVector2, Vector2>
    {
        private CompareVector2ConditionSO SO => so as CompareVector2ConditionSO;

        protected override void OnChangeValue(Vector2 changedValue)
        {
            if (Calculator.Compare(changedValue.x, SO.ThresholdX, SO.ComparatorX) && Calculator.Compare(changedValue.y, SO.ThresholdY, SO.ComparatorY))
                IsTrue = true;
            else
                IsTrue = false;
        }

    }
}
