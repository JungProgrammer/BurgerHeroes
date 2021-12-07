using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BurgerHeroes.Input
{
    [DefaultExecutionOrder(-1)]
    public class InputManager : Singleton<InputManager>
    {
        public delegate void StartTouchEvent(Vector2 position, float time);
        public event StartTouchEvent OnStartTouch;
        public delegate void EndTouchEvent(Vector2 position, float time);
        public event EndTouchEvent OnEndTouch;


        private TouchControls _touchControls;

        private void Awake()
        {
            _touchControls = new TouchControls();
        }

        private void OnEnable()
        {
            _touchControls.Enable();
        }

        private void OnDisable()
        {
            _touchControls.Disable();
        }

        private void Start()
        {
            _touchControls.Touch.TouchPress.started += ctx => StartTouch(ctx);
            _touchControls.Touch.TouchPress.canceled += ctx => EndTouch(ctx);
        }
        
        private void StartTouch(InputAction.CallbackContext context)
        {
            Debug.Log("Touch started" + _touchControls.Touch.TouchPosition.ReadValue<Vector2>());
            OnStartTouch?.Invoke(_touchControls.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.startTime);
        }
        
        private void EndTouch(InputAction.CallbackContext context)
        {
            Debug.Log("Touch ended");
            OnEndTouch?.Invoke(_touchControls.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.startTime);
        }

        public Vector2 GetTouchPosition()
        {
            return _touchControls.Touch.TouchPosition.ReadValue<Vector2>();
        }

        public Vector2 GetTouchDelta()
        {
            return _touchControls.Touch.TouchDelta.ReadValue<Vector2>();
        }
    }   
}
