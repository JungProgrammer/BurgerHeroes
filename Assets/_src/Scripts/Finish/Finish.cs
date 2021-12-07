using System;
using System.Collections;
using System.Collections.Generic;
using BurgerHeroes.Event;
using BurgerHeroes.Player;
using Sirenix.OdinInspector;
using UnityEngine;


namespace BurgerHeroes.Finish
{
    public class Finish : MonoBehaviour
    {
        [SerializeField, AssetsOnly, Required] 
        private GameEvent _playerFinishedEvent;


        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerMovement playerMovement))
            {
                _playerFinishedEvent.Raise();
            }
        }
    }   
}
