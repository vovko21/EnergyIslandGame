using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] private Light _sun;
    [SerializeField] private AnimationCurve _intensityCurve;

    private void Update()
    {
        _sun.intensity = _intensityCurve.Evaluate(Time.deltaTime);
    }
}
