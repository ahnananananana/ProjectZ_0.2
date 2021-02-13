using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [CreateAssetMenu]
    public class StatTable : ScriptableObject
    {
#if UNITY_EDITOR
        public StatData[] EditorStats { get => stats; set => stats = value; }
#endif
        [HideInInspector]
        [SerializeField]
        private StatData[] stats = new StatData[0];
        public StatData[] Stats => stats;
    }
}
