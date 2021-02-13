using System;
using System.Reflection;
using UnityEngine;

namespace HDV
{
    [CreateAssetMenu(menuName = "StateActions/BindAnimParamFloatAction")]
    public class BindAnimParamFloatActionSO : StateActionSO<BindAnimParamFloatAction>
    {
        [SerializeField] private string systemName, propertyNam, animParamName;

        public string SystemName => systemName;
        public string PropertyNam => propertyNam;
        public string AnimParamName => animParamName;
    }

    public class BindAnimParamFloatAction : StateAction
    {
        private BindAnimParamFloatActionSO SO => (BindAnimParamFloatActionSO)so;

        private EventFloat bindedProperty;
        private AnimationSystem animationSystem;
        private int animParamHash;

        protected override void OnInit(SystemBinder systemBinder)
        {
            var systemType = Type.GetType("HDV." + SO.SystemName);
            Debug.AssertIsNotNull(systemType);
            var propertyInfo = systemType.GetProperty(SO.PropertyNam, BindingFlags.Public | BindingFlags.Instance);
            Debug.AssertIsNotNull(propertyInfo);
            var system = systemBinder.GetSystem(systemType);
            Debug.AssertIsNotNull(system);
            bindedProperty = propertyInfo.GetValue(system) as EventFloat;
            Debug.AssertIsNotNull(bindedProperty);

            animationSystem = systemBinder.GetSystem<AnimationSystem>();

            animParamHash = Animator.StringToHash(SO.AnimParamName);

            hasActivate = true;
            hasDeactivate = true;
        }

        public override void OnStateActivate()
        {
            bindedProperty.ChangeEvent += OnChangeValue;
        }

        public override void OnStateDeactivate()
        {
            bindedProperty.ChangeEvent -= OnChangeValue;
            animationSystem.SetFloat(animParamHash, 0f);
        }

        private void OnChangeValue(float changedValue)
        {
            animationSystem.SetFloat(animParamHash, changedValue);
        }
    }
}
