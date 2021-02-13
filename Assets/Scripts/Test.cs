using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace HDV
{
    [Serializable]
    public struct tests 
    {
        public int a, b, c;
    }


    public class Test : MonoBehaviour
    {
        public Transform a, b;
        public EventBool testBool;
        public EventValueType<tests> testBools;
        public EventReferenceType<Test> testTest;
        public tests q;

        private void Awake()
        {
        }
    }
}
