using System;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public class StateMachine : MonoBehaviour
    {
        [SerializeField] private StateSO[] registeredStateSOs;
        private State[] registeredStates;
        private int currentStateBitMask;
        private List<State> currentStates = new List<State>();
        private Dictionary<StateCondition, State> conditionStates = new Dictionary<StateCondition, State>();
        private List<State> pendingActivatedStates = new List<State>();
        private List<State> pendingDeactivatedStates = new List<State>();

        public List<State> CurrentStates => currentStates;

        //초기화는 다른 곳에서 순서대로 할 수 있게 해야
        public void Init(IStateObject stateObject, SystemBinder systemBinder)
        {
            Debug.AssertIsNotNull(registeredStateSOs);

            int bitMask = 1;
            Dictionary<StateSO, State> createdStates = new Dictionary<StateSO, State>();
            for(int i = 0; i < registeredStateSOs.Length; ++i)
            {
                var so = registeredStateSOs[i];
                so.CreateState(stateObject, ref bitMask, systemBinder, createdStates, conditionStates);
            }

            registeredStates = new State[createdStates.Count];
            createdStates.Values.CopyTo(registeredStates, 0);

            for (int i = 0; i < registeredStates.Length; ++i)
                TryActivateState(registeredStates[i], false);
        }

        private bool CanbeActivated(State state, int currentStateBitMask, bool allowRetrigger = true)
        {
            if (!state.CanbeActivated() ||
                (currentStateBitMask & state.BlockingBitMask) > 0 ||
                ((currentStateBitMask & state.RequiredBitMask) != state.RequiredBitMask) ||
                ((currentStateBitMask & state.BitMask) > 0 && !state.IsRetriggerable) ||
                ((currentStateBitMask & state.BitMask) > 0 && state.IsRetriggerable && !allowRetrigger))
                return false;
            else
                return true;
        }

        private bool CanbeDeactivated(State state, int currentStateBitMask, bool isTerminating = false)
        {
            if ((currentStateBitMask & state.BitMask) == 0 || 
                (!isTerminating && !state.CanbeDeactivated()))
                return false;
            else
                return true;
        }

        private bool TryActivateState(State state, bool allowRetrigger = true)
        {
            if (!CanbeActivated(state, currentStateBitMask, allowRetrigger))
                return false;

            if ((currentStateBitMask & state.BitMask) == 0)
            {
                currentStates.Add(state);
            }

            currentStateBitMask |= state.BitMask;
            //state.Activate();
            pendingActivatedStates.Add(state);

            for (int i = 0; i < state.TerminateOnActivateStates.Count; ++i)
                TryDeactivateState(state.TerminateOnActivateStates[i], true);

            return true;
        }

        private bool TryDeactivateState(State state, bool isTerminating = false)
        {
            if (!CanbeDeactivated(state, currentStateBitMask, isTerminating))
                return false;

            currentStates.Remove(state);
            currentStateBitMask -= state.BitMask;
            //state.Deactivate();
            pendingDeactivatedStates.Add(state);

            for (int i = 0; i < state.TerminateOnDeactivateStates.Count; ++i)
                TryDeactivateState(state.TerminateOnDeactivateStates[i], true);

            return true;
        }

        private void Update()
        {
            for (int i = 0; i < currentStates.Count; ++i)
                TryDeactivateState(currentStates[i]);

            for (int i = 0; i < pendingDeactivatedStates.Count; ++i)
                pendingDeactivatedStates[i].Deactivate();
            if (pendingDeactivatedStates.Count > 0)
                pendingDeactivatedStates.Clear();

            for (int i = 0; i < currentStates.Count; ++i)
                currentStates[i].Update();

            for (int i = 0; i < registeredStates.Length; ++i)
                TryActivateState(registeredStates[i]);

            for (int i = 0; i < pendingActivatedStates.Count; ++i)
                pendingActivatedStates[i].Activate();
            if (pendingActivatedStates.Count > 0)
                pendingActivatedStates.Clear();
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < currentStates.Count; ++i)
                currentStates[i].FixedUpdate();
        }
    }
}
