using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [CreateAssetMenu(menuName = "StateActions/RootMotionAction")]
    public class RootMotionActionSO : StateActionSO<RootMotionAction>
    {
        [SerializeField] private bool trueOnActivate;

        public bool TrueOnActivate => trueOnActivate;
    }

    public class RootMotionAction : StateAction
    {
        private AnimationSystem animationSystem;

        private RootMotionActionSO SO => so as RootMotionActionSO;

        protected override void OnInit(SystemBinder systemBinder)
        {
            animationSystem = systemBinder.GetSystem<AnimationSystem>();

            hasActivate = true;
            hasDeactivate = true;
        }

        public override void OnStateActivate()
        {
            animationSystem.SetRootMotion(SO.TrueOnActivate);
        }

        public override void OnStateDeactivate()
        {
            animationSystem.SetRootMotion(!SO.TrueOnActivate);
        }
    }
}
