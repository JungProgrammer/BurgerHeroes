using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ScaleIncreaser : MonoBehaviour
{
    [SerializeField, ChildGameObjectsOnly, Required]
    private Transform[] _transforms;


    [SerializeField] 
    private float _increaseCoefficient = 1.25f;


    [Button]
    private void GetTransforms()
    {
        _transforms = GetComponentsInChildren<Transform>();
    }


    [Button]
    private void IncreaseSize()
    {
        for (int i = 0; i < _transforms.Length; i++)
            if (_transforms[i] != null)
                _transforms[i].localScale *= _increaseCoefficient;
    }
    
    
    [Button]
    private void DecreaseSize()
    {
        for (int i = 0; i < _transforms.Length; i++)
            if (_transforms[i] != null)
                _transforms[i].localScale /= _increaseCoefficient;
    }
}
