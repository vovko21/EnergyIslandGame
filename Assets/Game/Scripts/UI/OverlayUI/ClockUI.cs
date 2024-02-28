using TMPro;
using UnityEngine;

public class ClockUI : MonoBehaviour
{
    [SerializeField] private RectTransform _clockFace;
    [SerializeField] private TextMeshProUGUI _day, _time;

    private float _startingRotation;

    private void OnEnable()
    {
        _startingRotation = _clockFace.localEulerAngles.z;
        GameTimeManager.Instance.OnDateTimeChanged += OnDateTimeChanged;
    }

    private void OnDisable()
    {
        GameTimeManager.Instance.OnDateTimeChanged -= OnDateTimeChanged;
    }

    private void OnDateTimeChanged(InGameDateTime dateTime)
    {
        _day.text = "DAY " + dateTime.TotalNumDays.ToString();
        _time.text = dateTime.TimeToString();

        float newRotation = Mathf.Lerp(0, 360, dateTime.Hour / 24f);

        _clockFace.localEulerAngles = new Vector3 (0, 0, newRotation);
    }
}
