using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [CreateAssetMenu(menuName = "Conditions/CompareVector3Condition")]
    public class CompareVector3ConditionSO : StateDynamicConditionSO<CompareVector3Condition>
    {
        [SerializeField] Comparator comparatorX;
        [SerializeField] float thresholdX;
        [SerializeField] Comparator comparatorY;
        [SerializeField] float thresholdY;
        [SerializeField] Comparator comparatorZ;
        [SerializeField] float thresholdZ;

        public Comparator ComparatorX => comparatorX;
        public float ThresholdX => thresholdX;
        public Comparator ComparatorY => comparatorY;
        public float ThresholdY => thresholdY;
        public Comparator ComparatorZ => comparatorZ;
        public float ThresholdZ => thresholdZ;
    }

    public class CompareVector3Condition : StateValueCondition<EventVector3, Vector3>
    {
        private CompareVector3ConditionSO SO => so as CompareVector3ConditionSO;

        protected override void OnChangeValue(Vector3 changedValue)
        {
            if (Calculator.Compare(changedValue.x, SO.ThresholdX, SO.ComparatorX) && 
                Calculator.Compare(changedValue.y, SO.ThresholdY, SO.ComparatorY) &&
                Calculator.Compare(changedValue.z, SO.ThresholdY, SO.ComparatorZ))
                IsTrue = true;
            else
                IsTrue = false;
        }

    }
}
