using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarView : MonoBehaviour
{
    [Header("VISUAL")]
    [SerializeField] private Slider _slider;
    [SerializeField] private Gradient _gradient;

    [Header("DATA")]
    [SerializeField] private TMP_Text _dataText;

    public void SetMaxValue(float value)
    {
        _slider.maxValue = value;
    }

    public void SetValue(float value)
    {
        _slider.value = value;
    }

    public void SetDataText(string text)
    {
        if (_dataText == null) return;
        _dataText.text = text;
    }
}
