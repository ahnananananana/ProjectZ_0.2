using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HDV
{
    public class BoolPropagator : MonoBehaviour
    {
        [SerializeField] private UnityEvent trueEvent, falseEvent;

        public void Raise(bool value)
        {
            if (value)
                trueEvent?.Invoke();
            else
                falseEvent?.Invoke();
        }
    }
}
