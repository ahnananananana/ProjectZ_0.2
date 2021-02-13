using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public class UIUnitSelection : MonoBehaviour
    {
        [SerializeField] private GameData gameData;
        [SerializeField] private PlayerData playerData;
        [SerializeField] private UIUnit uiUnitPrefab;
        [SerializeField] private Transform contextTF;
        private Dictionary<PlayerUnitBaseData, UIUnit> uiUnits = new Dictionary<PlayerUnitBaseData, UIUnit>();

        private void Awake()
        {
            for(int i = 0; i < gameData.PlayerUnitBaseDatas.Length; ++i)
            {
                var uiUnit = Instantiate(uiUnitPrefab, contextTF);
                uiUnit.Init(gameData.PlayerUnitBaseDatas[i]);
                uiUnits.Add(gameData.PlayerUnitBaseDatas[i], uiUnit);
                uiUnit.SetActive(false);
            }
        }

        public void Refresh()
        {
            foreach(var i in uiUnits)
            {
                i.Value.SetActive(false);
            }

        }
    }
}
