using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

namespace HDV
{
    public class PlayerUnit : Unit<PlayerUnitData, PlayerUnitModel>
    {
        [SerializeField] private Gun currentGun;
        [SerializeField] private FollowingCamera followingCamera;
        [SerializeField] private TargetDetectSystem targetDetectSystem;

        public new PlayerUnitData UnitData => unitData;
        public new PlayerUnitModel UnitModel => unitModel;
        public Gun CurrentGun => currentGun;
        public FollowingCamera FollowingCamera { get => followingCamera; set => followingCamera = value; }
        public override EventObject<ITargetable> Target => targetDetectSystem.Target;
        public override EventFloat TargetDistance => targetDetectSystem.TargetDistance;

        protected override void OnInit()
        {
            followingCamera = FindObjectOfType<FollowingCamera>();

            if (followingCamera != null)
            {

            }
                followingCamera.SetTarget(transform);

            if(unitData.BaseData.EquipedGunData.Prefab)
            {
                var baseGun = Instantiate(unitData.BaseData.EquipedGunData.Prefab);
                baseGun.Init(unitData.GunData);
                Equip(baseGun);
            }
                
        }

       /* public void EquipWeapon(GunData gunData)
        {
            currentGun.Init(gunData);

            //TODO: 제거 Action이 불안정함
            UnitData.Stats[StatType.AttackPoint].Modify(gunData.Stats[StatType.AttackPoint].Value, Operation.SUM, UnequipWeapon);

            if (!UnitData.Stats.TryGetValue(StatType.TimeTillAttack, out Stat stat))
            {
                stat = new Stat(StatType.TimeTillAttack);
                UnitData.Stats[StatType.TimeTillAttack] = stat;
            }
            stat.Modify(gunData.Stats[StatType.TimeTillAttack].Value, Operation.SUM, UnequipWeapon);

            if (!UnitData.Stats.TryGetValue(StatType.RecoilAngle, out stat))
            {
                stat = new Stat(StatType.RecoilAngle);
                UnitData.Stats[StatType.RecoilAngle] = stat;
            }
            stat.Modify(gunData.Stats[StatType.RecoilAngle].Value, Operation.SUM, UnequipWeapon);

            currentGun.DamageFunc += unitData.Stats[StatType.AttackPoint].GetValue;
            currentGun.RecoilAngleFunc += unitData.Stats[StatType.RecoilAngle].GetValue;
            currentGun.TimeTillAttackFunc += unitData.Stats[StatType.TimeTillAttack].GetValue;

            UnitModel.WeaponRigSystem.SetWeaponRigData(gunData.BaseData.WeaponRigData);
            UnitModel.WeaponRigSystem.SetHandRig(1f);
            UnitModel.WeaponRigSystem.SetWeaponPoseRig(1f);

            systemBinder.GetSystem<AttackSystem>().Attackable = currentGun;
        }*/

        public void UnequipWeapon()
        {
            /*currentGun.DamageFunc -= unitData.Stats[StatType.AttackPoint].GetValue;
            currentGun.RecoilAngleFunc -= unitData.Stats[StatType.RecoilAngle].GetValue;
            currentGun.TimeTillAttackFunc -= unitData.Stats[StatType.TimeTillAttack].GetValue;*/

            UnitModel.WeaponRigSystem.SetHandRig(0f);
            UnitModel.WeaponRigSystem.SetWeaponPoseRig(0f);

            systemBinder.GetSystem<AttackSystem>().Attackable = null;
        }

        public void Equip(Gun gun)
        {
            UnitModel.WeaponRigSystem.SetWeaponRigData(gun.GunData.BaseData.WeaponRigData);
            UnitModel.WeaponRigSystem.SetHandRig(1f);
            UnitModel.WeaponRigSystem.SetWeaponPoseRig(1f);

            gun.SetDrop(false);
            gun.transform.SetParent(UnitModel.WeaponRef);
            gun.transform.localPosition = Vector3.zero;
            gun.transform.localRotation = Quaternion.identity;
            gun.transform.localScale = Vector3.one;

            if(currentGun != null)
            {
                currentGun.SetDrop(true);
            }

            UnitData.Stats[StatType.AttackPoint].AddModifier(gun.GunData.Stats[StatType.AttackPoint].Value, Operation.SUM, gun);
            UnitData.Stats[StatType.AttackSpeed].AddModifier(gun.GunData.Stats[StatType.AttackSpeed].Value, Operation.SUM, gun);
            UnitData.Stats[StatType.RecoilAngle].AddModifier(gun.GunData.Stats[StatType.RecoilAngle].Value, Operation.SUM, gun);
            UnitData.Stats[StatType.MagazineSize].AddModifier(gun.GunData.Stats[StatType.MagazineSize].Value, Operation.SUM, gun);

            currentGun = gun;
        }
    }
}
