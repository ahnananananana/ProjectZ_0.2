using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace HDV
{
    [CustomEditor(typeof(StateMachine))]
    public class StateMachineEditor : Editor
    {
        private StateMachine obj;

        private void OnEnable()
        {
            obj = target as StateMachine;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("Current States");
            for (int i = 0; i < obj.CurrentStates.Count; ++i)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.Space(10f, false);
                EditorGUILayout.LabelField(obj.CurrentStates[i].Name);
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
        }
    }
}
