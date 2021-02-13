using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace HDV
{
    [Serializable] public class ContactEvent : UnityEvent<ContactData> { }
    [Serializable] public class ObjectContactEvent : UnityEvent<object, ContactData> { }
    [Serializable] public class StatChangeEvent : UnityEvent<StatChangeData> { }
    [Serializable] public class UIWindowEvent : UnityEvent<UIWindow> { }
    [Serializable] public class SceneNameEvent : UnityEvent<SceneName> { }
    [Serializable] public class PointerEvent : UnityEvent<PointerEventData> { }
    [Serializable] public class UnitSlotEvent : UnityEvent<UnitSlot> { }
    [Serializable] public class PlayerUnitDataEvent : UnityEvent<PlayerUnitData> { }
    [Serializable] public class StateObjectEvent : UnityEvent<IStateObject> { }
    [Serializable] public class SectorEvent : UnityEvent<Sector> { }
    [Serializable] public class UnitEvent : UnityEvent<Unit> { }
    [Serializable] public class UIUnitEvent : UnityEvent<UIUnit> { }
    [Serializable] public class GunCompareEvent : UnityEvent<GunCompareData> { }
    [Serializable] public class GunEvent : UnityEvent<Gun> { }
    [Serializable] public class StatEvent : UnityEvent<Stat> { }

}
