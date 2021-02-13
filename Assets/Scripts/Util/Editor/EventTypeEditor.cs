using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

namespace HDV
{
    [CustomPropertyDrawer(typeof(EventBool))]
    public class EventBoolPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var target = fieldInfo.GetValue(property.serializedObject.targetObject) as EventBool;
            target.Value = EditorGUI.Toggle(position, label, target.Value);
        }
    }

    [CustomPropertyDrawer(typeof(EventInt))]
    public class EventIntPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var target = fieldInfo.GetValue(property.serializedObject.targetObject) as EventInt;
            target.Value = EditorGUI.IntField(position, label, target.Value);
        }
    }

    [CustomPropertyDrawer(typeof(EventFloat))]
    public class EventFloatPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var target = fieldInfo.GetValue(property.serializedObject.targetObject) as EventFloat;
            target.Value = EditorGUI.FloatField(position, label, target.Value);
        }
    }

    [CustomPropertyDrawer(typeof(EventString))]
    public class EventStringPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var target = fieldInfo.GetValue(property.serializedObject.targetObject) as EventString;
            target.Value = EditorGUI.TextField(position, label, target.Value);
        }
    }

    [CustomPropertyDrawer(typeof(EventReferenceType<>))]
    public class EventReferencePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var target = fieldInfo.GetValue(property.serializedObject.targetObject);
            var pi = fieldInfo.FieldType.GetProperty("Value");
            var p = pi.GetValue(target);
            if(p is UnityEngine.Object)
            {
                var value = EditorGUI.ObjectField(position, label, (UnityEngine.Object)p, pi.PropertyType, true);
                pi.SetValue(target, value);
            }
            else
            {
                var s = property.FindPropertyRelative("value");
                EditorGUI.BeginProperty(position, label, s);
                EditorGUI.PropertyField(position, s, label);
                EditorGUI.EndProperty();
            }
        }
    }
}
