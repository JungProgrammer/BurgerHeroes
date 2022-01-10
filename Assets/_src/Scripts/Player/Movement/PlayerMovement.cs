using System;
using System.Collections;
using System.Collections.Generic;
using BurgerHeroes.Input;
using BurgerHeroes.Platforms;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.Touch;


namespace BurgerHeroes.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField, ChildGameObjectsOnly, Required]
        private Transform _viewTransform;


        [SerializeField] 
        private float _moveForwardSpeed;


        [SerializeField]
        private float _rotatingSpeed;

        
        [SerializeField] 
        private float _moveLateralSpeed;


        [SerializeField]
        private LayerMask _platformLayer;
        


        private InputManager _inputManager;


        private Vector2 _worldUnitsInCamera;
        private Vector2 _worldToPixelAmount;

        private Vector3 _leftLimiterPosition;
        private Vector3 _rightLimiterPosition;
        
        
        // Для управления с телефона
        private bool _isHoldTouch;
        private float _sceneWidth;
        private Vector3 _pressedPoint;
        private Vector3 _startPositionBeforeLateralMovement;


        private Transform _transform;

        private float _currentRotation;

        private bool _isMoving;
        private bool _isRotating;



        private void Awake()
        {
            _transform = transform;

            _currentRotation = (_transform.eulerAngles.y > 180) ? _transform.eulerAngles.y - 360 : _transform.eulerAngles.y;

            _worldUnitsInCamera.y = Camera.main.orthographicSize * 2;
            _worldUnitsInCamera.x = _worldUnitsInCamera.y * Screen.width / Screen.height;

            _worldToPixelAmount.x = Screen.width / _worldUnitsInCamera.x;
            _worldToPixelAmount.y = Screen.height / _worldUnitsInCamera.y;
            
            _inputManager = InputManager.Instance;
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


        //private void Move()
        //{
        //    if (!_isMoving)
        //        return;


        //    _rigidbody.velocity = new Vector3(0, _rigidbody.velocity.y, _moveForwardSpeed);

        //    if (_isHoldTouch)
        //    {
        //        Vector2 currentTouchPosition = _inputManager.GetTouchPosition();

        //        if (_pressedPoint == Vector3.zero)
        //            _pressedPoint = new Vector3(currentTouchPosition.x, currentTouchPosition.y, 0);

        //        // float deltaPositionX = currentTouchPosition.x - _pressedPoint.x;
        //        float deltaPositionX = (currentTouchPosition.x - _pressedPoint.x) / _worldToPixelAmount.x *
        //                               _moveLateralSpeed;

        //        // _rigidbody.position = new Vector3(
        //        //     _startPositionBeforeLateralMovement.x + deltaPositionX * _moveLateralSpeed / _sceneWidth,
        //        //     _rigidbody.position.y,
        //        //     _rigidbody.position.z);

        //        _rigidbody.position = new Vector3(
        //            _startPositionBeforeLateralMovement.x + deltaPositionX * _moveLateralSpeed / _sceneWidth,
        //            _rigidbody.position.y,
        //            _rigidbody.position.z);

        //        SetLimitPosition();
        //    }
        //}


        //private void SetLimitPosition()
        //{
        //    if (_rigidbody.position.x <= _leftLimiterPosition.x)
        //        _rigidbody.position = new Vector3(_leftLimiterPosition.x,
        //            _rigidbody.position.y,
        //            _rigidbody.position.z);
        //    if(_rigidbody.position.x >= _rightLimiterPosition.x)
        //        _rigidbody.position = new Vector3(_rightLimiterPosition.x,
        //            _rigidbody.position.y,
        //            _rigidbody.position.z);
        //}


        private void Move()
        {
            if (!_isMoving)
                return;


            //_rigidbody.velocity = new Vector3(0, _rigidbody.velocity.y, _moveForwardSpeed);

            //_rigidbody.velocity = _transform.forward * _moveForwardSpeed;
            //Debug.Log(_rigidbody.velocity);
            //_rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);

            _transform.Translate(Vector3.forward * _moveForwardSpeed * Time.deltaTime);

            CheckCurvePlatform();

            if (_isHoldTouch)
            {
                Vector2 currentTouchPosition = _inputManager.GetTouchPosition();

                if (_pressedPoint == Vector3.zero)
                    _pressedPoint = new Vector3(currentTouchPosition.x, currentTouchPosition.y, 0);

                float deltaPositionX = (currentTouchPosition.x - _pressedPoint.x) / _worldToPixelAmount.x *
                                       _moveLateralSpeed;


                _viewTransform.localPosition = new Vector3(
                    _startPositionBeforeLateralMovement.x + deltaPositionX * _moveLateralSpeed / _sceneWidth,
                    _viewTransform.localPosition.y,
                    _viewTransform.localPosition.z);

                SetLimitPosition();
            }
        }


        private void CheckCurvePlatform()
        {
            if (_isRotating)
                return;

            Ray ray = new Ray(_transform.position + _transform.forward * 1.5f, Vector3.down);
            //Ray ray = new Ray(_transform.position - _transform.forward * 1.5f, Vector3.down);

            if (Physics.Raycast(ray, out RaycastHit hit, 1, _platformLayer))
            {
                if (hit.transform.gameObject != null)
                {
                    if (hit.transform.TryGetComponent(out Curved curved))
                    {
                        if (!curved.Reached)
                        {
                            curved.SetReached();
                            RotateOnCurvedPlatform(curved.RotateDegree);
                        }
                    }
                }
            }
        }


        private void RotateOnCurvedPlatform(float rotateDegree)
        {
            _isRotating = true;

            float rightAngle = (Mathf.Round(transform.eulerAngles.y) > 180) ? Mathf.Round(transform.eulerAngles.y) - 360 : Mathf.Round(transform.eulerAngles.y);
            Debug.Log(rightAngle);
            float delta;
            _currentRotation += rotateDegree;


            StartCoroutine(Rotate());


            IEnumerator Rotate()
            {
                if (rightAngle > _currentRotation)
                {
                    while (rightAngle > _currentRotation + 0.35f)
                    {
                        delta = _rotatingSpeed * 2.17f * Time.deltaTime;
                        if (rightAngle - delta < _currentRotation)
                        {
                            delta = rightAngle - _currentRotation;
                        }
                        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y - delta, 0);
                        rightAngle = (Mathf.Round(transform.eulerAngles.y) > 180) ? Mathf.Round(transform.eulerAngles.y) - 360 : Mathf.Round(transform.eulerAngles.y);
                        //rightAngle = transform.eulerAngles.y - 360;
                        yield return null;
                    }
                }
                else
                {
                    while (rightAngle < _currentRotation - 0.35f)
                    {
                        delta = _rotatingSpeed * 2.045f * Time.deltaTime;
                        if (rightAngle + delta > _currentRotation)
                        {
                            delta = _currentRotation - rightAngle;
                        }
                        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + delta, 0);
                        rightAngle = (Mathf.Round(transform.eulerAngles.y) > 180) ? transform.eulerAngles.y - 360 : transform.eulerAngles.y;
                        yield return null;
                    }
                }

                yield return new WaitForSeconds(0.01f);


                _isRotating = false;
            }
        }


        private void SetLimitPosition()
        {
            if (_viewTransform.localPosition.x <= _leftLimiterPosition.x)
                _viewTransform.localPosition = new Vector3(_leftLimiterPosition.x,
                    _viewTransform.localPosition.y,
                    _viewTransform.localPosition.z);
            if (_viewTransform.localPosition.x >= _rightLimiterPosition.x)
                _viewTransform.localPosition = new Vector3(_rightLimiterPosition.x,
                    _viewTransform.localPosition.y,
                    _viewTransform.localPosition.z);
        }


        private void OnStartTouch(Vector2 screenPosition, float time)
        {
            _pressedPoint = _inputManager.GetTouchPosition();
            _startPositionBeforeLateralMovement = _viewTransform.localPosition;
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
            //_rigidbody.velocity = Vector3.zero;
        }


        public void OnFinished()
        {
            _currentRotation = 0;
        }
    }   
}
