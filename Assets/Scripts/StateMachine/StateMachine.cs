using System;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    /****************************
     * 컴포넌트 형식의 상태 기계
     ****************************/

    public class StateMachine : MonoBehaviour
    {
        //등록된 상태 목록
        [SerializeField] private StateSO[] registeredStateSOs;

        //등록된 상태 목록
        private State[] registeredStates;

        //현재 상태 비트
        private int currentStateBitMask;

        //현재 활성화된 상태 목록
        private List<State> currentStates = new List<State>();

        //활성화 될 상태 목록
        private List<State> pendingActivatedStates = new List<State>();

        //비활성화 될 상태 목록
        private List<State> pendingDeactivatedStates = new List<State>();

        public List<State> CurrentStates => currentStates;

        /****************************
         * 상태 기계를 초기화시키는 함수
         * @param IStateObject stateObject : StateMachine을 사용할 오브젝트
         * @param SystemBinder systemBinder : 
         *          StateMachine을 구축에 필요한 컴포넌트에 대한 정보를 가진 컴포넌트
         ****************************/
        public void Init(IStateObject stateObject, SystemBinder systemBinder)
        {
            Debug.AssertIsNotNull(registeredStateSOs);

            int bitMask = 1;
            Dictionary<StateSO, State> createdStates = new Dictionary<StateSO, State>();
            for (int i = 0; i < registeredStateSOs.Length; ++i)
            {
                var so = registeredStateSOs[i];
                so.CreateState(stateObject, ref bitMask, systemBinder, createdStates);
            }

            registeredStates = new State[createdStates.Count];
            createdStates.Values.CopyTo(registeredStates, 0);

            for (int i = 0; i < registeredStates.Length; ++i)
                TryActivateState(registeredStates[i], false);
        }

        /****************************
         * 상태가 활성화 가능한 지 검사하는 함수
         * @param State state : 활성화하고 싶은 상태
         * @param int currentStateBitMask : 현재 상태의 bit 상태
         * @param bool allowRetrigger : 재활성화가 가능한지
         * @return bool : 가능하면 true, 아니면 false          
         ****************************/
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

        /****************************
         * 상태가 비활성화 가능한 지 검사하는 함수
         * @param State state : 비활성화하고 싶은 상태
         * @param int currentStateBitMask : 현재 상태의 bit 상태
         * @param bool isTerminating : 비활성화를 강제할 것인지
         * @return bool : 가능하면 true, 아니면 false          
         ****************************/
        private bool CanbeDeactivated(State state, int currentStateBitMask, bool isTerminating = false)
        {
            if ((currentStateBitMask & state.BitMask) == 0 ||
                (!isTerminating && !state.CanbeDeactivated()))
                return false;
            else
                return true;
        }

        /****************************
         * 상태를 활성화하는 함수
         * @param State state : 활성화하고 싶은 상태
         * @param bool allowRetrigger : 재활성화가 가능한지
         * @return bool : 활성화했다면 true, 아니면 false          
         ****************************/
        private bool TryActivateState(State state, bool allowRetrigger = true)
        {
            if (!CanbeActivated(state, currentStateBitMask, allowRetrigger))
                return false;

            if ((currentStateBitMask & state.BitMask) == 0)
            {
                currentStates.Add(state);
            }

            currentStateBitMask |= state.BitMask;
            pendingActivatedStates.Add(state);

            for (int i = 0; i < state.TerminateOnActivateStates.Count; ++i)
                TryDeactivateState(state.TerminateOnActivateStates[i], true);

            return true;
        }

        /****************************
         * 상태를 비활성화하는 함수
         * @param State state : 비활성화하고 싶은 상태
         * @param bool isTerminating : 비활성화를 강제할 것인지
         * @return bool : 비활성화했다면 true, 아니면 false          
         ****************************/
        private bool TryDeactivateState(State state, bool isTerminating = false)
        {
            if (!CanbeDeactivated(state, currentStateBitMask, isTerminating))
                return false;

            currentStates.Remove(state);
            currentStateBitMask -= state.BitMask;
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
