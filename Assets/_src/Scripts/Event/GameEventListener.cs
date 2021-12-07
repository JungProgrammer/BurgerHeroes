using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;


namespace BurgerHeroes.Event
{
	public class GameEventListener : MonoBehaviour
	{
		[SerializeField, AssetsOnly, Required]
		private GameEvent _event;


		[SerializeField, Required]
		private UnityEvent _response;


		private void OnEnable() => _event.RegisterListener(this);


		private void OnDisable() => _event.UnregisterListener(this);


		public virtual void OnEventRaised() => _response.Invoke();
	}
}
