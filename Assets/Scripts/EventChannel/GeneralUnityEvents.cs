using System;
using UnityEngine;
using UnityEngine.Events;

namespace HDV
{
    [Serializable] public class IntEvent : UnityEvent<int> { }
    [Serializable] public class BoolEvent : UnityEvent<bool> { }
    [Serializable] public class FloatEvent : UnityEvent<float> { }
    [Serializable] public class Vector2Event : UnityEvent<Vector2> { }
    [Serializable] public class DetectEvent : UnityEvent<Collider, Vector3> { }
    [Serializable] public class ColliderEvent : UnityEvent<Collider> { }
    [Serializable] public class CollisionEvent : UnityEvent<Collision> { }
    [Serializable] public class TransformEvent : UnityEvent<Transform> { }
    [Serializable] public class IToggleEvent : UnityEvent<IToggle> { }
}