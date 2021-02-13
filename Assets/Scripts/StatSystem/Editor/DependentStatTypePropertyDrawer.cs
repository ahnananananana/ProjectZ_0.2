using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace HDV
{
    [CustomPropertyDrawer(typeof(DependentStatType))]
    public class DependentStatTypePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Rect newPos = position;
            newPos.width /= 2;
            property.FindPropertyRelative("statType").objectReferenceValue =
                EditorGUI.ObjectField(newPos, property.FindPropertyRelative("statType").objectReferenceValue, typeof(StatType), false);
            newPos.x = newPos.width + 25;
            newPos.width -= 10;
            property.FindPropertyRelative("dependentType").enumValueIndex =
                (int)(StatDependentType)EditorGUI.EnumPopup(newPos, (StatDependentType)property.FindPropertyRelative("dependentType").enumValueIndex);
        }

    }
}
