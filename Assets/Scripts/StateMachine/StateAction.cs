using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public abstract class StateAction
    {
        protected StateActionSO so;
        protected bool hasActivate, hasDeactivate, hasUpdate, hasFixedUpdate;

        public bool HasActivate => hasActivate;
        public bool HasDeactivate => hasDeactivate;
        public bool HasUpdate => hasUpdate;
        public bool HasFixedUpdate => hasFixedUpdate;

        public void Init(StateActionSO so, SystemBinder systemBinder)
        {
            this.so = so;
            OnInit(systemBinder);
        }
        protected abstract void OnInit(SystemBinder systemBinder);
        public virtual void OnStateActivate() { }
        public virtual void OnStateDeactivate() { }
        public virtual void OnStateUpdate() { }
        public virtual void OnStateFixedUpdate() { }
    }

    /*public abstract class StateAction<M> : StateAction where M : class
    {
        //TODO: GetComponent 반복 최적화 필요
        public sealed override void Init(StateActionSO so, SystemBinder systemBinder)
        {
            this.so = so;
            OnInit(systemBinder);
        }

        protected abstract void OnInit(SystemBinder systemBinder);
    }*/
}
