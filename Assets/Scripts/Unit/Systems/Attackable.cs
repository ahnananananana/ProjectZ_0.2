using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public abstract class Attackable : MonoBehaviour
    {
        public abstract void Attack(LayerMask targetLayerMask);
        public abstract void Stop();
    }
}
