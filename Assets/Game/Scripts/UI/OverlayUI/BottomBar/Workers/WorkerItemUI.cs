using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorkerItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private ButtonUI _buyButton;

    private int _price;

    public Button BuyButton => _buyButton.Button;

    public void Initialize(int price)
    {
        _price = price;

        if (price <= 0)
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
        _priceText.text = _price.ToString() + "$";
    }
}
