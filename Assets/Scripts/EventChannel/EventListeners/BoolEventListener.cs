using UnityEngine;
using UnityEngine.Events;

namespace HDV
{
    public class BoolEventListener : EventListener<bool>
    {
        [SerializeField] private UnityEvent trueEvent, falseEvent;

        public override void Invoke(bool param)
        {
            if (param)
                trueEvent?.Invoke();
            else
                falseEvent?.Invoke();
        }
    }
}
