using UnityEngine;

public class BuildArea : BuyArea
{
    [Header("Building parametrs")]
    [SerializeField] private GameObject _building;
    [SerializeField] private ProductionBuilding _productionBuilding;

    private void Start()
    {
        _building.SetActive(false);
    }

    protected override void Bought()
    {
        Build();
    }

    public void Build()
    {
        _building.SetActive(true);

        Destroy(this.gameObject);

        EventManager.TriggerEvent(new BuildingUpdatedEvent() { productionBuilding = _productionBuilding });
    }
}
