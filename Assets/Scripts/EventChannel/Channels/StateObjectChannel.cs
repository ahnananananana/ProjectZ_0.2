﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [CreateAssetMenu(menuName = "EventChannel/StateObjectChannel")]
    public class StateObjectChannel : Channel<IStateObject>
    {
    }
}
