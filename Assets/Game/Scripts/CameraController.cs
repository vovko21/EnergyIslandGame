using Cinemachine;
using UnityEngine;

public class CameraController : SingletonMonobehaviour<CameraController>
{
    [SerializeField] private CinemachineVirtualCamera _playerCamera;
    [SerializeField] private CinemachineVirtualCamera _eventCamera;
    [SerializeField] private CinemachineVirtualCamera _firstLoadCamera;

    private CinemachineVirtualCamera _currentCamera;

    private int _activePrioraty = 20;
    private int _secondaryPrioraty = 10;

    private void Start()
    {
        _currentCamera = _playerCamera;
        _playerCamera.Priority = _activePrioraty;
        _eventCamera.Priority = _secondaryPrioraty;
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

    private void SwitchCamera(CinemachineVirtualCamera newCamera)
    {
        _currentCamera.Priority = _secondaryPrioraty;
        _currentCamera = newCamera;
        _currentCamera.Priority = _activePrioraty;
    }
}
