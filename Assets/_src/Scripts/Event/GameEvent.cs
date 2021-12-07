using System;
using System.Collections.Generic;
using UnityEngine;


namespace BurgerHeroes.Event
{
	[CreateAssetMenu(menuName = "Event/GameEvent"), Serializable]
	public class GameEvent : ScriptableObject
	{
		private List<GameEventListener> _listeners = new List<GameEventListener>();


		public virtual void Raise()
		{
			for (var i = _listeners.Count - 1; i >= 0; i--)
				_listeners[i].OnEventRaised();
		}


		public void RegisterListener(GameEventListener listener) => _listeners.Add(listener);


		public void UnregisterListener(GameEventListener listener) => _listeners.Remove(listener);
	}
}
