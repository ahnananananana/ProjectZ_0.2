using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [CreateAssetMenu(menuName = "StateActions/BindVector3ToVector3Action")]
    public class BindVector3ToVector3ActionSO : StateActionSO<BindVector3ToVector3Action>
    {
        [SerializeField] private string sourceComponentName, sourcePropertyName;
        [SerializeField] private string targetComponentName, targetePropertyName;

        public string SourceComponentName => sourceComponentName;
        public string SourcePropertyName => sourcePropertyName;
        public string TargetComponentName => targetComponentName;
        public string TargetePropertyName => targetePropertyName;
    }

    public class BindVector3ToVector3Action : StateAction
    {
        private BindVector3ToVector3ActionSO SO => so as BindVector3ToVector3ActionSO;

        private EventVector3 sourceProperty, targetProperty;

        protected override void OnInit(SystemBinder systemBinder)
        {
            var compType = Type.GetType("HDV." + SO.SourceComponentName);
            var component = systemBinder.GetSystem(compType);
            var propertyInfo = compType.GetProperty(SO.SourcePropertyName, BindingFlags.Public | BindingFlags.Instance);
            Debug.AssertIsNotNull(propertyInfo);
            sourceProperty = propertyInfo.GetValue(component) as EventVector3;
            Debug.AssertIsNotNull(sourceProperty);

            compType = Type.GetType("HDV." + SO.TargetComponentName);
            component = systemBinder.GetSystem(compType);
            propertyInfo = compType.GetProperty(SO.TargetePropertyName, BindingFlags.Public | BindingFlags.Instance);
            Debug.AssertIsNotNull(propertyInfo);
            targetProperty = propertyInfo.GetValue(component) as EventVector3;
            Debug.AssertIsNotNull(targetProperty);

            hasActivate = true;
            hasDeactivate = true;
        }

        public override void OnStateActivate()
        {
            sourceProperty.ChangeEvent += targetProperty.SetValue;
            targetProperty.Value = sourceProperty.Value;
        }

        public override void OnStateDeactivate()
        {
            sourceProperty.ChangeEvent -= targetProperty.SetValue;
        }
    }
}
