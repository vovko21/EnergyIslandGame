using TMPro;
using UnityEngine;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    public void Initialize()
    {
        _textMeshPro.text = TimeManager.Instance.IsServerTimeSuccess ? "ONLINE" : "PROBLEM";

        _textMeshPro.color = TimeManager.Instance.IsServerTimeSuccess ? Color.green : Color.red;
    }
}
