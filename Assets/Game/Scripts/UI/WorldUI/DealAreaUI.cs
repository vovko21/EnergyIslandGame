using TMPro;
using UnityEngine;

public class DealAreaUI : MonoBehaviour
{
    [SerializeField] private GoodDealEvent _goodDealEvent;
    [SerializeField] private TextMeshProUGUI _textData;

    private void OnEnable()
    {
        _goodDealEvent.EnergyBank.OnAdded += OnAddedInBank;

        OnAddedInBank();
    }

    private void OnDisable()
    {
        _goodDealEvent.EnergyBank.OnAdded -= OnAddedInBank;
    }

    private void OnAddedInBank()
    {
        _textData.text = $"{_goodDealEvent.EnergyBank.Energy}/{_goodDealEvent.EnergyDeffaultCount}";
    }
}
