using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using Object = System.Object;

namespace BurgerHeroes.Event
{
	[DefaultExecutionOrder(-10)]
    public class GameEventListenerWithParameter : MonoBehaviour
    {
        [SerializeField, AssetsOnly, Required]
        private GameEventWithParameter _event;


        [SerializeField, Required]
        private UnityEvent<Object> _response;


        private void OnEnable() => _event.RegisterListener(this);


        private void OnDisable() => _event.UnregisterListener(this);


        public virtual void OnEventRaised(Object obj) => _response.Invoke(obj);
    }
}
