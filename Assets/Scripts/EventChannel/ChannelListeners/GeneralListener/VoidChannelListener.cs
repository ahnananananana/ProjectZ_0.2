using UnityEngine;
using UnityEngine.Events;

namespace HDV
{
	public class VoidChannelListener : MonoBehaviour
	{
		[SerializeField] private VoidChannel channel;

		public UnityEvent OnEventRaised;

		private void OnEnable()
		{
			if (channel != null)
				channel.OnEventRaised += Respond;
		}

		private void OnDisable()
		{
			if (channel != null)
				channel.OnEventRaised -= Respond;
		}

		private void Respond() => OnEventRaised?.Invoke();
	}
}
