using TMPro;
using UnityEngine;

public class DollarsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    private void Start()
    {
        ProgressionManager.Instance.Wallet.OnDollarsChanged += OnDollarsChanged;

        _textMeshPro.text = ProgressionManager.Instance.Wallet.Dollars.ToString();
    }

    private void OnDisable()
    {
        ProgressionManager.Instance.Wallet.OnDollarsChanged -= OnDollarsChanged;
    }

    private void OnDollarsChanged(int obj)
    {
        _textMeshPro.text = obj.ToString();
    }
}
