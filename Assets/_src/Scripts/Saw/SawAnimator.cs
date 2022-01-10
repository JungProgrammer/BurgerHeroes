using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawAnimator : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Transform _sawWheelTransform;

    private void Update() {
        _sawWheelTransform.Rotate(_rotationSpeed * Time.deltaTime, 0, 0);
    }
}
