using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace HDV
{
    [CreateAssetMenu(menuName = "Conditions/CollectionChangedCondition")]
    public class CollectionChangedConditionSO : StateDynamicConditionSO<CollectionChangedCondition>
    {
        [SerializeField] private bool triggerOnAdd, triggerOnRemove;

        public bool TriggerOnAdd => triggerOnAdd;
        public bool TriggerOnRemove => triggerOnRemove;
    }

    public class CollectionChangedCondition : StateDynamicCondition
    {
        private CollectionChangedConditionSO SO => so as CollectionChangedConditionSO;

        protected override void OnBindProperty(object propertyObj)
        {
            var collection = (IEventCollection)propertyObj;
            Debug.AssertIsNotNull(collection);

            collection.OnAddEvent += OnAdd;
            collection.OnRemoveEvent += OnRemove;
        }

        private void OnAdd()
        {
            if (SO.TriggerOnAdd)
                IsTrue = true;
        }

        private void OnRemove()
        {
            if (SO.TriggerOnRemove)
                IsTrue = true;
        }
        
    }
}
