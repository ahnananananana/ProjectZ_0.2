using System;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [CreateAssetMenu]
    public class WaveSet : ScriptableObject
    {
        [SerializeField] private Wave[] waves;

        public Wave[] Waves => waves; 
    }

    [Serializable]
    public struct Wave
    {
        [SerializeField] EnemySet[] enemySets;

        public EnemySet[] EnemySets => enemySets;
    }

    [Serializable]
    public struct EnemySet
    {
        [SerializeField] private EnemyUnitBaseData baseData;
        [SerializeField] private int count;

        public EnemyUnitBaseData BaseData => baseData;
        public int Count => count;
    }
}
