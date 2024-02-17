using System.Collections;
using UnityEngine;

public class UserInterface : MonoBehaviour
{
    [SerializeField] private VariableJoystick _vareiableJoystick;
    [SerializeField] private GameObject _uiContainer;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _timeToFade;

    public bool IsUIHiden { get; private set; }

    private void Start()
    {
        _canvasGroup.gameObject.SetActive(true);
    }

    private void Update()
    {
        UIEvents.JoystickInput = _vareiableJoystick.Direction;
    }

    private IEnumerator StartFadeIn()
    {
        bool finished = false;

        if (_canvasGroup.alpha < 1)
        {
            while (!finished)
            {
                _canvasGroup.alpha += _timeToFade * Time.deltaTime;

                if (_canvasGroup.alpha >= 1)
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

        if (_canvasGroup.alpha >= 0)
        {
            while (!finished)
            {
                _canvasGroup.alpha -= _timeToFade * Time.deltaTime;

                if (_canvasGroup.alpha == 0)
                {
                    finished = true;
                }

                yield return null;
            }
        }
    }

    public void FadeIn()
    {
        StartCoroutine(StartFadeIn());
    }

    public void FadeOut()
    {
        StartCoroutine(StartFadeOut());
    }

    public void HideUI()
    {
        _uiContainer.SetActive(false);

        IsUIHiden = true;
    }

    public void ShowUI()
    {
        _uiContainer.SetActive(true);

        IsUIHiden = false;
    }
}
