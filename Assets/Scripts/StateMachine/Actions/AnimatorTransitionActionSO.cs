using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public enum AnimatorParameterType
    {
        Bool,
        Int,
        Float,
        Trigger,
    }

    [CreateAssetMenu(menuName = "StateActions/AnimatorAction")]
    public class AnimatorTransitionActionSO : StateActionSO<AnimatorTransitionAction>
    {
        [SerializeField] private AnimatorParameterType parameterType;
        [SerializeField] private string parameterName;
        [SerializeField] private bool boolValue;
        [SerializeField] private int intValue;
        [SerializeField] private float floatValue;

        public AnimatorParameterType ParameterType => parameterType;
        public string ParameterName => parameterName;
        public bool BoolValue => boolValue;
        public int IntValue => intValue;
        public float FloatValue => floatValue;
    }

    public class AnimatorTransitionAction : StateAction
    {
        private AnimatorTransitionActionSO SO => so as AnimatorTransitionActionSO;

        private int parameterNameHash;
        private AnimationSystem animationSystem;

        protected override void OnInit(SystemBinder systemBinder)
        {
            animationSystem = systemBinder.GetSystem<AnimationSystem>();
            Debug.AssertIsNotNull(animationSystem);

            parameterNameHash = Animator.StringToHash(SO.ParameterName);

            hasActivate = true;
            hasDeactivate = true;
        }

        public override void OnStateActivate()
        {
            switch (SO.ParameterType)
            {
                case AnimatorParameterType.Bool:
                    {
                        animationSystem.SetBool(parameterNameHash, SO.BoolValue);
                        break;
                    }
                case AnimatorParameterType.Int:
                    {
                        animationSystem.SetInteger(parameterNameHash, SO.IntValue);
                        break;
                    }
                case AnimatorParameterType.Float:
                    {
                        animationSystem.SetFloat(parameterNameHash, SO.FloatValue);
                        break;
                    }
                case AnimatorParameterType.Trigger:
                    {
                        animationSystem.SetTrigger(parameterNameHash);
                        break;
                    }
            }
        }

        public override void OnStateDeactivate()
        {
            switch (SO.ParameterType)
            {
                case AnimatorParameterType.Bool:
                    {
                        animationSystem.SetBool(parameterNameHash, !SO.BoolValue);
                        break;
                    }
                case AnimatorParameterType.Int:
                    {
                        animationSystem.SetInteger(parameterNameHash, 0);
                        break;
                    }
                case AnimatorParameterType.Float:
                    {
                        animationSystem.SetFloat(parameterNameHash, 0f);
                        break;
                    }
                case AnimatorParameterType.Trigger:
                    {
                        animationSystem.ResetTrigger(parameterNameHash);
                        break;
                    }
            }
        }
    }
}
