using UnityEngine;

public class SellAreaUI : ProgressAreaUI
{
    [Header("Sell settings")]
    [SerializeField] private ResourceStack _stack;

    protected override void OnCharacterTrigger(bool inside)
    {
        base.OnCharacterTrigger(inside);

        if (_stack.StuckValue > 0)
        {
            StartCoroutine(ProgressCoroutineAnimation());
        }
    }
}
