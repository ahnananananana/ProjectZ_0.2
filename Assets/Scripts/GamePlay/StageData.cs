using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [CreateAssetMenu]
    public class StageData : ScriptableObject
    {
        [SerializeField] private int mapCount;
        [SerializeField] private Map[] maps;
        [SerializeField] private WaveSet[] waveSets;

        public int MapCount => mapCount;
        public Map[] Maps => maps;
        public WaveSet[] WaveSets => waveSets;
    }
}
