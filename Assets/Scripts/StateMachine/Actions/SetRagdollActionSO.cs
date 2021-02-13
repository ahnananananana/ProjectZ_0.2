using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [CreateAssetMenu(menuName = "StateActions/SetRagdollAction")]
    public class SetRagdollActionSO : StateActionSO<SetHealthBarAction>
    {
        [SerializeField] private bool setOnActivate;

        public bool SetOnActivate => setOnActivate;
    }

    public class SetRagdollAction : StateAction
    {
        private RagdollSystem ragdollSystem;

        private SetRagdollActionSO SO => (SetRagdollActionSO)so;

        protected override void OnInit(SystemBinder systemBinder)
        {
            ragdollSystem = systemBinder.GetSystem<RagdollSystem>();

            hasActivate = true;
            hasDeactivate = false;
        }

        public override void OnStateActivate()
        {
            ragdollSystem.IsActive = SO.SetOnActivate;
        }

        public override void OnStateDeactivate()
        {
            ragdollSystem.IsActive = !SO.SetOnActivate;
        }
    }
}


