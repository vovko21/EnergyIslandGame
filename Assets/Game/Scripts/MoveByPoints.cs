using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByPoints : MonoBehaviour
{
    [SerializeField] private List<Transform> _transforms = new List<Transform>();
    [SerializeField] private float _speed = 0.01f;
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private Transform _objectToMove;

    private bool _isFinished;

    public event Action OnFinished;

    public void StartWay()
    {
        StartCoroutine(Coroutine());
    }

    private IEnumerator Coroutine()
    {
        if (_transforms.Count == 0) _isFinished = true;

        int currentIndex = 0;
        while (!_isFinished)
        {
            if (IsOnPosition(_transforms[currentIndex]))
            {
                currentIndex++;
            }
            else
            {
                _objectToMove.position = Vector3.MoveTowards(_objectToMove.position, _transforms[currentIndex].position, _speed);

                var lookDirection = _objectToMove.position - _transforms[currentIndex].position;

                if(lookDirection != Vector3.zero)
                {
                    _objectToMove.rotation = Quaternion.Slerp(_objectToMove.rotation, Quaternion.LookRotation(lookDirection), Time.deltaTime * _rotationSpeed);
                }
            }
            if (_transforms.Count == currentIndex)
            {
                _isFinished = true;

                OnFinished?.Invoke();
            }

            yield return new WaitForFixedUpdate();
        }
    }

    private bool IsOnPosition(Transform transform)
    {
        return _objectToMove.transform.position == transform.position;
    }
}
