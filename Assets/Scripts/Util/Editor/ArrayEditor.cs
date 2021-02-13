using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace HDV
{
    public class ArrayEditor
    {
        private SerializedProperty array;

        public ArrayEditor(SerializedProperty array)
        {
            this.array = array;
        }

        public void Paint()
        {
            for (int i = 0; i < array.arraySize; ++i)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(array.GetArrayElementAtIndex(i));
                if (GUILayout.Button("Remove")) 
                    Remove(i);
                EditorGUILayout.EndHorizontal();
            }

            if (GUILayout.Button("Add"))
                Add();
        }

        private void Add()
        {
            array.InsertArrayElementAtIndex(array.arraySize);
        }

        private void Remove(int index)
        {
            for (int j = index + 1; j < array.arraySize - 1; ++j)
                array.MoveArrayElement(j + 1, j);
            array.arraySize--;
        }
    }
}
