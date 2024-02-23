using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] private Light _sun;
    [SerializeField] private AnimationCurve _intensityCurve;

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
        _sun.intensity = _intensityCurve.Evaluate((dateTime.Minute + dateTime.Hour * 60f) / (24f * 60f));
    }
}
