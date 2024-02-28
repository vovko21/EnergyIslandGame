using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] public List<Transform> _points;
    [SerializeField] public Transform _objectToMove;
    [SerializeField] public float _speed = 5f;
    [SerializeField] public float _rotationSpeed = 2f;
    [SerializeField] public float _time = 0.008f;

    private int _currentPointIndex = 0;

    public event Action OnFinished;

    private void Start()
    {
        StartWay();
    }

    public void StartWay()
    {
        MoveToNextPoint();
    }

    private void MoveToNextPoint()
    {
        if (_currentPointIndex < _points.Count)
        {
            Vector3 direction = (_points[_currentPointIndex].position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

            _objectToMove.DOMove(_points[_currentPointIndex].position, _speed)
                .SetSpeedBased(true)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    _currentPointIndex++;

                    MoveToNextPoint();
                });

            _objectToMove.DORotateQuaternion(targetRotation, _rotationSpeed)
                .SetSpeedBased(true)
                .SetEase(Ease.Linear);
        }
        else
        {
            _currentPointIndex = 0;

            OnFinished?.Invoke();
        }
    }
}
