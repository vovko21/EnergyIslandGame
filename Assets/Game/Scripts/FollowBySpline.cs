using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

public class FollowBySpline : MonoBehaviour
{
    [SerializeField] private SplineContainer _splineContainer;
    [SerializeField] private float _speed;
    [SerializeField] private int _stopPointIndex;

    private float _spllineLength;
    private float _distancePercentage;

    private void Start()
    {
        _spllineLength = _splineContainer.CalculateLength();
    }

    public void GoToShoer(Action onFinish = null)
    {
        _distancePercentage = 0;
        StartCoroutine(Coroutine(0, 0.5f, onFinish));
    }

    public void GoFromShoer(Action onFinish = null)
    {
        _distancePercentage = 0.5f;
        StartCoroutine(Coroutine(0.5f, 1f, onFinish));
    }

    [ContextMenu("StartWay")]
    public void StartWay(Action onFinish = null)
    {
        if(_distancePercentage == 0)
        {
            StartCoroutine(Coroutine(0, 0.5f, onFinish));
        }
        if(_distancePercentage >= 0.5f)
        {
            StartCoroutine(Coroutine(0.5f, 1f, onFinish));
        }
    }

    private IEnumerator Coroutine(float startNormalized, float endNormalized, Action onFinish)
    {
        bool isFinished = false;
        float distancePercentage = startNormalized;
        while (!isFinished)
        {
            distancePercentage += _speed * Time.fixedDeltaTime / _spllineLength;

            if(distancePercentage >= endNormalized)
            {
                isFinished = true;
            }

            Vector3 position = _splineContainer.EvaluatePosition(distancePercentage);
            transform.position = position;

            Vector3 nextPosition = _splineContainer.EvaluatePosition(distancePercentage + 0.05f);
            Vector3 direction = nextPosition - position;
            transform.rotation = Quaternion.LookRotation(direction);

            yield return new WaitForFixedUpdate();
        }

        onFinish?.Invoke();
        _distancePercentage = distancePercentage;

        if(_distancePercentage >= 1)
        {
            _distancePercentage = 0;
        }
    } 
}
