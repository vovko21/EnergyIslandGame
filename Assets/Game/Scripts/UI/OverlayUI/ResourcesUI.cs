using TMPro;
using UnityEngine;

public class ResourcesUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dolarsText;
    [SerializeField] private TextMeshProUGUI _diamandsText;

    private void OnEnable()
    {
        ProgressionManager.Instance.Wallet.OnDollarsChanged += OnDollarsChanged;
        ProgressionManager.Instance.Wallet.OnDiamandsChanged += OnDiamandsChanged;

        OnDollarsChanged(ProgressionManager.Instance.Wallet.Dollars);
        OnDiamandsChanged(ProgressionManager.Instance.Wallet.Diamands);
    }

    private void OnDisable()
    {
        ProgressionManager.Instance.Wallet.OnDollarsChanged -= OnDollarsChanged;
        ProgressionManager.Instance.Wallet.OnDiamandsChanged -= OnDiamandsChanged;
    }

    private void OnDollarsChanged(int value)
    {
        _dolarsText.text = ProgressionManager.Instance.GetFormatedValue(value);
    }

    private void OnDiamandsChanged(int value)
    {
        _diamandsText.text = ProgressionManager.Instance.GetFormatedValue(value);
    }
}
