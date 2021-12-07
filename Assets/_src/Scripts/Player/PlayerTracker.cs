using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BurgerHeroes.Player
{
    public class PlayerTracker : MonoBehaviour
    {
        [SerializeField] 
        private PlayerMovement _playerMovement;
        

        [SerializeField] 
        private float _lateralSpeed;

        
        private Transform _transform;
        private Vector3 _offset;


        private void Awake()
        {
            _transform = transform;
            _offset = _transform.position - _playerMovement.transform.position;
        }


        private void FixedUpdate()
        {
            LateralTrack();
        }


        private void Update()
        {
            ForwardTrack();
        }


        private void ForwardTrack()
        {
            _transform.position = new Vector3(
                _transform.position.x,
                _transform.position.y,
                (_playerMovement.transform.position + _offset).z);
        }


        private void LateralTrack()
        {
            Vector3 lateralPosition = new Vector3(_playerMovement.transform.position.x,
                _transform.position.y,
                _transform.position.z);
            
            _transform.position = Vector3.Slerp(_transform.position, lateralPosition, _lateralSpeed);
        }
    }   
}
