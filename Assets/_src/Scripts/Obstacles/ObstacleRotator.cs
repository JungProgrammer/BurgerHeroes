using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BurgerHeroes.Obstacles
{
    public class ObstacleRotator : MonoBehaviour
    {
        [SerializeField] 
        private float _yRotationSpeed;


        private Transform _transform;


        private void Awake()
        {
            _transform = transform;
        }


        private void Update()
        {
            _transform.Rotate(0, _yRotationSpeed * Time.deltaTime, 0);
        }
    }   
}
