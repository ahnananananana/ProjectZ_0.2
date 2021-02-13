using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public abstract class StateActionSO : ScriptableObject
    {
        public abstract StateAction CreateAction(SystemBinder systemBinder);
    }

    public abstract class StateActionSO<T> : StateActionSO where T : StateAction, new()
    {
        public override StateAction CreateAction(SystemBinder systemBinder)
        {
            StateAction action = new T();
            action.Init(this, systemBinder);
            return action;
        }
    }
}
