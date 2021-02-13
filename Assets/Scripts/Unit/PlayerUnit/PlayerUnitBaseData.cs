using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [CreateAssetMenu]
    public class PlayerUnitBaseData : UnitBaseData<PlayerUnit>
    {
        [SerializeField] private EventBool canPlay;
        public GunBaseData EquipedGunData;

        public EventBool CanPlay => canPlay;
    }
}
