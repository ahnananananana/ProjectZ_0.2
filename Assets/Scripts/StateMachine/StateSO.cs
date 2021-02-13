using System;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [CreateAssetMenu(menuName = "State")]
    public class StateSO : ScriptableObject
    {
        [SerializeField] private bool isRetriggerable;
        [SerializeField] private int executePriority;
        [SerializeField] private StateObjectChannel bindedChannel;
        [SerializeField] private StateConditionSO[] activateConditionSOs, deactivateConditionSOs;
        [SerializeField] private StateSO[] blockingStateSOs, blockingOnlyStateSOs, requiredStateSOs;
        [SerializeField] private StateActionSO[] actionSOs;

        public bool IsRetriggerable => isRetriggerable;
        public int ExecutePriority => executePriority;
        public StateObjectChannel BindedChannel => bindedChannel;

        public State CreateState(IStateObject stateObject, ref int bitMask, SystemBinder systemBinder, Dictionary<StateSO, State> createdStates, 
            Dictionary<StateCondition, State> createdConditions/*, 
            Action<StateCondition> onTriggerActivateMethod, Action<StateCondition> onTriggerDeactivateMethod*/)
        {
            if (createdStates.TryGetValue(this, out State state))
                return state;

            state = new State(stateObject, ref bitMask, this);

            createdStates.Add(this, state);

            var activateOnlyConditions = CreateConditions(activateConditionSOs, state, systemBinder, createdConditions/*, onTriggerActivateMethod*/);
            var deactivateOnlyConditions = CreateConditions(deactivateConditionSOs, state, systemBinder, createdConditions/*, onTriggerDeactivateMethod*/);

            var blockingStates = CreateStates(stateObject, blockingStateSOs, ref bitMask, systemBinder, createdStates, createdConditions/*, onTriggerActivateMethod, onTriggerDeactivateMethod*/);
            var blockingOnlyStates = CreateStates(stateObject, blockingOnlyStateSOs, ref bitMask, systemBinder, createdStates, createdConditions/*, onTriggerActivateMethod, onTriggerDeactivateMethod*/);
            var requiredStates = CreateStates(stateObject, requiredStateSOs, ref bitMask, systemBinder, createdStates, createdConditions/*, onTriggerActivateMethod, onTriggerDeactivateMethod*/);

            var actions = CreateActions(actionSOs, systemBinder);

            state.Init(activateOnlyConditions, deactivateOnlyConditions,
                blockingStates, blockingOnlyStates, requiredStates,
                actions);

            return state;
        }

        private State[] CreateStates(IStateObject stateObject, StateSO[] stateSOs, ref int bitMask, SystemBinder systemBinder, Dictionary<StateSO, State> createdStates, Dictionary<StateCondition, State> createdConditions/*,
            Action<StateCondition> onTriggerActivateMethod, Action<StateCondition> onTriggerDeactivateMethod*/)
        {
            State[] states = new State[stateSOs.Length];
            for (int i = 0; i < states.Length; ++i)
            {
                var so = stateSOs[i];
                if (createdStates.TryGetValue(so, out State state))
                {
                    states[i] = state;
                }
                else
                {
                    state = so.CreateState(stateObject, ref bitMask, systemBinder, createdStates, createdConditions/*, onTriggerActivateMethod, onTriggerDeactivateMethod*/);
                    states[i] = state;
                }
            }
            return states;
        }

        private StateCondition[] CreateConditions(StateConditionSO[] stateConditionSOs, State state, SystemBinder systemBinder, Dictionary<StateCondition, State> createdConditions/*, Action<StateCondition> triggerMethod*/)
        {
            StateCondition[] conditions = new StateCondition[stateConditionSOs.Length];
            for (int i = 0; i < conditions.Length; ++i)
            {
                conditions[i] = stateConditionSOs[i].CreateCondition(systemBinder);
                //conditions[i].TriggerEvent += triggerMethod;
                createdConditions.Add(conditions[i], state);
            }
            return conditions;
        }

        private StateAction[] CreateActions(StateActionSO[] stateActionSOs, SystemBinder systemBinder)
        {
            StateAction[] stateActions = new StateAction[stateActionSOs.Length];
            for (int i = 0; i < stateActionSOs.Length; ++i)
            {
                stateActions[i] = stateActionSOs[i].CreateAction(systemBinder);
            }
            return stateActions;
        }

    }
}
