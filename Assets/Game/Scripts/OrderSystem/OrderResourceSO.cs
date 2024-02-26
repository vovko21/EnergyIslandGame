using UnityEngine;

[CreateAssetMenu(fileName = "OrderData", menuName = "ScriptableObjects/Order")]
public class OrderResourceSO : ScriptableObject
{
    [field: SerializeField] public EnergyResourceType Type { get; private set; }
    [field: SerializeField] public int Price { get; private set; }
    [field: SerializeField] public int Value { get; private set; }
}
