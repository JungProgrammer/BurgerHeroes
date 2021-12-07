using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


namespace BurgerHeroes.Obstacles
{
    public class FlyingObstacle : MonoBehaviour
    {
        [SerializeField, Required]
        private Collider _collider;


        [SerializeField] 
        private int _strength;


        private int _currentStrength;


        private void Awake()
        {
            _currentStrength = _strength;
        }


        public void ReduceStrength()
        {
            _currentStrength--;
            if(_currentStrength <= 0) 
                _collider.enabled = false;
        }
    }   
}
