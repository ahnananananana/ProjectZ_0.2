using System;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [Serializable]
    public class PlayerUnitData : UnitData
    {
        [SerializeField] private GunData gunData;
        //TODO: 장착중인 Perk

        public GunData GunData => gunData;
        public new PlayerUnitBaseData BaseData => baseData as PlayerUnitBaseData;

        public PlayerUnitData() : base() { }
        public PlayerUnitData(PlayerUnitBaseData baseData) : base(baseData) { }

        protected override void OnInitChild()
        {
            gunData = new GunData(BaseData.EquipedGunData);
        }
    }
}
