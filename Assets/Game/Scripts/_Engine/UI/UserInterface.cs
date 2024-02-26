using System.Collections;
using UnityEngine;

public class UserInterface : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private VariableJoystick _vareiableJoystick;
    [SerializeField] private GameObject _uiContainer;

    [Header("Panels")]
    [SerializeField] private CanvasGroup _loading;
    [SerializeField] private BottomBarUI _bottomBar;

    [Header("Settings")]
    [SerializeField] private float _timeToFade;

    public bool IsUIHiden { get; private set; }
    public bool IsJoystickHidden { get; private set; }
    public BottomBarUI BottomBar => _bottomBar;

    private void Start()
    {
        _loading.gameObject.SetActive(true);

        CameraController.Instance.OnFollowTargets += OnCameraFollowTargets;
    }

    private void Update()
    {
        if (!IsJoystickHidden)
        {
            UIEvents.JoystickInput = _vareiableJoystick.Direction;
        }
    }

    private IEnumerator StartFadeIn()
    {
        bool finished = false;

        if (_loading.alpha < 1)
        {
            while (!finished)
            {
                _loading.alpha += _timeToFade * Time.deltaTime;

                if (_loading.alpha >= 1)
                {
                    finished = true;
                }

                yield return null;
            }
        }
    }

    private IEnumerator StartFadeOut()
    {
        bool finished = false;

        if (_loading.alpha >= 0)
        {
            while (!finished)
            {
                _loading.alpha -= _timeToFade * Time.deltaTime;

                if (_loading.alpha == 0)
                {
                    finished = true;
                }

                yield return null;
            }
        }
    }

    private void OnCameraFollowTargets(bool condition)
    {
        if (condition)
        {
            _vareiableJoystick.DeadZone = 1;
            _vareiableJoystick.gameObject.SetActive(false);
            IsJoystickHidden = true;
        }
        else
        {
            _vareiableJoystick.DeadZone = 0;
            _vareiableJoystick.gameObject.SetActive(true);
            IsJoystickHidden = false;
        }

        _vareiableJoystick.ResetInput();
        UIEvents.JoystickInput = Vector2.zero;
    }

    public void FadeIn()
    {
        StartCoroutine(StartFadeIn());
    }

    public void FadeOut()
    {
        StartCoroutine(StartFadeOut());
    }

    public void Hide()
    {
        _uiContainer.SetActive(false);

        IsUIHiden = true;
    }

    public void Show()
    {
        _uiContainer.SetActive(true);

        IsUIHiden = false;
    }
}
