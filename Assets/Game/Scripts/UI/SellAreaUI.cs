using UnityEngine;

public class SellAreaUI : ProgressAreaUI
{
    [Header("Sell settings")]
    [SerializeField] private ResourceStack _stack;

    protected override void OnPlayerTrigger(bool inside)
    {
        base.OnPlayerTrigger(inside);

        if (_stack.StuckValue > 0)
        {
            StartCoroutine(ProgressCoroutineAnimation());
        }
    }
}
