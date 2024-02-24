using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public struct CarryObject
{
    public EnergyResourceType type;
    public GameObject prefab;
    public int maxCarryCount;
}

public class CarrySystem : MonoBehaviour
{
    [Header("Stack settings")]
    [SerializeField] private ResourceStack _stack;
    [SerializeField] private List<CarryObject> _carryObject;

    [Header("Items")]
    [SerializeField] private List<HandItem> _items;

    protected CarryObject _currentObject;
    protected HandItem _previousItem;
    protected HandItem _currentItem;

    public bool IsCarrying { get; private set; }
    public bool IsMax => _stack.IsMax;
    public int StuckValue => _stack.StuckValue;
    public CarryObject CurrentObject => _currentObject;
    public HandItem CurrentItem => _currentItem;

    public event Action<CarrySystem> OnStackChanged;

    public int AddToStack(EnergyResourceType type, int value)
    {
        if (IsCarrying)
        {
            if (_currentObject.type == type)
            {
                return AddToStack(value);
            }
            else
            {
                return -1;
            }
        }

        _currentObject = GetObjectByType(type);

        SetStackObjectByCurrent();

        return AddToStack(value);
    }

    public int UpdateStack(EnergyResourceType type, int value)
    {
        if (IsCarrying)
        {
            if (_currentObject.type == type)
            {
                return UpdateStack(value);
            }
            else
            {
                return -1;
            }
        }

        SetStackObjectByCurrent();

        _currentObject = GetObjectByType(type);

        return UpdateStack(value);
    }

    private int AddToStack(int value)
    {
        if (value <= 0)
        {
            return -1;
        }

        var overflow = _stack.AddToStuck(value);

        if (!IsCarrying)
        {
            IsCarrying = true;
        }

        OnStackChanged?.Invoke(this);

        return overflow;
    }

    private int UpdateStack(int value)
    {
        if (value <= 0)
        {
            return -1;
        }

        _stack.UpdateStack(value);

        if (!IsCarrying)
        {
            IsCarrying = true;
        }

        OnStackChanged?.Invoke(this);

        return 0;
    }

    public void ClearStack()
    {
        _stack.ClearStack();

        IsCarrying = false;

        OnStackChanged?.Invoke(this);
    }

    public bool TakeItem(HandItemType itemType)
    {
        foreach (var item in _items)
        {
            if (item.Type == itemType)
            {
                _previousItem?.Deactivate();
                _previousItem = _currentItem;
                _currentItem = item;
                _currentItem.Activate();

                return true;
            }
        }

        return false;
    }

    private void SetStackObjectByCurrent()
    {
        ClearStack();
        _stack.Initialize(_currentObject.maxCarryCount);
        _stack.ChangeItemPrefab(_currentObject.prefab);
    }

    private CarryObject GetObjectByType(EnergyResourceType type)
    {
        return _carryObject.Where(x => x.type == type).FirstOrDefault();
    }
}
