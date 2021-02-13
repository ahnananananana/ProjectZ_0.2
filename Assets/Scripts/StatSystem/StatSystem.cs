using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HDV
{
    public struct StatChangeData
    {
        /*private object sourceObject;
        private StatType statType;
        private float changedValue;
        private float delta;

        public object SourceObject => sourceObject;
        public StatType StatType => statType;
        public float ChangedValue => changedValue;
        public float Delta => delta;

        public StatChangeData(object sourceObject, StatType statType, float changedValue, float delta)
        {
            this.sourceObject = sourceObject;
            this.statType = statType;
            this.changedValue = changedValue;
            this.delta = delta;
        }*/
    }

    public class StatSystem : MonoBehaviour
    {
        /*[SerializeField] private StatTable statTable;
        private Dictionary<Type, Stat> stats = new Dictionary<Type, Stat>();
#if UNITY_EDITOR
        public Dictionary<Type, Stat> EditorStats => stats;
#endif

        [SerializeField] private StatChangeEvent statChangeEvent;

        private void Awake()
        {
            Init(statTable.Stats);
        }

        public void Init(StatData[] statDatas)
        {
            for(int i = 0; i < statDatas.Length; ++i)
                AddStat(new Stat(statDatas[i]), statDatas);
        }

        private void AddStat(Stat newStat, StatData[] statDatas)
        {
            if (stats.ContainsKey(newStat.Type.GetType()))
                return;

            newStat.ChangeValueEvent += statChangeEvent.Invoke;

            stats.Add(newStat.Type.GetType(), newStat);

            if (newStat.Type.DependentStatTypes == null || newStat.Type.DependentStatTypes.Length == 0)
                return;

            Stat[] dependentStats = new Stat[newStat.Type.DependentStatTypes.Length];
            for (int i = 0; i < newStat.Type.DependentStatTypes.Length; ++i)
            {
                var dst = newStat.Type.DependentStatTypes[i];
                var sd = Array.Find(statDatas, s => s.Type == dst.StatType);
                Stat ds;
                if (sd != null)
                    ds = new Stat(sd);
                else
                    ds = new Stat(new StatData(dst.StatType, newStat.Value));
                AddStat(ds, statDatas);
                dependentStats[i] = ds;
            }
            newStat.InitDependency(newStat.Type.DependentStatTypes, dependentStats);
        }

        public float GetStatValue(Type statType)
        {
            Debug.AssertContainKey(stats, statType);
            return stats[statType].Value;
        }

        public void Modify(Type statType, float value, Operation operation, Action removeEvent = null)
        {
            Debug.AssertContainKey(stats, statType);
            stats[statType].Modify(value, operation, removeEvent);
        }*/

    }
}
