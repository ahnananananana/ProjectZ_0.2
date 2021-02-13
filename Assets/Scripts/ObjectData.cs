using System;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [Serializable]
    public abstract class ObjectData
    {
        [SerializeField] protected ObjectBaseData baseData;
        protected bool isInit;

        public bool IsInit => isInit;

        public ObjectData()
        {
            if (baseData != null)
                Init();
        }

        public ObjectData(ObjectBaseData baseData)
        {
            Debug.AssertIsNotNull(baseData);
            Init(baseData);
        }

        //[RuntimeInitializeOnLoadMethod]
        public void Init(ObjectBaseData baseData = null)
        {
            Debug.Assert(baseData != null || this.baseData != null);
            if (baseData == null)
                baseData = this.baseData;
            else
                this.baseData = baseData;

            OnInit();
            isInit = true;
        }

        protected abstract void OnInit();
    }
}