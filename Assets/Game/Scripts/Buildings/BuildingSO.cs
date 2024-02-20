using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ProducitonBuilding")]
public class BuildingSO : ScriptableObject
{
    [field: SerializeField] public int ProductionPerGameHour { get; private set; }
    [field: SerializeField] public int MaxSupply { get; private set; }
}
