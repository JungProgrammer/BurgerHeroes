using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;


namespace BurgerHeroes.Player
{
    public class PlayerTransformController : MonoBehaviour
    {
        [SerializeField, ChildGameObjectsOnly, Required]
        private Transform _playerMeshView;
        
        
        [SerializeField] 
        private float _startXCharacterRotate;
        
        
        [SerializeField, ChildGameObjectsOnly, Required]
        private Transform _idlePlate;


        [SerializeField, ChildGameObjectsOnly, Required]
        private Transform _mainPlate;
        

        private Quaternion _startRotation;

        
        private void Awake()
        {
            SetStartRotate();
        }


        private void SetStartRotate()
        {
            _playerMeshView.Rotate(_startXCharacterRotate, 0, 0);
            _startRotation = _playerMeshView.transform.rotation;
            
            ShowIdlePlate();
        }
        
        
        private void ShowIdlePlate()
        {
            _idlePlate.gameObject.SetActive(true);
            _mainPlate.gameObject.SetActive(false);
        }
        

        private void ShowMainPlate()
        {
            _idlePlate.gameObject.SetActive(false);
            _mainPlate.gameObject.SetActive(true);
        }


        public void SetStartRotateAfterFinish()
        {
            _playerMeshView.transform.rotation = _startRotation;
            ShowIdlePlate();
        }


        public void SetRunRotate()
        {
            _playerMeshView.Rotate(-_startXCharacterRotate, 0, 0);
            ShowMainPlate();
        }
        
        
        public void SetWinRotate()
        {
            _playerMeshView.Rotate(45, 0, 0);
            _playerMeshView.DORotate(new Vector3(0, 180, 0), 1);
        }
    }   
}
