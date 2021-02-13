using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public class GunCompareDataViewModel : UIViewModel<GunCompareData>
    {
        private EventString gunName = new EventString();
        private EventFloat damage = new EventFloat();
        private EventFloat attckSpeed = new EventFloat();
        private EventFloat dps = new EventFloat();
        private EventFloat recoil = new EventFloat();
        private EventFloat magezineSize = new EventFloat();
        private EventValueType<Comparator> dpsCompare = new EventValueType<Comparator>();
        private EventValueType<Comparator> damageCompare = new EventValueType<Comparator>();
        private EventValueType<Comparator> speedCompare = new EventValueType<Comparator>();
        private EventValueType<Comparator> recoilCompare = new EventValueType<Comparator>();
        private EventValueType<Comparator> magazineCompare = new EventValueType<Comparator>();

        public EventString GunName => gunName;
        public EventFloat Damage => damage;
        public EventFloat AttckSpeed => attckSpeed;
        public EventFloat DPS => dps;
        public EventFloat Recoil => recoil;
        public EventFloat MagezineSize => magezineSize;
        public EventValueType<Comparator> DpsCompare => dpsCompare;
        public EventValueType<Comparator> DamageCompare => damageCompare;
        public EventValueType<Comparator> SpeedCompare => speedCompare;
        public EventValueType<Comparator> RecoilCompare => recoilCompare;
        public EventValueType<Comparator> MagazineCompare => magazineCompare;

        protected override void OnBindModel(GunCompareData model)
        {
            gunName.Value = model.OtherGun.GunData.BaseData.GunName;
            damage.Value = model.OtherGun.GunData.Stats[StatType.AttackPoint].Value;
            attckSpeed.Value = model.OtherGun.GunData.Stats[StatType.AttackSpeed].Value;
            dps.Value = damage.Value * attckSpeed.Value;
            recoil.Value = model.OtherGun.GunData.Stats[StatType.RecoilAngle].Value;
            magezineSize.Value = model.OtherGun.GunData.Stats[StatType.MagazineSize].Value;

            damageCompare.Value = (Comparator)damage.Value.CompareTo(model.PlayerGun.GunData.Stats[StatType.AttackPoint].Value);
            speedCompare.Value = (Comparator)attckSpeed.Value.CompareTo(model.PlayerGun.GunData.Stats[StatType.AttackSpeed].Value);
            dpsCompare.Value = (Comparator)dps.Value.CompareTo(model.PlayerGun.GunData.Stats[StatType.AttackPoint].Value * model.PlayerGun.GunData.Stats[StatType.AttackSpeed].Value);
            recoilCompare.Value = (Comparator)recoil.Value.CompareTo(model.PlayerGun.GunData.Stats[StatType.RecoilAngle].Value);
            magazineCompare.Value = (Comparator)magezineSize.Value.CompareTo(model.PlayerGun.GunData.Stats[StatType.MagazineSize].Value);
        }
    }
}
