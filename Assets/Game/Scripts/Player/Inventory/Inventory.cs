using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Items")]
    [SerializeField] private List<HandItem> _items;

    protected HandItem _previousItem;
    protected HandItem _currentItem;

    public HandItem CurrentItem => _currentItem;
    public bool IsItemInHand => _currentItem != null;

    private void Start()
    {
        DeactivateAll();
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

    public void DeactivateAll()
    {
        foreach (var item in _items)
        {
            item.Deactivate();
        }
    }
}
