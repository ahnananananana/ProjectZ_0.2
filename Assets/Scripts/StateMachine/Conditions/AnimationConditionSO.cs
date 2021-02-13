using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HDV
{
    [CreateAssetMenu(menuName = "Conditions/AnimationCondition")]
    public class AnimationConditionSO : StateConditionSO<AnimationCondition>
    {
        [SerializeField] private string animationName;
        [SerializeField] private bool isIn;

        public string AnimationName => animationName;

        public bool IsIn => isIn;
    }

    public class AnimationCondition : StateCondition
    {
        private AnimationConditionSO SO => so as AnimationConditionSO;

        private AnimationSystem animationSystem;
        private int animationNameHash;

        protected override void OnInit(SystemBinder systemBinder)
        {
            animationSystem = systemBinder.GetSystem<AnimationSystem>();
            animationNameHash = Animator.StringToHash(SO.AnimationName);

            animationSystem.CurrentAnimationHash.ChangeEvent += OnChangeValue;
        }

        private void OnChangeValue(int changedValue)
        {
            if ((changedValue == animationNameHash && SO.IsIn) ||
                (changedValue != animationNameHash && !SO.IsIn))
                IsTrue = true;
            else
                IsTrue = false;
        }
    }
}
