using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace HDV
{

    /*[CustomEditor(typeof(BitStateMachine<Enum>), true)]
    public class BitStateMachineEditor<T> : Editor where T : Enum
    {
        private BitStateMachine<T> stateMachine;

        private void OnEnable()
        {
            stateMachine = target as BitStateMachine<T>;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            foreach (var i in stateMachine.EditorCurStates)
            {
                EditorGUILayout.LabelField(i.type.ToString());
            }
        }
    }*/
}

