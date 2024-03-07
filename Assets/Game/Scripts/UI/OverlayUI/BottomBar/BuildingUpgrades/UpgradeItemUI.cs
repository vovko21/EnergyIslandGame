using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _valueText;
    [SerializeField] private ButtonUI _buyButton;

    private BuildingStat _buildingStat;
    private string _buildingId;

    public BuildingStat BuildingStat => _buildingStat;
    public string BuildingId => _buildingId;
    public Button BuyButton => _buyButton.Button;

    public void Initialize(string buildingId, BuildingStat buildingStat)
    {
        _buildingStat = buildingStat;
        _buildingId = buildingId;

        if (_buildingStat == null)
        {
            _buyButton.SetInactive();
            _buyButton.SetText("MAX");
            _priceText.text = "";
        }
        else
        {
            _buyButton.SetActive();
            _buyButton.SetText("BUY");
            DisplayText();
        }
    }

    private void DisplayText()
    {
        _priceText.text = _buildingStat.Price.ToString() + "$";
        _valueText.text = _buildingStat.Value.ToString();
    }
}
