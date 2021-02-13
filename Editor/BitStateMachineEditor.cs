using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace HDV
{

    [CustomEditor(typeof(BitStateMachine<>), true)]
    public class BitStateMachineEditor<T> : Editor where T : Enum
    {
        private BitStateMachine<T> stateMachine;

        /*private void OnEnable()
        {
            stateMachine = target as BitStateMachine<UnitState>;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (!EditorApplication.isPlaying)
                return;

            foreach (var i in stateMachine.curStates)
            {
                EditorGUILayout.LabelField(i.type.ToString());
            }
        }*/
    }
}

