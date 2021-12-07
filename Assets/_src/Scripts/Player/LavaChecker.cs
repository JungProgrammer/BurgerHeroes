using System;
using System.Collections;
using System.Collections.Generic;
using BurgerHeroes.Event;
using BurgerHeroes.Obstacles;
using Sirenix.OdinInspector;
using UnityEngine;


namespace BurgerHeroes.Player
{
    public class LavaChecker : MonoBehaviour
    {
        [SerializeField, AssetsOnly, Required] 
        private GameEvent _takeLavaDamageEvent;


        [SerializeField] 
        private float _takeLavaDamageDelay;


        private float _currentDelay;


        private void Awake()
        {
            _currentDelay = 0;
        }


        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out Lava lava))
            {
                _currentDelay += Time.deltaTime;

                if (_currentDelay >= _takeLavaDamageDelay)
                {
                    _currentDelay = 0;
                    _takeLavaDamageEvent.Raise();
                }
            }
        }
    }   
}
