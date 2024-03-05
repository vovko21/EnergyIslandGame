using TMPro;
using UnityEngine;

public class TerminalUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _summaryText;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private EnergyBank _bank;

    private void Start()
    {
        GameTimeManager.Instance.OnDateTimeChanged += OnDateTimeChanged;
    }

    private void OnDateTimeChanged(InGameDateTime dateTime)
    {
        if (_bank.Energy > 0)
        {
            var energyPrice = StockMarket.Instance.EnergyPrice;

            _summaryText.text = $"Summary: {(int)(energyPrice * _bank.Energy)}";

            _priceText.text = $"Price: {energyPrice:0.##}";
        }
        else
        {
            _summaryText.text = $"Summary: 0";

            _priceText.text = $"Price: 0";
        }
    }
}
