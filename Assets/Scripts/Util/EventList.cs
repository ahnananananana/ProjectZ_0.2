using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV
{
    [Serializable]
    public class EventList<T> : List<T>, IEventCollection<T>, IEventCollection
    {
        public event Action<T> AddEvent, RemoveEvent;
        public event Action OnAddEvent, OnRemoveEvent;

        public new void Add(T item)
        {
            base.Add(item);
            AddEvent?.Invoke(item);
            OnAddEvent?.Invoke();
        }

        public new bool Remove(T item)
        {
            if (base.Remove(item))
            {
                OnRemoveEvent?.Invoke();
                RemoveEvent?.Invoke(item);
                return true;
            }
            return false;
        }

        public new void RemoveAt(int index)
        {
            var item = this[index];
            OnRemoveEvent?.Invoke();
            base.RemoveAt(index);
            RemoveEvent?.Invoke(item);
        }

    }
}
