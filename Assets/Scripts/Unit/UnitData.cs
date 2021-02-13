using System;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [Serializable]
    public abstract class UnitData : ObjectData
    {
        [SerializeField] protected Dictionary<StatType, Stat> stats = new Dictionary<StatType, Stat>();
        /*[SerializeField] protected StatCurrent healthPoint;
        [SerializeField] protected Stat attackPoint, defensePoint, moveSpeed;

        public StatCurrent HealthPoint => healthPoint;
        public Stat AttackPoint => attackPoint;
        public Stat DefensePoint => defensePoint;
        public Stat MoveSpeed => moveSpeed;*/
        public Dictionary<StatType, Stat> Stats => stats;

        public UnitData() : base() { }
        public UnitData(UnitBaseData baseData) : base(baseData) { }
        public UnitBaseData BaseData => baseData as UnitBaseData;


        protected sealed override void OnInit()
        {
            for (int i = 0; i < (int)StatType.count; ++i)
                stats.Add((StatType)i, new Stat((StatType)i));

            stats[StatType.HealthPoint]= new StatCurrent(StatType.HealthPoint, BaseData.HealthPoint, BaseData.HealthPoint);
            stats[StatType.AttackPoint] = new Stat(StatType.AttackPoint, BaseData.AttackPoint);
            stats[StatType.DefensePoint] = new Stat(StatType.DefensePoint, BaseData.DefensePoint);
            stats[StatType.MoveSpeed] = new Stat(StatType.MoveSpeed, BaseData.MoveSpeed);
            stats[StatType.AttackSpeed] = new Stat(StatType.AttackSpeed, BaseData.AttackSpeed);
            

            OnInitChild();
        }

        protected abstract void OnInitChild();
    }
}