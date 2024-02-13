using UnityEngine;

[RequireComponent(typeof(ProductionBuilding))]
public class GatheringObject : MonoBehaviour, IInteractable
{
    private ProductionBuilding _productionBuilding;

    private void Start()
    {
        _productionBuilding = GetComponent<ProductionBuilding>();
    }

    public void Interact()
    {
        Debug.Log(ProgressionManager.Instance.Wallet.Coins);
        ProgressionManager.Instance.Wallet.AddCoins((int)_productionBuilding.Gather());
        Debug.Log(ProgressionManager.Instance.Wallet.Coins);
    }
}
