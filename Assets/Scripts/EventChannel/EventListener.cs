using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public abstract class EventListener<T> : MonoBehaviour
    {
        public abstract void Invoke(T param);
    }
}
