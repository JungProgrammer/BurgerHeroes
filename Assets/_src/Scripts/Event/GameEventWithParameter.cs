using System;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

namespace BurgerHeroes.Event
{
    [CreateAssetMenu(menuName = "Event/GameEventWithParameter"), Serializable]
    public class GameEventWithParameter : ScriptableObject
    {
        private List<GameEventListenerWithParameter> _listeners = new List<GameEventListenerWithParameter>();


        public virtual void Raise(Object obj) {
            for (var i = _listeners.Count - 1; i >= 0; i--)
                _listeners[i].OnEventRaised(obj);
        }


        public void RegisterListener(GameEventListenerWithParameter listener) => _listeners.Add(listener);


        public void UnregisterListener(GameEventListenerWithParameter listener) => _listeners.Remove(listener);
    }
}
