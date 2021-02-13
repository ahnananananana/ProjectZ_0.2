using System;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [Serializable]
    public enum StatType
    {
        HealthPoint,
        DefensePoint,
        MoveSpeed,
        AttackPoint,
        AttackSpeed,
        MagazineSize,
        RecoilAngle,
        BulletSpeed,
        AttackRange,
        count,
    }

    [Serializable]
    public class Stat
    {
        [SerializeField] private StatType type;
        [SerializeField] private float baseValue;
        protected float finalValue;
        private bool isDirty;

        private List<StatModifier> modifiers = new List<StatModifier>();
        public event Action<float> ChangeValueEvent;
        public float Value => CalculateFinalValue();

        public StatType Type => type;

        public float GetValue() => CalculateFinalValue();

        public Stat(StatType type, float baseValue = 0)
        {
            this.type = type;
            this.baseValue = baseValue;
            isDirty = true;
        }

        public void Modify(float value, Operation operation, Action removeEvent = null)
        {
            isDirty = true;
            var modifier = new StatModifier(operation, value);
            removeEvent += () => modifiers.Remove(modifier);
            modifiers.Add(modifier);

            float newValue = CalculateFinalValue();

            ChangeValueEvent?.Invoke(newValue);
        }

        public void AddModifier(float value, Operation operation, object source)
        {
            isDirty = true;
            var modifier = new StatModifier(operation, value, source);
            modifiers.Add(modifier);

            ChangeValueEvent?.Invoke(CalculateFinalValue());
        }

        public void RemoveModifier(object source)
        {
            for(int i = 0; i < modifiers.Count; ++i)
            {
                if(modifiers[i].Source == source)
                {
                    isDirty = true;
                    modifiers.RemoveAt(i--);
                }
            }

            if(isDirty)
                ChangeValueEvent?.Invoke(CalculateFinalValue());
        }

        protected float CalculateFinalValue()
        {
            if (!isDirty)
                return finalValue;

            finalValue = baseValue;

            for (int i = 0; i < modifiers.Count; ++i)
            {
                var modifier = modifiers[i];
                finalValue = Calculator.Calculate(modifier.Operation, finalValue, modifier.Value);
            }

            isDirty = false;
            return finalValue;
        }

    }
}
