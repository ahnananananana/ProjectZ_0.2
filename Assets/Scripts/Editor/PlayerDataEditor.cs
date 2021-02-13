using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace HDV
{
    [CustomEditor(typeof(PlayerData))]
    public class PlayerDataEditor : Editor
    {
        private PlayerData playerData;
        private SerializedProperty currentPlayerUnit, money;
        private ArrayEditor unitArray;

        private void OnEnable()
        {
            playerData = target as PlayerData;
            currentPlayerUnit = serializedObject.FindProperty("currentSelectedUnit");
            money = serializedObject.FindProperty("money");
            unitArray = new ArrayEditor(serializedObject.FindProperty("editorUnitArray"));
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            base.OnInspectorGUI();
            /*EditorGUILayout.LabelField("Money");
            playerData.Money.Value = EditorGUILayout.IntField(playerData.Money.Value);
            if(GUI.changed)
            {
                EditorUtility.SetDirty(playerData);
            }*/
            EditorGUILayout.LabelField("Units");
            unitArray.Paint();
            serializedObject.ApplyModifiedProperties();
        }
    }
}
