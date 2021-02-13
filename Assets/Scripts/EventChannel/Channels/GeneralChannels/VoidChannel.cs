using System;
using UnityEngine;

namespace HDV
{
	[CreateAssetMenu(menuName = "EventChannel/VoidChannel")]
	public class VoidChannel : ScriptableObject
	{
		public Action OnEventRaised;

		public void RaiseEvent()
		{
			if (OnEventRaised != null)
				OnEventRaised.Invoke();
		}
	}
}