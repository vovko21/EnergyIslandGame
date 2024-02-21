using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveByPoints : MonoBehaviour
{
    //[SerializeField] private List<Transform> _transforms = new List<Transform>();
    //[SerializeField] private float _time = 0.01f;
    //[SerializeField] private float _speed = 0.01f;
    //[SerializeField] private float _rotationSpeed = 5f;
    //[SerializeField] private Transform _objectToMove;

    //private bool _isFinished;

    //public event Action OnFinished;

    //public void StartWay()
    //{
    //    StartCoroutine(Coroutine());
    //}

    //private IEnumerator Coroutine()
    //{
    //    if (_transforms.Count == 0) _isFinished = true;

    //    int currentIndex = 0;
    //    while (!_isFinished)
    //    {
    //        if (IsOnPosition(_transforms[currentIndex]))
    //        {
    //            currentIndex++;
    //        }
    //        else
    //        {
    //            var direction = (_transforms[currentIndex].position - _objectToMove.position).normalized;

    //            if(direction != Vector3.zero)
    //            {
    //                _objectToMove.rotation = Quaternion.Slerp(_objectToMove.rotation, Quaternion.LookRotation(direction), _rotationSpeed);
    //            }

    //            //_objectToMove.position = Vector3.MoveTowards(_objectToMove.position, Vector3.Lerp(_objectToMove.position, _transforms[currentIndex].position, _time), _speed);
    //            _objectToMove.position = Vector3.MoveTowards(_objectToMove.position, _objectToMove.forward, _speed);
    //        }
    //        if (_transforms.Count == currentIndex)
    //        {
    //            _isFinished = true;

    //            OnFinished?.Invoke();
    //        }

    //        yield return new WaitForSeconds(_time);
    //    }
    //}

    //private bool IsOnPosition(Transform transform)
    //{
    //    return Vector3.Distance(_objectToMove.transform.position, transform.position) < 0.05f;
    //}

    [SerializeField] public List<Transform> _points;
    [SerializeField] public Transform _objectToMove;
    [SerializeField] public float _speed = 5f;
    [SerializeField] public float _rotationSpeed = 2f;
    [SerializeField] public float _time = 0.008f;

    private int _currentPointIndex = 0;
    private bool _isFinished;

    public event Action OnFinished;

    public void StartWay()
    {
        if (_points == null || _points.Count == 0) return;
        StartCoroutine(Coroutine());
    }

    private IEnumerator Coroutine()
    {
        while (!_isFinished)
        {
            Transform targetPoint = _points[_currentPointIndex];
            Vector3 direction = targetPoint.position - _objectToMove.position;
            direction.y = 0f;

            // Повертання в сторону цільової точки
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            _objectToMove.rotation = Quaternion.Slerp(_objectToMove.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime);

            // Рух вперед
            _objectToMove.Translate(Vector3.forward * _speed * Time.fixedDeltaTime);

            // Перевірка, чи об'єкт досягнув цільової точки
            if (Vector3.Distance(_objectToMove.position, targetPoint.position) < 0.1f)
            {
                _currentPointIndex++;

                if (_currentPointIndex >= _points.Count)
                {
                    _isFinished = true;

                    OnFinished?.Invoke();
                }
            }

            yield return new WaitForSeconds(_time);
        }       
    }
}
