using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private List<Transform> _points;
    [SerializeField] private Transform _objectToMove;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _rotationDuration = 1f;
    [SerializeField] private Ease _moveMode;
    [SerializeField] private Ease _rotationMode;

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
            _objectToMove.DOMove(_points[_currentPointIndex].position, _speed)
                .SetSpeedBased(true)
                .SetEase(_moveMode)
                .OnComplete(() =>
                {
                    _currentPointIndex++;

                    MoveToNextPoint();
                });

            _objectToMove.DOLookAt(_points[_currentPointIndex].position, _rotationDuration, AxisConstraint.Y)
                .SetEase(_rotationMode);
        }
        else
        {
            _currentPointIndex = 0;
            OnFinished?.Invoke();
        }
    }
}
