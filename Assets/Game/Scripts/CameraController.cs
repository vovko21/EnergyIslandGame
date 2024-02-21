using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : SingletonMonobehaviour<CameraController>
{
    [SerializeField] private CinemachineVirtualCamera _playerCamera;
    [SerializeField] private CinemachineVirtualCamera _eventCamera;
    [SerializeField] private CinemachineVirtualCamera _firstLoadCamera;
    [SerializeField] private CinemachineVirtualCamera _spotCamera;
    [SerializeField] private Transform _spotCameraAnchor;
    [SerializeField] private float _speed;

    private bool _isFollow;
    private CinemachineVirtualCamera _currentCamera;
    private List<Transform> _followTargets;

    private int _activePrioraty = 20;
    private int _secondaryPrioraty = 10;

    public event Action<bool> OnFollowTargets;
        
    private void Start()
    {
        _currentCamera = _playerCamera;
        _playerCamera.Priority = _activePrioraty;
        _eventCamera.Priority = _secondaryPrioraty;
    }

    private IEnumerator SpotCoroutine()
    {
        int _currentPointIndex = 0;
        //Move camera to follow targets
        while (_isFollow)
        {
            Transform targetPoint = _followTargets[_currentPointIndex];

            _spotCameraAnchor.position = Vector3.MoveTowards(_spotCameraAnchor.position, Vector3.Lerp(_spotCameraAnchor.position, targetPoint.position, 0.1f), _speed * Time.fixedDeltaTime);

            if (Vector3.Distance(_spotCameraAnchor.position, targetPoint.position) < 0.1f)
            {
                yield return new WaitForSeconds(1f);

                _currentPointIndex++;

                if (_currentPointIndex >= _followTargets.Count)
                {
                    _isFollow = false;
                }
            }

            yield return new WaitForFixedUpdate();
        }

        //Move camera to player
        bool isFinished = false;
        while (!isFinished)
        {
            _spotCameraAnchor.position = Vector3.MoveTowards(_spotCameraAnchor.position, Vector3.Lerp(_spotCameraAnchor.position, _playerCamera.Follow.position, 0.1f), _speed * Time.fixedDeltaTime);

            if (Vector3.Distance(_spotCameraAnchor.position, _playerCamera.Follow.position) < 0.1f)
            {
                isFinished = true;
            }

            yield return new WaitForFixedUpdate();
        }

        FollowPlayer();

        OnFollowTargets?.Invoke(false);
    }

    public void FollowPlayer()
    {
        SwitchCamera(_playerCamera);
    }

    public void FollowEvent()
    {
        SwitchCamera(_eventCamera);
    }

    public void FollowFirstLoadScene()
    {
        SwitchCamera(_firstLoadCamera);
    }

    public void FollowTransforms(List<Transform> transforms)
    {
        //Start position
        _spotCameraAnchor.position = _playerCamera.Follow.position;

        _followTargets = transforms;
        SwitchCamera(_spotCamera);
        _isFollow = true;

        OnFollowTargets?.Invoke(true);

        StartCoroutine(SpotCoroutine());
    }

    private void SwitchCamera(CinemachineVirtualCamera newCamera)
    {
        _currentCamera.Priority = _secondaryPrioraty;
        _currentCamera = newCamera;
        _currentCamera.Priority = _activePrioraty;
    }
}
