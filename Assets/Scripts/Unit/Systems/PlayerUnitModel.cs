using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public class PlayerUnitModel : UnitModel
    {
        [SerializeField] private WeaponRigSystem weaponRigSystem;

        public WeaponRigSystem WeaponRigSystem => weaponRigSystem;
    }
}
