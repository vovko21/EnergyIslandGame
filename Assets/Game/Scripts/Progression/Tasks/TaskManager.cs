using System.Collections;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField] private MoveByPoints _target;
    [SerializeField] private Transform _targetTransform;

    private GameTask _currentTask;
    private InGameDateTime _lastTime;

    private void Start()
    {
        _lastTime = TimeManager.Instance.CurrentDateTime;

        _target.OnFinished += OnTargetFinished;
        TimeManager.Instance.OnDateTimeChanged += OnDateTimeChanged;
    }

    private void OnDisable()
    {
        _target.OnFinished -= OnTargetFinished;
        TimeManager.Instance.OnDateTimeChanged -= OnDateTimeChanged;
    }

    private void OnDateTimeChanged(InGameDateTime dateTime)
    {
        if (_lastTime.Hour != dateTime.Hour)
        {
            _currentTask = GenerateTask();

            _target.StartWay();

            CameraController.Instance.FollowEvent();

            _lastTime = dateTime;
        }
    }

    private void OnTargetFinished()
    {
        CameraController.Instance.FollowPlayer();
    }

    private GameTask GenerateTask()
    {
        var targetProgress = Random.Range(1, 2);
        return new GameTask("1", "2", targetProgress);
    }
}
