using System;
using UnityEngine;
using UnityEngine.Events;

namespace HDV
{
	public abstract class Channel<T> : ScriptableObject
	{
		public event Action<T> RaiseEvent;

		public virtual void Raise(T param)
		{
			RaiseEvent?.Invoke(param);
		}
	}
}
