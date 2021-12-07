using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


namespace BurgerHeroes.Cameras
{
    public class CameraLift : MonoBehaviour
    {
        [SerializeField, SceneObjectsOnly, Required]
        private Transform _targetTranform;


        private Transform _transform;

        private Vector3 _startOffset;
        private Vector3 _startTargetPosition;

        private Vector3 _targetOffset;

        private void Awake()
        {
            _transform = transform;
            _startTargetPosition = _targetTranform.position;
            
            _startOffset = _transform.position - _startTargetPosition;
        }


        private void Update()
        {
            _targetOffset = _targetTranform.position - _startTargetPosition;

            float _yOffset = _targetOffset.y;
            
            _transform.position = _targetTranform.position + _startOffset + new Vector3(0, _yOffset, 0) / 2;
        }
    }   
}
