using System;
using UnityEngine;

public class EnergyBank : MonoBehaviour
{
    #region ReadOnly
#if UNITY_EDITOR
    [SerializeField]
    [ReadOnly]
#endif
#endregion
    private int _energy;

    public int Energy => _energy;

    public event Action OnAdded;

    public void AddEnergy(int energy)
    {
        if (energy <= 0)
        {
            return;
        }

        _energy += energy;

        OnAdded?.Invoke();
    }

    public void ClearEnergy()
    {
        _energy = 0;
    }
}
