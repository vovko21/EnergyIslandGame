using UnityEngine;

public class OrderArea : InteractableArea
{
    [SerializeField] private UserInterface _ui;
    [SerializeField] private OrderManager _orderManager;

    private void OnEnable()
    {
        _ui.BottomBar.OnBuyPress += OnBuyPress;
    }

    private void OnDisable()
    {
        _ui.BottomBar.OnBuyPress -= OnBuyPress;
    }

    protected override void ContactWithPlayer(Player player)
    {
        _ui.BottomBar.ShowOrders();
    }

    protected override void PlayerExit(Player player)
    {
        _ui.BottomBar.HideOrders();
    }

    private void OnBuyPress(OrderResourceSO orderSO)
    {
        bool result = ProgressionManager.Instance.Wallet.TrySpend(orderSO.Price);

        if (result)
        {
            _orderManager.AddResource(orderSO.Type, orderSO.Value);
        }
    }
}
