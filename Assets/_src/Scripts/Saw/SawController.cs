using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BurgerHeroes.Event;
using DG.Tweening;

public class SawController : MonoBehaviour
{
    [SerializeField] private float _movespeed;
    [SerializeField] private float _distance;
    [SerializeField] private GameEvent _defeatEvent;

    private bool _isActive = true;
    private Vector3 _startPoint;
    private Vector3 _endPoint;
    private Transform _transform;
    private Sequence _moveSequence;

    private bool _movesForward = true;

    private void Awake() {
        _transform = transform;
        _startPoint = _transform.position;
        _endPoint = _transform.rotation * Vector3.forward * _distance + _startPoint;

        SetMovement();
    }

    private void SetMovement() {
        _moveSequence = DOTween.Sequence();
        _moveSequence.SetLoops(-1, LoopType.Restart);

        _moveSequence.Append(_transform.DOMove(_endPoint, _distance / _movespeed));
        _moveSequence.Append(_transform.DOMove(_startPoint, _distance / _movespeed));
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("pososi");
        if (_isActive) {
            Debug.Log("pososiSnova");
            _defeatEvent.Raise();
        }
    }

    public void OnDefeat() {
        _isActive = false;
        _moveSequence.Kill();
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(_startPoint, 0.5f);
        Gizmos.DrawSphere(_endPoint, 0.5f);
    }
}
