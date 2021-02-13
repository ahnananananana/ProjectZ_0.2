using System;
using UnityEngine;

namespace HDV
{
    [Serializable]
    public class StatCurrent : Stat
    {
        [SerializeField] private float currentValue;
        private float minValue = 0;
        public event Action<float> ChangeCurrentValueEvent;

        public float CurrentValue => currentValue;

        public StatCurrent(StatType type, float baseValue, float currentValue) : base(type, baseValue)
        {
            this.currentValue = currentValue;
            ChangeValueEvent += OnChangeMaxValue;
        }

        public void ModifyCurrentValue(float value, Operation operation)
        {
            currentValue = Calculator.Calculate(operation, currentValue, value);

            currentValue = Mathf.Clamp(currentValue, minValue, CalculateFinalValue());

            ChangeCurrentValueEvent?.Invoke(currentValue);
        }

        private void OnChangeMaxValue(float maxValue)
        {
            if(maxValue < currentValue)
            {
                ModifyCurrentValue(currentValue - maxValue, Operation.SUB);
            }
        }

    }
}
