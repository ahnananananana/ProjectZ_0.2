using UnityEngine;
using UnityEngine.Events;

namespace HDV
{
	public abstract class ChannelListener<C, E, P> : MonoBehaviour where C : Channel<P> where E : UnityEvent<P>
	{
		[SerializeField] private C channel;

		public E RaiseEvent;

		private void OnEnable()
		{
			if (channel != null)
				channel.RaiseEvent += OnRaised;
		}

		private void OnDisable()
		{
			if (channel != null)
				channel.RaiseEvent -= OnRaised;
		}

		private void OnRaised(P value) => RaiseEvent?.Invoke(value);
	}
}
