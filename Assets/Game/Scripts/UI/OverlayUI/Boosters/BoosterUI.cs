using UnityEngine;
using UnityEngine.UI;

public class BoosterUI : MonoBehaviour
{
    [SerializeField] private ButtonUI _diamondsButton;
    [SerializeField] private ButtonUI _watchBatton;
    [SerializeField] private int _diamandsPrice;

    private BoostSO _boostSO;

    public Button DiamondsButton => _diamondsButton.Button;
    public Button WatchButton => _watchBatton.Button;

    private void OnEnable()
    {
        SetDiamondsButton();
    }

    public void Initialize(BoostSO boostSO)
    {
        _boostSO = boostSO;
    }

    private void SetDiamondsButton()
    {
        _diamondsButton.SetText(_diamandsPrice.ToString());

        if (ProgressionManager.Instance.Wallet.Diamands < _diamandsPrice)
        {
            _diamondsButton.SetInactive();
        }
        else
        {
            _diamondsButton.SetActive();
        }
    }
}
