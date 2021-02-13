using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace HDV
{

    [CustomEditor(typeof(StatTable), true)]
    public class StatTableEditor : Editor
    {
        private StatTable objectBaseData;
        private SerializedProperty stats;

        private void OnEnable()
        {
            objectBaseData = target as StatTable;
            stats = serializedObject.FindProperty("stats");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("Stats");
            for (int i = 0; i < stats.arraySize; ++i)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(stats.GetArrayElementAtIndex(i));
                if (GUILayout.Button("Remove")) RemoveStat(i);
                EditorGUILayout.EndHorizontal();
            }

            if (GUILayout.Button("Add Stat")) AddStat();

            EditorGUILayout.EndVertical();
            GUI.changed = false;
            serializedObject.ApplyModifiedProperties();
        }

        private void AddStat()
        {
            StatData[] newArray = new StatData[objectBaseData.EditorStats.Length + 1];
            Array.Copy(objectBaseData.EditorStats, newArray, objectBaseData.EditorStats.Length);
            newArray[newArray.Length - 1] = new StatData();
            objectBaseData.EditorStats = newArray;
        }

        private void RemoveStat(int index)
        {
            StatData[] newArray = new StatData[objectBaseData.EditorStats.Length - 1];
            for (int i = 0, j = 0; i < newArray.Length; ++i, ++j)
            {
                if (j == index)
                {
                    --i;
                    continue;
                }
                newArray[i] = objectBaseData.EditorStats[j];
            }
            objectBaseData.EditorStats = newArray;
        }
    }
}