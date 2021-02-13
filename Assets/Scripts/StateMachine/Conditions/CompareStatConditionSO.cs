using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace HDV
{
    [CreateAssetMenu(menuName = "Conditions/CompareStatCondition")]
    public class CompareStatConditionSO : StateConditionSO<CompareStatCondition>
    {
        [SerializeField] private StatType type;
        [SerializeField] private Comparator comparator;
        [SerializeField] private float threshold;

        public StatType Type => type;
        public Comparator Comparator => comparator;
        public float Threshold => threshold;
    }

    public class CompareStatCondition : StateCondition
    {
        private CompareStatConditionSO SO => (CompareStatConditionSO)so;

        protected override void OnInit(SystemBinder systemBinder)
        {
            var stat = systemBinder.GetSystem<Unit>().UnitData.Stats[SO.Type];
            stat.ChangeValueEvent += OnChangeValue;
        }

        private void OnChangeValue(float changedValue)
        {
            if (Calculator.Compare(changedValue, SO.Threshold, SO.Comparator))
                IsTrue = true;
            else
                IsTrue = false;
        }
    }
}
