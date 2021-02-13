using System;
using System.Collections.Generic;

namespace HDV
{
    public class EventQueue<T> : Queue<T>, IEventCollection<T>, IEventCollection
    {
        public event Action<T> AddEvent, RemoveEvent;
        public event Action OnAddEvent, OnRemoveEvent;

        public bool TryDequeue(out T item)
        {
            if (Count > 0)
            {
                item = Dequeue();
                OnRemoveEvent?.Invoke();
                RemoveEvent?.Invoke(item);
                return true;
            }
            item = default;
            return false;
        }

        public new void Enqueue(T item)
        {
            base.Enqueue(item);
            AddEvent?.Invoke(item);
            OnAddEvent?.Invoke();
        }
    }
}
