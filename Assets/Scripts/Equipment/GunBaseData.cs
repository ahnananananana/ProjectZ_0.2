using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [CreateAssetMenu]
    public class GunBaseData : ObjectBaseData
    {
        [SerializeField] private Gun prefab;
        [SerializeField] private string gunName;
        public Sprite Image;
        public Rank Rank;
        [SerializeField] private Bullet bullet;
        [SerializeField] private float attackPoint, attackSpeed, magazineSize, recoilAngle, bulletSpeed;
        [SerializeField] private WeaponRigData weaponRigData;
        [SerializeField] private AudioClip fireAudioClip;

        public string GunName => gunName;
        public Gun Prefab => prefab;
        public WeaponRigData WeaponRigData => weaponRigData;
        public float AttackPoint => attackPoint;
        public float AttackSpeed => attackSpeed;
        public float MagazineSize => magazineSize;
        public float RecoilAngle => recoilAngle;
        public float BulletSpeed => bulletSpeed;
        public Bullet Bullet => bullet;
        public AudioClip FireAudioClip => fireAudioClip;
    }
}
