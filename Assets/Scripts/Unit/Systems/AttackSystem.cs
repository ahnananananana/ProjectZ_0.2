using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public class AttackSystem : MonoBehaviour, IStateComponent
    {
        [SerializeField] private Attackable attackable;
        private bool isActive = true;
        private IEnumerator toggleCoroutine;
        [SerializeField] private LayerMask targetLayerMask;

        public bool IsActive { get => isActive; set => isActive = value; }
        public Attackable Attackable { get => attackable; set => attackable = value; }

        public void AttackOnce()
        {
            attackable.Attack(targetLayerMask);
        }

        public void AttackToggle(float interval = -1)
        {
            if (interval < 0)
                interval = 0;

            toggleCoroutine = ToggledAttack(interval);
            StartCoroutine(toggleCoroutine);
        }

        private IEnumerator ToggledAttack(float interval)
        {
            while(true)
            {
                attackable.Attack(targetLayerMask);
                yield return new WaitForSeconds(interval);
            }
        }

        public void StopAttack()
        {
            if(toggleCoroutine != null)
                StopCoroutine(toggleCoroutine);
            attackable.Stop();
        }
    }
}
