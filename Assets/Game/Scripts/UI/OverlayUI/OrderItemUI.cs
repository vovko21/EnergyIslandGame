using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderItemUI : MonoBehaviour
{
    [SerializeField] private OrderResourceSO _orderSO;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Button _buyButton;

    public Button BuyButton => _buyButton;
    public OrderResourceSO OrderSO => _orderSO;

    private void Start()
    {
        _text.text = _orderSO.Price + " " + _orderSO.Type.ToString();
    }
}
