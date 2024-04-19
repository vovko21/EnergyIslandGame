using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _buttonText;
    [SerializeField] private Button _button;

    public Button Button => _button;

    public void SetText(string text)
    {
        if (_buttonText == null) return;
        _buttonText.text = text;
    }

    public void SetInactive()
    {
        _button.interactable = false;
    }

    public void SetActive()
    {
        _button.interactable = true;
    }
}
