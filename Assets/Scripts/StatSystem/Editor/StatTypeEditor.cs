using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace HDV
{
    [CustomEditor(typeof(StatType), true)]
    public class StatTypeEditor : Editor
    {
        private SerializedProperty hasMax;
        private SerializedProperty hasMin;
        private SerializedProperty maxValue;
        private SerializedProperty minValue;
        private SerializedProperty dependentStatTypes;
        private SerializedProperty relationType;

        private void OnEnable()
        {
            hasMax = serializedObject.FindProperty("hasMax");
            hasMin = serializedObject.FindProperty("hasMin");
            maxValue = serializedObject.FindProperty("maxValue");
            minValue = serializedObject.FindProperty("minValue");
            dependentStatTypes = serializedObject.FindProperty("dependentStatTypes");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            hasMax.boolValue = EditorGUILayout.Toggle("HasMax", hasMax.boolValue);

            if(hasMax.boolValue)
                maxValue.floatValue = EditorGUILayout.FloatField("Max Value", maxValue.floatValue);

            hasMin.boolValue = EditorGUILayout.Toggle("HasMin", hasMin.boolValue);

            if (hasMin.boolValue)
                minValue.floatValue = EditorGUILayout.FloatField("Min Value", minValue.floatValue);

            EditorGUILayout.LabelField("Dependent StatTypes");
            for (int i = 0; i < dependentStatTypes.arraySize; ++i)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(dependentStatTypes.GetArrayElementAtIndex(i));
                if(GUILayout.Button("Remove"))
                {
                    for(int j = i + 1; j < dependentStatTypes.arraySize - 1; ++j)
                        dependentStatTypes.MoveArrayElement(j + 1, j);
                    dependentStatTypes.arraySize--;
                }
                EditorGUILayout.EndHorizontal();
            }

            if (GUILayout.Button("Add DependentStatType"))
            {
                dependentStatTypes.InsertArrayElementAtIndex(dependentStatTypes.arraySize);
            }

            serializedObject.ApplyModifiedProperties();
        }

    }
}
