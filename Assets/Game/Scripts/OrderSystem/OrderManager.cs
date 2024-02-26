using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public List<MyDicitonaryItem> items;

    private Dictionary<EnergyResourceType, EnergyResourcesArea> _energyAreas = new Dictionary<EnergyResourceType, EnergyResourcesArea>();

    private void Start()
    {
        foreach (var item in items)
        {
            _energyAreas.Add(item.type, item.area);
        }
    }

    public bool AddResource(EnergyResourceType type, int value)
    {
        var area = _energyAreas.GetValueOrDefault(type);

        if (area == null) return false;

        area.AddValue(value);

        return true;
    }
}

[System.Serializable]
public class MyDicitonaryItem
{
    public EnergyResourceType type;
    public EnergyResourcesArea area;
}
