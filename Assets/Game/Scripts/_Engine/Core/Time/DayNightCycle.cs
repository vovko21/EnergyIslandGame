using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private Light _sun;

    [Header("Settings")]
    [SerializeField] private AnimationCurve _intensityCurve;
    [SerializeField] private AnimationCurve _fogDensity;
    [SerializeField] private Gradient _directionalColorGradient;

    private void OnEnable()
    {
        GameTimeManager.Instance.OnDateTimeChanged += OnDateTimeChanged;
    }

    private void OnDisable()
    {
        GameTimeManager.Instance.OnDateTimeChanged -= OnDateTimeChanged;
    }

    private void OnDateTimeChanged(InGameDateTime dateTime)
    {
        float time = (dateTime.Minute + dateTime.Hour * 60f) / (24f * 60f);
        _sun.intensity = _intensityCurve.Evaluate(time);

        RenderSettings.fogDensity = _fogDensity.Evaluate(time);
        //_sun.color = _directionalColorGradient.Evaluate(time);

    }
}
