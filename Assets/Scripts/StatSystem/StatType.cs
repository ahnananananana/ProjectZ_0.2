using System;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{

    public enum StatModifyType
    {
        Modifier,
        Normal,
    }

    public enum StatDependentType
    {
        None,
        LimitMax,
    }

   /* public abstract class StatType
    {
        [SerializeField] protected StatModifyType modifyType;
        [SerializeField] protected bool hasMax;
        [SerializeField] protected bool hasMin;
        [SerializeField] protected float maxValue;
        [SerializeField] protected float minValue;
        [SerializeField] protected DependentStatType[] dependentStatTypes;

        public StatModifyType ModifyType => modifyType;
        public bool HasMax => hasMax;
        public bool HasMin => hasMin;
        public float MaxValue => maxValue;
        public float MinValue => minValue;
        public DependentStatType[] DependentStatTypes { get => dependentStatTypes; set => dependentStatTypes = value; }
    }*/

}
