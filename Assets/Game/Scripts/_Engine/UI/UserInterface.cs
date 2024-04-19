using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class UserInterface : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private VariableJoystick _vareiableJoystick;
    [SerializeField] private GameObject _uiContainer;
    [SerializeField] private GameObject _midleSectionContainer;

    [Header("Panels")]
    [SerializeField] private CanvasGroup _loading;
    [SerializeField] private BottomBarUI _bottomUI;
    [SerializeField] private TasksUI _tasksUI;
    [SerializeField] private DailyRewardsUI _dailyRewardsUI;
    [SerializeField] private BoosterUI _boosterUI;
    [SerializeField] private SettingsUI _settingsUI;
    [SerializeField] private CanvasGroup _hintBar;
    [SerializeField] private AdTimerUI _adTimerUI;
    [SerializeField] private NoAdsUI _noAdsUI;

    [Header("Settings")]
    [SerializeField] private float _timeToFade;
    [SerializeField] private TextMeshProUGUI _hintText;

    public bool IsUIHiden { get; private set; }
    public bool IsJoystickHidden { get; private set; }
    public bool IsSettingsOpen { get; private set; }
    public BottomBarUI BottomBar => _bottomUI;
    public BoosterUI BoosterUI => _boosterUI;
    public AdTimerUI AdTimerUI => _adTimerUI;

    private void Start()
    {
        _loading.gameObject.SetActive(true);

        HideTasks();
        HideDailyRewards();
        HideBooster();
        HideSettings();
        HideHint();
        HideAdTimer();
        HideNoAds();

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
    
    private void PopUp(Transform transform)
    {
        transform.localScale = new Vector3(0.5f, 0.5f);
        transform.DOScale(new Vector3(1f, 1f), 0.1f).SetEase(Ease.OutElastic);
    }

    public void FadeIn()
    {
        StartCoroutine(StartFadeIn());
    }

    public void FadeOut()
    {
        StartCoroutine(StartFadeOut());
    }

    public void Show()
    {
        _uiContainer.SetActive(true);

        IsUIHiden = false;
    }

    public void Hide()
    {
        _uiContainer.SetActive(false);

        IsUIHiden = true;
    }

    public void ShowTasks()
    {
        _midleSectionContainer.SetActive(false);
        _tasksUI.Initialize();
        _tasksUI.gameObject.SetActive(true);
        PopUp(_tasksUI.transform);
    }

    public void HideTasks()
    {
        _midleSectionContainer.SetActive(true);
        _tasksUI.Deinitialize();
        _tasksUI.gameObject.SetActive(false);
    }

    public void ShowDailyRewards()
    {
        _midleSectionContainer.SetActive(false);
        _dailyRewardsUI.Initialize();
        _dailyRewardsUI.gameObject.SetActive(true);
        PopUp(_dailyRewardsUI.transform);
    }

    public void HideDailyRewards()
    {
        _midleSectionContainer.SetActive(true);
        _dailyRewardsUI.gameObject.SetActive(false);
    }

    public void ShowBooster(BoostSO boostSO)
    {
        _midleSectionContainer.SetActive(false);
        _boosterUI.Initialize(boostSO);
        _boosterUI.gameObject.SetActive(true);
        PopUp(_boosterUI.transform);
    }

    public void HideBooster()
    {
        _midleSectionContainer.SetActive(true);
        _boosterUI.gameObject.SetActive(false);
    }

    public void ShowSettings()
    {
        if (IsSettingsOpen)
        {
            HideSettings();
            return;
        }

        _settingsUI.Initialize();
        _settingsUI.gameObject.SetActive(true);
        IsSettingsOpen = true;

        PopUp(_settingsUI.transform);
    }

    public void HideSettings()
    {
        _settingsUI.gameObject.SetActive(false);
        IsSettingsOpen = false;
    }

    public void ShowHint(string message)
    {
        _hintBar.gameObject.SetActive(true);
        _hintText.text = message;
        PopUp(_hintBar.transform);
    }

    public void HideHint()
    {
        _hintBar.gameObject.SetActive(false);
    }

    public void ShowAdTimer()
    {
        _adTimerUI.gameObject.SetActive(true);
        _adTimerUI.Initialize();
    }

    public void HideAdTimer()
    {
        _adTimerUI.Deinitialize();
        _adTimerUI.gameObject.SetActive(false);
    }

    public void ShowNoAds()
    {
        if (!_noAdsUI.gameObject.activeSelf)
        {
            _noAdsUI.gameObject.SetActive(true);
            PopUp(_noAdsUI.transform);
        }
    }

    public void HideNoAds()
    {
        _noAdsUI.gameObject.SetActive(false);
    }
}