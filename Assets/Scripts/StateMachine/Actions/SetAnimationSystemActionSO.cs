using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [CreateAssetMenu(menuName = "StateActions/SetAnimationSystemAction")]
    public class SetAnimationSystemActionSO : StateActionSO<SetAnimationSystemAction>
    {
        [SerializeField] private bool setOnActivate;

        public bool SetOnActivate => setOnActivate;
    }

    public class SetAnimationSystemAction : StateAction
    {
        private AnimationSystem animationSystem;

        private SetAnimationSystemActionSO SO => (SetAnimationSystemActionSO)so;

        protected override void OnInit(SystemBinder systemBinder)
        {
            animationSystem = systemBinder.GetSystem<AnimationSystem>();

            hasActivate = true;
            hasDeactivate = false;
        }

        public override void OnStateActivate()
        {
            animationSystem.IsActive = SO.SetOnActivate;
        }

        public override void OnStateDeactivate()
        {
            animationSystem.IsActive = !SO.SetOnActivate;
        }
    }
}


