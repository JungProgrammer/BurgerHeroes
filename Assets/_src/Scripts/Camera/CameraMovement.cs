using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement Instance;


    [SerializeField, SceneObjectsOnly, Required]
    private Transform _positionTarget;


    [SerializeField, SceneObjectsOnly, Required]
    private Transform _lookAtTarget;


    [SerializeField]
    private float _moveSpeed = 15f;


    private Transform _transform;


    private void Awake()
    {
        Instance = this;
        _transform = transform;
    }


    private void LateUpdate()
    {
        if (_positionTarget == null || _lookAtTarget == null)
            return;


        _transform.position = Vector3.Lerp(_transform.position, _positionTarget.position, Time.deltaTime * _moveSpeed);
        _transform.LookAt(_lookAtTarget);
    }
}
