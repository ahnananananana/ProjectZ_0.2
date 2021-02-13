using System;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [Serializable]
    public class GunData : ObjectData
    {
        [SerializeField] private Dictionary<StatType, Stat> stats = new Dictionary<StatType, Stat>();

        public GunBaseData BaseData => baseData as GunBaseData;
        public Dictionary<StatType, Stat> Stats => stats;

        public GunData() : base() { }
        public GunData(GunBaseData baseData) : base(baseData) { }

        protected override void OnInit()
        {
            stats.Add(StatType.AttackPoint, new Stat(StatType.AttackPoint, BaseData.AttackPoint));
            stats.Add(StatType.AttackSpeed, new Stat(StatType.AttackSpeed, BaseData.AttackSpeed));
            stats.Add(StatType.MagazineSize, new Stat(StatType.MagazineSize, BaseData.MagazineSize));
            stats.Add(StatType.RecoilAngle, new Stat(StatType.RecoilAngle, BaseData.RecoilAngle));
            stats.Add(StatType.BulletSpeed, new Stat(StatType.BulletSpeed, BaseData.BulletSpeed));
        }
    }
}