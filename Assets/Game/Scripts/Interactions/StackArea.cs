using System.Collections;
using UnityEngine;

public class StackArea : InteractableArea
{
    [SerializeField] private ResourceStack _energyStack;
    [SerializeField] private int _stackPerTick = 10;

    private const float RATE = 0.25f;
    private IEnumerator _coroutine;

    public float Rate => RATE;

    protected override void ContactWithPlayer(Collider other)
    {
        if (_coroutine == null)
        {
            _coroutine = StartStack(other.GetComponent<Player>());

            StartCoroutine(_coroutine);
        }
    }

    protected override void PlayerExit(Collider other)
    {
        if (_coroutine == null) return;

        StopCoroutine(_coroutine);
        _coroutine = null;
    }

    private IEnumerator StartStack(Player player)
    {
        bool isFinished = false;

        var energyToStack = player.CarrySystem.StuckValue;

        if (energyToStack <= 0)
        {
            isFinished = true;
        }

        while (!isFinished)
        {
            yield return new WaitForSeconds(RATE);

            energyToStack -= _stackPerTick;

            player.CarrySystem.UpdateEnergyStack(energyToStack);

            _energyStack.AddToStuck(_stackPerTick);

            if (energyToStack <= 0)
            {
                isFinished = true;
                player.CarrySystem.ClearAll();
            }
        }
    }
}
