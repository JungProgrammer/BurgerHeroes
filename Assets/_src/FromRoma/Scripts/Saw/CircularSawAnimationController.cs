using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

public class CircularSawAnimationController : MonoBehaviour
{
    [SerializeField, ChildGameObjectsOnly, Required]
    private Transform _sawModel;

    [SerializeField] private float _rotationSpeed;

    private void Update() {
        _sawModel.Rotate(0, -_rotationSpeed * Time.deltaTime, 0);
    }
}
