using System;
using UnityEngine;

namespace HDV
{
    [Serializable]
    public class EventValueType<T> where T : struct
    {
        public event Action<T> ChangeEvent;

        [SerializeField] protected T value;
        public virtual T Value
        {
            get => value;
            set
            {
                if (this.value.Equals(value))
                    return;
                this.value = value;
                ChangeEvent?.Invoke(value);
            }
        }

        public EventValueType() { }
        public EventValueType(T value)
        {
            this.value = value;
        }

        public void SetValue(T newValue) => Value = newValue;
    }

    [Serializable]
    public class EventReferenceType<T> where T : class
    {
        public virtual event Action<T> ChangeEvent;

        [SerializeField] protected T value;
        public virtual T Value
        {
            get => value;
            set
            {
                if (this.value == value)
                    return;
                this.value = value;
                ChangeEvent?.Invoke(value);
            }
        }

        public void SetValue(T newValue) => Value = newValue;
    }

    [Serializable] public class EventInt : EventValueType<int> { }

    [Serializable]
    public class EventFloat : EventValueType<float>
    {
        public EventFloat() : base() { }
        public EventFloat(float value) : base(value) { }
    }
    [Serializable] public class EventBool : EventValueType<bool> { }

    [Serializable] public class EventVector3 : EventValueType<Vector3> { }

    [Serializable] public class EventVector2 : EventValueType<Vector2> { }

    [Serializable] public class EventString : EventReferenceType<string> { public EventString() => value = ""; }

    [Serializable] public class EventSprite : EventReferenceType<Sprite> { }

    [Serializable] public class EventObject : EventReferenceType<object> { }

    [Serializable]
    public class EventObject<T> : EventObject where T : class
    {
        public Action<T> changeEvent;
        public override event Action<object> ChangeEvent { add => changeEvent += value; remove => changeEvent -= value; }
        public new T Value
        {
            get => (T)value;
            set
            {
                if (this.value == value)
                    return;
                this.value = value;
                changeEvent?.Invoke(value);
            }
        }
    }

}
