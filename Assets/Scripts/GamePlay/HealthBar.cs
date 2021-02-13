using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HDV
{
    public class HealthBar : MonoBehaviour, IStateComponent
    {
        [SerializeField] private bool activeOnAwake;
        [SerializeField] private Unit unit;
        [SerializeField] private Canvas canvas;
        [SerializeField] private Slider outsidebar, insideBar;
        [SerializeField] private float waitingTime, followSpeed;
        private IEnumerator followCoroutine;

        [SerializeField] private FloatEvent healthChangedEvent;

        public bool IsActive {
            get => canvas.enabled;
            set
            {
                canvas.enabled = value;
            }
        }

        public void Start()
        {
            outsidebar.maxValue = unit.UnitData.Stats[StatType.HealthPoint].Value;
            outsidebar.value = ((StatCurrent)unit.UnitData.Stats[StatType.HealthPoint]).CurrentValue;
            insideBar.maxValue = outsidebar.maxValue;
            insideBar.value = outsidebar.value;

            healthChangedEvent?.Invoke(insideBar.value);

            unit.UnitData.Stats[StatType.HealthPoint].ChangeValueEvent += OnChangeMaxHealth;
            ((StatCurrent)unit.UnitData.Stats[StatType.HealthPoint]).ChangeCurrentValueEvent += OnChangeCurrentHealth;

            IsActive = activeOnAwake;
        }

        private void OnChangeMaxHealth(float changedValue)
        {
            outsidebar.maxValue = changedValue;
            insideBar.maxValue = outsidebar.maxValue;
        }

        public void OnChangeCurrentHealth(float changedValue)
        {
            float delta = outsidebar.value - changedValue;

            outsidebar.value -= delta;

            if (followCoroutine == null)
            {
                followCoroutine = FollowOutside();
                StartCoroutine(followCoroutine);
            }

            if (delta > 0 && changedValue > 0)
            {
                if (!IsActive)
                    IsActive = true;
            }

            healthChangedEvent?.Invoke(changedValue);
        }

        private IEnumerator FollowOutside()
        {
            while (!Mathf.Approximately(insideBar.value, outsidebar.value))
            {
                insideBar.value = Mathf.Lerp(insideBar.value, outsidebar.value, followSpeed * Time.deltaTime);
                yield return new WaitForSeconds(waitingTime);
            }
            insideBar.value = outsidebar.value;
            followCoroutine = null;
        }

    }
}
