using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [CreateAssetMenu]
    public class EnemyUnitBaseData : UnitBaseData<EnemyUnit>
    {
        [SerializeField] private float attackRange;

        public float AttackRange => attackRange;
    }
}

