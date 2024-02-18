using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] private Light _sun;
    [SerializeField] private AnimationCurve _intensityCurve;

    private void OnEnable()
    {
        TimeManager.Instance.OnDateTimeChanged += OnDateTimeChanged;
    }

    private void OnDisable()
    {
        TimeManager.Instance.OnDateTimeChanged -= OnDateTimeChanged;
    }

    private void OnDateTimeChanged(InGameDateTime dateTime)
    {
        _sun.intensity = _intensityCurve.Evaluate((dateTime.Minute + dateTime.Hour * 60f) / (24f * 60f));
    }
}
