using TMPro;
using UnityEngine;

public class TerminalUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _summaryText;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private ResourceStack _resources;

    private void Start()
    {
        GameTimeManager.Instance.OnDateTimeChanged += OnDateTimeChanged;

        _resources.OnStuckChange += OnStuckChange;
    }

    private void OnDateTimeChanged(InGameDateTime dateTime)
    {
        if (_resources.StuckValue != 0)
        {
            var energyPrice = StockMarket.Instance.EnergyPrice;

            _summaryText.text = $"Summary: {(int)(energyPrice * _resources.StuckValue)}";

            _priceText.text = $"Price: {energyPrice:0.##}";
        }
    }

    private void OnStuckChange()
    {
        if (_resources.StuckValue == 0)
        {
            _summaryText.text = $"Summary: 0";

            _priceText.text = $"Price: 0";
        }
    }
}
