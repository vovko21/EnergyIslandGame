using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Boost")]
public class BoostSO : ScriptableObject
{
    [field: SerializeField] public int Speed { get; private set; }
    [field: SerializeField] public int Dollars { get; private set; }
    [field: SerializeField] public int Diamands { get; private set; }
}
