using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace HDV
{
    [CustomPropertyDrawer(typeof(StatData))]
    public class StatDataPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //EditorGUI.ObjectField(position, property.FindPropertyRelative("statType"));
            Rect newPos = position;
            newPos.width /= 2;
            property.FindPropertyRelative("type").objectReferenceValue = 
                EditorGUI.ObjectField(newPos, property.FindPropertyRelative("type").objectReferenceValue, typeof(StatType), false);
            newPos.x = newPos.width + 25;
            newPos.width -= 10;
            property.FindPropertyRelative("value").floatValue = 
                EditorGUI.FloatField(newPos, property.FindPropertyRelative("value").floatValue);
        }

    }
}
