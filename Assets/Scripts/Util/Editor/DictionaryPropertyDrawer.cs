using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Util
{
    class DictionaryPropertyDrawer
    {
    }
}
/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace HDV
{

    [CustomEditor(typeof(ObjectBaseData), true)]
    public class ObjectBaseDataEditor : Editor
    {
        private ObjectBaseData objectBaseData;

        private void OnEnable()
        {
            objectBaseData = target as ObjectBaseData;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("Stats");
            for(int i = 0; i < objectBaseData.editorStatKeys.Count; ++i)
            {
                EditorGUILayout.BeginHorizontal();
                StatType statType = EditorGUILayout.ObjectField(objectBaseData.editorStatKeys[i], typeof(StatType), false) as StatType;
                if (GUI.changed)
                {
                    ChangeStatType(statType, i);
                    GUI.changed = false;
                }

                float statValue = EditorGUILayout.FloatField(objectBaseData.editorStatValues[i]);
                if (GUI.changed)
                {
                    ChangeStatValue(statType, statValue, i);
                    GUI.changed = false;
                }

                if (GUILayout.Button("Remove")) RemoveStat(statType, i);
                EditorGUILayout.EndHorizontal();
            }

            if (GUILayout.Button("Add Stat")) AddStat();

            EditorGUILayout.EndVertical();
            GUI.changed = false;
        }

        private void AddStat()
        {
            objectBaseData.editorStatKeys.Add(null);
            objectBaseData.editorStatValues.Add(0);
        }

        private void RemoveStat(StatType statType, int index)
        {
            objectBaseData.editorStatKeys.RemoveAt(index);
            objectBaseData.editorStatValues.RemoveAt(index);
            if (statType != null)
                objectBaseData.Stats.Remove(statType);
        }

        private void ChangeStatType(StatType statType, int index)
        {
            objectBaseData.editorStatKeys[index] = statType;
            if (statType == null)
                objectBaseData.Stats.Remove(statType);
        }

        private void ChangeStatValue(StatType statType, float newValue, int index)
        {
            objectBaseData.editorStatKeys[index] = statType;
            objectBaseData.editorStatValues[index] = newValue;
            objectBaseData.Stats[statType] = newValue;
        }
    }
}
*/