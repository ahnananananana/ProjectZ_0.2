using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [CreateAssetMenu(menuName = "Conditions/NullCheckCondition")]
    public class NullCheckConditionSO : StateDynamicConditionSO<NullCheckCondition>
    {
        [SerializeField] private bool triggerIfNull;

        public bool TriggerIfNull => triggerIfNull;
    }

    public class NullCheckCondition : StateReferenceCondition<EventReferenceType<object>, object>
    {
        private NullCheckConditionSO SO => so as NullCheckConditionSO;

        protected override void OnChangeValue(object changedValue)
        {
            if ((SO.TriggerIfNull && changedValue == null) ||
                (!SO.TriggerIfNull && changedValue != null))
                IsTrue = true;
            else
                IsTrue = false;
        }
    }
}
