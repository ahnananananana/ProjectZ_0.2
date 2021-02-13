using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace HDV
{
    [CreateAssetMenu(menuName = "Conditions/CompareStatCurrentCondition")]
    public class CompareStatCurrentConditionSO : StateConditionSO<CompareStatCurrentCondition>
    {
        [SerializeField] private StatType type;
        [SerializeField] private Comparator comparator;
        [SerializeField] private float threshold;

        public StatType Type => type;
        public Comparator Comparator => comparator;
        public float Threshold => threshold;
    }

    public class CompareStatCurrentCondition : StateCondition
    {
        private CompareStatCurrentConditionSO SO => (CompareStatCurrentConditionSO)so;

        protected override void OnInit(SystemBinder systemBinder)
        {
            var stat = systemBinder.GetSystem<Unit>().UnitData.Stats[SO.Type] as StatCurrent;
            Debug.AssertIsNotNull(stat);
            stat.ChangeCurrentValueEvent += OnChangeValue;
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
