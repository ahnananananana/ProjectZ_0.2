using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [CreateAssetMenu]
    public class GameData : ScriptableObject
    {
        [SerializeField] private EventReferenceType<PlayerUnitBaseData> playerUnitData = new EventReferenceType<PlayerUnitBaseData>();
        public PlayerUnitBaseData[] PlayerUnitBaseDatas;
        public EnemyUnitBaseData[] EnemyUnitBaseDatas;
        public GunBaseData[] GunBaseDatas;

        public EventReferenceType<PlayerUnitBaseData> PlayerUnitData => playerUnitData;

        public void SetPlayerUnit(PlayerUnitBaseData baseData)
        {
            PlayerUnitData.Value = baseData;
        }
    }
}
