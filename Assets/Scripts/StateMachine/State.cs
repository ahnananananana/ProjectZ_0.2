using System;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [Serializable]
    public class State : IComparable<State>, IEquatable<State>
    {
        private IStateObject stateObject;
        private int bitMask;
        private StateSO so;
        private StateCondition[] activateConditions, deactivateConditions;
        private int blockingBitMask, requiredBitMask;
        private List<State> terminateOnActivateStates = new List<State>();
        private List<State> terminateOnDeactivateStates = new List<State>();
        private Action activateAction, updateAction, fixedUpdateAction, deactivateAction;

        public string Name => so.name;
        public int BitMask => bitMask;
        public bool IsRetriggerable => so.IsRetriggerable;
        public int ExecutePriority => so.ExecutePriority;
        public int BlockingBitMask => blockingBitMask;
        public int RequiredBitMask => requiredBitMask;
        public List<State> TerminateOnDeactivateStates => terminateOnDeactivateStates;
        public List<State> TerminateOnActivateStates => terminateOnActivateStates;
        public StateSO SO => so;

        public State(IStateObject stateObject, ref int bitMask, StateSO so)
        {
            this.stateObject = stateObject;
            this.bitMask = bitMask;
            bitMask <<= 1;

            this.so = so;
        }

        public void Init(StateCondition[] activateConditions, StateCondition[] deactivateConditions,
            State[] blockingStates, State[] blockingOnlyStates, State[] requiredStates,
            StateAction[] actions)
        {
            this.activateConditions = activateConditions;
            this.deactivateConditions = deactivateConditions;

            for(int i = 0; i < blockingStates.Length; ++i)
            {
                blockingBitMask |= blockingStates[i].bitMask;
                blockingStates[i].terminateOnActivateStates.Add(this);
            }
            for (int i = 0; i < blockingOnlyStates.Length; ++i)
            {
                blockingBitMask |= blockingOnlyStates[i].bitMask;
            }
            for (int i = 0; i < requiredStates.Length; ++i)
            {
                requiredBitMask |= requiredStates[i].bitMask;
                requiredStates[i].terminateOnDeactivateStates.Add(this);
            }

            for(int i = 0; i < actions.Length; ++i)
            {
                var action = actions[i];
                if (action.HasActivate)
                    activateAction += action.OnStateActivate;
                if (action.HasDeactivate)
                    deactivateAction += action.OnStateDeactivate;
                if (action.HasUpdate)
                    updateAction += action.OnStateUpdate;
                if (action.HasFixedUpdate)
                    fixedUpdateAction += action.OnStateFixedUpdate;
            }
        }

        public bool CanbeActivated()
        {
            for(int i = 0; i < activateConditions.Length; ++i)
                if (!activateConditions[i].IsTrue)
                    return false;

            return true;
        }

        public bool CanbeDeactivated()
        {
            for (int i = 0; i < deactivateConditions.Length; ++i)
                if (deactivateConditions[i].IsTrue)
                    return true;

            return false;
        }

        public void Activate()
        {
            activateAction?.Invoke();
            if(so.BindedChannel != null)
                so.BindedChannel.Raise(stateObject);
        }

        public void Deactivate()
        {
            deactivateAction?.Invoke();
        }

        public void Update()
        {
            updateAction?.Invoke();
        }

        public void FixedUpdate()
        {
            fixedUpdateAction?.Invoke();
        }

        public int CompareTo(State other)
        {
            if (so.ExecutePriority < other.so.ExecutePriority)
                return 1;
            else if (so.ExecutePriority == other.so.ExecutePriority)
                return 0;
            else
                return -1;
        }

        public bool Equals(State other)
        {
            return bitMask == other.bitMask;
        }
    }
}
