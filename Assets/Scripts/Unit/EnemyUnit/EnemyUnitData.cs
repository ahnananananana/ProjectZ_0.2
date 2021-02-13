using System;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [Serializable]
    public class EnemyUnitData : UnitData
    {
        /*[SerializeField] private Stat attackRange;
        public Stat AttackRange => attackRange;*/

        public new EnemyUnitBaseData BaseData => baseData as EnemyUnitBaseData;

        public EnemyUnitData() : base() { }
        public EnemyUnitData(EnemyUnitBaseData baseData) : base(baseData) { }

        public void Init(EnemyUnitBaseData baseData = null)
        {
        }

        protected override void OnInitChild()
        {
            stats[StatType.AttackRange] = new Stat(StatType.AttackRange, BaseData.AttackRange);
        }
    }
}
