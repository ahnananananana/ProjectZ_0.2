using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public class UnitModelDisplayer : MonoBehaviour
    {
        [SerializeField] private Transform displayTransform;
        private Unit currentUnit;

        public void OnDisplayNewUnit(PlayerUnitData playerUnitData)
        {
            var unitPrefab = playerUnitData.BaseData.UnitPrefab;
            if(currentUnit != null)
            {
                Destroy(currentUnit.gameObject);
            }

            currentUnit = Instantiate(unitPrefab, displayTransform);
            currentUnit.transform.localScale = Vector3.one;
            currentUnit.transform.localPosition = Vector3.zero;
            currentUnit.transform.localRotation = Quaternion.identity;
        }
    }
}
