using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private Light _sun;

    [Header("Settings")]
    [SerializeField] private AnimationCurve _intensityCurve;
    [SerializeField] private AnimationCurve _fogDensity;
    [SerializeField] private Gradient _directionalColorGradient;

    [Header("Audio")]
    [SerializeField] private AudioSource _daySource;
    [SerializeField] private AudioSource _nightSource;

    private void OnEnable()
    {
        GameTimeManager.Instance.OnDateTimeChanged += OnDateTimeChanged;
    }

    private void OnDisable()
    {
        GameTimeManager.Instance.OnDateTimeChanged -= OnDateTimeChanged;
    }

    private void Start()
    {
        _daySource.Stop();
        _nightSource.Stop();
    }

    private void OnDateTimeChanged(InGameDateTime dateTime)
    {
        float time = (dateTime.Minute + dateTime.Hour * 60f) / (24f * 60f);
        _sun.intensity = _intensityCurve.Evaluate(time);

        RenderSettings.fogDensity = _fogDensity.Evaluate(time);

        if (dateTime.IsNight())
        {
            if(!_nightSource.isPlaying)
            {
                _daySource.Stop();
                _nightSource.Play();
            }
        }
        else
        {
            if (!_daySource.isPlaying)
            {
                _nightSource.Stop();
                _daySource.Play();
            }
        }

        //_sun.color = _directionalColorGradient.Evaluate(time);
    }
}
