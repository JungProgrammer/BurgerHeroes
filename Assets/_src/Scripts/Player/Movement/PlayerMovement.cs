using System;
using System.Collections;
using System.Collections.Generic;
using BurgerHeroes.Input;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.Touch;


namespace BurgerHeroes.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] 
        private float _moveForwardSpeed;

        
        [SerializeField] 
        private float _moveLateralSpeed;


        private InputManager _inputManager;
        

        private Rigidbody _rigidbody;

        private Vector3 _leftLimiterPosition;
        private Vector3 _rightLimiterPosition;
        
        
        // Для управления с телефона
        private bool _isHoldTouch;
        private float _sceneWidth;
        private Vector3 _pressedPoint;
        private Vector3 _startPositionBeforeLateralMovement;


        private bool _isMoving;
        

        private void Awake()
        {
            _inputManager = InputManager.Instance;
            _rigidbody = GetComponent<Rigidbody>();
            _sceneWidth = Screen.width;

            _isMoving = false;
        }


        private void OnEnable()
        {
            _inputManager.OnStartTouch += OnStartTouch;
            _inputManager.OnEndTouch += OnEndTouch;
        }


        private void OnDisable()
        {
            _inputManager.OnEndTouch -= OnStartTouch;
            _inputManager.OnEndTouch -= OnEndTouch;
        }

        
        private void Update()
        {
            Move();
        }
        
        
        private void Move()
        {
            if (!_isMoving)
                return;


            _rigidbody.velocity = new Vector3(0, _rigidbody.velocity.y, _moveForwardSpeed);
            
            if (_isHoldTouch)
            {
                Vector2 currentTouchPosition = _inputManager.GetTouchPosition();
        
                if (_pressedPoint == Vector3.zero)
                    _pressedPoint = new Vector3(currentTouchPosition.x, currentTouchPosition.y, 0);
                
                float deltaPositionX = currentTouchPosition.x - _pressedPoint.x;

                _rigidbody.position = new Vector3(
                    _startPositionBeforeLateralMovement.x + deltaPositionX * _moveLateralSpeed / _sceneWidth,
                    _rigidbody.position.y,
                    _rigidbody.position.z);

                SetLimitPosition();
            }
        }


        private void SetLimitPosition()
        {
            if (_rigidbody.position.x <= _leftLimiterPosition.x)
                _rigidbody.position = new Vector3(_leftLimiterPosition.x,
                    _rigidbody.position.y,
                    _rigidbody.position.z);
            if(_rigidbody.position.x >= _rightLimiterPosition.x)
                _rigidbody.position = new Vector3(_rightLimiterPosition.x,
                    _rigidbody.position.y,
                    _rigidbody.position.z);
        }
        
        
        private void OnStartTouch(Vector2 screenPosition, float time)
        {
            _pressedPoint = _inputManager.GetTouchPosition();
            _startPositionBeforeLateralMovement = _rigidbody.position;
            _isHoldTouch = true;
        }
        
        
        private void OnEndTouch(Vector2 screenPosition, float time)
        {
            _isHoldTouch = false;
        }


        public void SetLimiters(Vector3 leftLimiterPosition, Vector3 rightLimiterPosition)
        {
            _leftLimiterPosition = leftLimiterPosition;
            _rightLimiterPosition = rightLimiterPosition;
        }


        public void ResumeMovement()
        {
            _isMoving = true;
        }
        

        public void StopMovement()
        {
            _isMoving = false;
            _rigidbody.velocity = Vector3.zero;
        }
    }   
}
