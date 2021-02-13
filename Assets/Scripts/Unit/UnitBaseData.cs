using System;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public abstract class UnitBaseData : ObjectBaseData
    {
        [SerializeField] private string unitName;
        [SerializeField] private Sprite image;
        [SerializeField] private float healthPoint, attackPoint, attackSpeed, defensePoint, moveSpeed;
        [SerializeField] protected Unit unitPrefab;

        public Unit UnitPrefab => unitPrefab;
        public string UnitName => unitName;
        public Sprite Image => image;
        public float HealthPoint => healthPoint;
        public float AttackPoint => attackPoint;
        public float AttackSpeed => attackSpeed;
        public float DefensePoint => defensePoint;
        public float MoveSpeed => moveSpeed;
    }

    public abstract class UnitBaseData<T> : UnitBaseData 
        where T : Unit
    {
        public new T UnitPrefab => (T)unitPrefab;
    }
}