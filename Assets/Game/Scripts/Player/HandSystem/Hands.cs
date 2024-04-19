using System;
using UnityEngine;

[RequireComponent(typeof(CarrySystem))]
[RequireComponent(typeof(Inventory))]
public class Hands : MonoBehaviour
{
    private CarrySystem _carrySystem;
    private Inventory _inventory;

    public HandItem CurrentItem => _inventory.CurrentItem;
    public CarryObject CurrentObject => _carrySystem.CurrentObject;
    public int StuckValue => _carrySystem.StuckValue;
    public Inventory Inventory => _inventory;   

    public event Action<CarrySystem> OnStackChanged;

    private void Awake()
    {
        _carrySystem = GetComponent<CarrySystem>();
        _inventory = GetComponent<Inventory>();
    }

    private void OnEnable()
    {
        _carrySystem.OnStackChanged += CarrySystem_OnChange;
    }

    private void OnDisable()
    {
        _carrySystem.OnStackChanged -= CarrySystem_OnChange;
    }

    public int AddToStack(EnergyResourceType type, int value)
    {
        if (_inventory.IsItemInHand) return -1;
        return _carrySystem.AddToStack(type, value);
    }

    public int UpdateStack(EnergyResourceType type, int value)
    {
        if (_inventory.IsItemInHand) return -1;
        return _carrySystem.UpdateStack(type, value);
    }

    public void ClearStack()
    {
        _carrySystem.ClearStack();
    }

    public bool TakeItem(HandItemType itemType)
    {
        if (_carrySystem.IsCarrying) return false;
        return _inventory.TakeItem(itemType);
    }

    public void HideItem()
    {
        _inventory.DeactivateAll();
    }

    private void CarrySystem_OnChange(CarrySystem system)
    {
        OnStackChanged?.Invoke(system);
    }
}
