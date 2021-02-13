using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public abstract class RigSystem : MonoBehaviour, IStateComponent
    {
        private bool isActive = true;
        public bool IsActive { get => isActive; set => isActive = value; }
    }
}
