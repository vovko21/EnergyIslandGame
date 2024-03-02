using UnityEngine;

public class SellAreaUI : ProgressAreaUI
{
    [Header("Sell settings")]
    [SerializeField] private EnergyBank _energyBank;

    protected override void OnCharacterTrigger(bool inside)
    {
        base.OnCharacterTrigger(inside);

        if (_energyBank.Energy > 0)
        {
            StartCoroutine(ProgressCoroutineAnimation());
        }
    }
}
