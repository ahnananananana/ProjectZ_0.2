using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [CreateAssetMenu(menuName = "Conditions/CompareBoolCondition")]
    public class CompareBoolConditionSO : StateDynamicConditionSO<CompareBoolCondition>
    {
        [SerializeField] private bool triggerIfTrue;

        public bool TriggerIfTrue => triggerIfTrue;
    }

    public class CompareBoolCondition : StateValueCondition<EventBool, bool>
    {
        private CompareBoolConditionSO SO => (CompareBoolConditionSO)so;

        protected override void OnChangeValue(bool changedValue)
        {
            if (SO.TriggerIfTrue == changedValue)
                IsTrue = true;
            else
                IsTrue = false;
        }
    }
}
