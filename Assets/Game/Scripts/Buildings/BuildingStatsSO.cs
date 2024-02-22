using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ProducitonBuilding")]
public class BuildingStatsSO : ScriptableObject
{
    [field: SerializeField] public int ProductionPerGameHour { get; private set; }
    [field: SerializeField] public int MaxSupply { get; private set; }
    [field: SerializeField] public float MaintainingTime { get; private set; }
}
