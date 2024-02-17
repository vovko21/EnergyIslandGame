using TMPro;
using UnityEngine;

public class DollarsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    private void Start()
    {
        ProgressionManager.Instance.Wallet.OnDollarsChanged += OnDollarsChanged;

        OnDollarsChanged(ProgressionManager.Instance.Wallet.Dollars);
    }

    private void OnDisable()
    {
        ProgressionManager.Instance.Wallet.OnDollarsChanged -= OnDollarsChanged;
    }

    private void OnDollarsChanged(int value)
    {
        string[] suffixes = { "", "K", "M", "B", "T" };

        int suffixIndex = 0;
        float num = value;
        while (Mathf.Abs(num) >= 1000 && suffixIndex < suffixes.Length - 1)
        {
            num /= 1000;
            suffixIndex++;
        }

        string formattedNumber = $"{num:0.##}{suffixes[suffixIndex]}";

        _textMeshPro.text = formattedNumber;
    }
}
