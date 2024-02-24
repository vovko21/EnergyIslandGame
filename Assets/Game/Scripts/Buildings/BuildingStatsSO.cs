using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ProducitonBuilding")]
public class BuildingStatsSO : ScriptableObject
{
    [field: Header("Main settings")]
    [field: SerializeField] public int ProductionPerGameHour { get; private set; }
    [field: SerializeField] public int MaxSupply { get; private set; }
    [field: SerializeField] public float MaintainingTime { get; private set; }

    [field:Header("Traditional energy settings")]
    [field: SerializeField] public int Consumption { get; private set; }
}
