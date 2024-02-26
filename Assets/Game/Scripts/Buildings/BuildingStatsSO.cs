using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuildingStat 
{
    [field: SerializeField] public int Price { get; private set; }
    [field: SerializeField] public int Value { get; private set; }
}

[CreateAssetMenu(menuName = "ScriptableObjects/ProducitonBuilding")]
public class BuildingStatsSO : ScriptableObject
{
    [field: Header("Main settings")]
    [field: SerializeField] public List<BuildingStat> ProductionPerGameHour { get; private set; }
    [field: SerializeField] public List<BuildingStat> MaxSupply { get; private set; }
    [field: SerializeField] public float MaintainingTime { get; private set; }

    [field:Header("Traditional energy settings")]
    [field: SerializeField] public int Consumption { get; private set; }
}
