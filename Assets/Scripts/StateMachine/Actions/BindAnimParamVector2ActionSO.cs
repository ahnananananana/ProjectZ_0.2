using System;
using System.Reflection;
using UnityEngine;

namespace HDV
{
    [CreateAssetMenu(menuName = "StateActions/BindAnimatorParamVector2Action")]
    public class BindAnimParamVector2ActionSO : StateActionSO<BindAnimParamVector2Action>
    {
        [SerializeField] private string systemName, propertyName, animParamXName, animParamYName;

        public string SystemName => systemName;
        public string PropertyName => propertyName; 
        public string AnimParamXName => animParamXName;
        public string AnimParamYName => animParamYName;
    }

    public class BindAnimParamVector2Action : StateAction
    {
        private EventVector2 bindedProperty;
        private AnimationSystem animationSystem;
        private int animParamXHash, animParamYHash;

        private BindAnimParamVector2ActionSO SO => (BindAnimParamVector2ActionSO)so;

        protected override void OnInit(SystemBinder systemBinder)
        {
            var systemType = Type.GetType("HDV." + SO.SystemName);
            Debug.AssertIsNotNull(systemType);
            var propertyInfo = systemType.GetProperty(SO.PropertyName, BindingFlags.Public | BindingFlags.Instance);
            Debug.AssertIsNotNull(propertyInfo);
            var system = systemBinder.GetSystem(systemType);
            Debug.AssertIsNotNull(system);
            bindedProperty = propertyInfo.GetValue(system) as EventVector2;
            Debug.AssertIsNotNull(bindedProperty);

            animationSystem = systemBinder.GetSystem<AnimationSystem>();

            animParamXHash = Animator.StringToHash(SO.AnimParamXName);
            animParamYHash = Animator.StringToHash(SO.AnimParamYName);

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
            animationSystem.SetFloat(animParamXHash, 0f);
            animationSystem.SetFloat(animParamYHash, 0f);
        }

        private void OnChangeValue(Vector2 changedValue)
        {
            animationSystem.SetFloat(animParamXHash, changedValue.x);
            animationSystem.SetFloat(animParamYHash, changedValue.y);
        }
    }
}
