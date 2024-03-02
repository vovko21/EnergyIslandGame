using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

public class FollowBySpline : MonoBehaviour
{
    [SerializeField] private SplineContainer _splineContainer;
    [SerializeField] private float _speed;

    private float _spllineLength;

    private void Start()
    {
        _spllineLength = _splineContainer.CalculateLength();

        StartWay();
    }

    public void StartWay()
    {
        StartCoroutine(Coroutine());
    }

    private IEnumerator Coroutine()
    {
        bool isFinished = false;
        float distancePercentage = 0;
        while (!isFinished)
        {
            distancePercentage += _speed * Time.fixedDeltaTime / _spllineLength;

            if(distancePercentage > 1f)
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

        StartCoroutine(Coroutine());
    }

}
