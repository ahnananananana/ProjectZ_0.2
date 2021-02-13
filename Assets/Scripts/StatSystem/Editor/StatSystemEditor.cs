using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace HDV
{
    [CustomEditor(typeof(StatSystem))]
    public class StatSystemEditor : Editor
    {
       /* private StatSystem statSystem;

        private void OnEnable()
        {
            statSystem = target as StatSystem;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            foreach(var i in statSystem.EditorStats)
            {

                EditorGUILayout.BeginHorizontal();
                string statName = i.Key.ToString();
                statName = statName.Substring(statName.IndexOf('.') + 1);
                EditorGUILayout.LabelField(statName);
                EditorGUILayout.LabelField(i.Value.Value.ToString());
                EditorGUILayout.EndHorizontal();
            }
        }*/

    }
}
