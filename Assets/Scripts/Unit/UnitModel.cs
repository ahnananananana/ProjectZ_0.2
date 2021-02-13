using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public abstract class UnitModel : ObjectModel
    {
        [SerializeField] private AnimationSystem animationSystem;
        [SerializeField] private Transform targetPoint, aimPoint, weaponRef;

        public AnimationSystem AnimationSystem => animationSystem;

        public Transform TargetPoint => targetPoint; 
        public Transform AimPoint => aimPoint;
        public Transform WeaponRef => weaponRef; 
    }
}
