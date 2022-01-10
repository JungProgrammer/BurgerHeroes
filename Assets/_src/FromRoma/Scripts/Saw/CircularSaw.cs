using UnityEngine;
using BurgerHeroes.Event;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using DG.Tweening;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(CircularSawAnimationController))]
public class CircularSaw : MonoBehaviour
{
    [SerializeField, Required]
    private GameEvent _defeat;

    private bool _isActive = true;
    private Transform _transform;
    private Collider _sawCollider;
    private CircularSawAnimationController _sawAnimationController;

    [Title("Movement properties")]
    [SerializeField] private float _movespeed;
    [SerializeField] private float _holdTimeOnPoint;
    [SerializeField] private List<Transform> _movementPoints;

    private Sequence _movementSequence;

    private void Awake() {
        PrepareComponents();
        SetMovement();
    }

    private void PrepareComponents() {
        _transform = transform;
        _sawCollider = GetComponent<Collider>();
        _sawAnimationController = GetComponent<CircularSawAnimationController>();
    }

    private void SetMovement() {
        if (_movementPoints.Count <= 1)
            return;

        _movementSequence = DOTween.Sequence();
        _movementSequence.SetLoops(-1, LoopType.Restart);

        Vector3 firstMovementPoint = new Vector3(
            _movementPoints[0].position.x,
            _transform.position.y,
            _movementPoints[0].position.z);
        _movementSequence.Append(_transform.DOMove(firstMovementPoint, 0));

        int movementPointsAmount = _movementPoints.Count;
        for (int i = 0; i < movementPointsAmount; i++) {
            Vector3 currentMovementPoint = new Vector3(
                _movementPoints[i].position.x,
                _transform.position.y,
                _movementPoints[i].position.z);

            int nextMovementPointIndex = Ring(i + 1, movementPointsAmount);
            Vector3 nextMovementPoint = new Vector3(
                _movementPoints[nextMovementPointIndex].position.x,
                _transform.position.y,
                _movementPoints[nextMovementPointIndex].position.z);

            Vector3 distance = nextMovementPoint - currentMovementPoint;

            _movementSequence.Append(
                _transform.DOMove(nextMovementPoint, distance.magnitude / _movespeed));
        }
    }

    private int Ring(int index, int ringSize) {
        while (index < 0)
            index += ringSize;
        return index % ringSize;
    }

    private void OnTriggerEnter(Collider other) {
        if (_isActive) {
            _defeat.Raise();
        }
    }

    public void OnDefeat() {
        _isActive = false;
        _sawCollider.enabled = false;
        _sawAnimationController.enabled = false;
        _movementSequence.Kill();
    }
}
