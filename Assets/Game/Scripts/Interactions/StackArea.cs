using System.Collections;
using UnityEngine;

public class StackArea : InteractableArea
{
    [SerializeField] private ResourceStack _energyStack;
    [SerializeField] private int _stackPerTick = 10;

    private const float RATE = 0.1f;
    private IEnumerator _playerCoroutine;
    private IEnumerator _workerCoroutine;

    public float Rate => RATE;

    protected override void ContactWithPlayer(Player player)
    {
        if (_playerCoroutine == null)
        {
            _playerCoroutine = StartStack(player.CarrySystem);

            StartCoroutine(_playerCoroutine);
        }
    }

    protected override void ContactWithWorker(Worker worker)
    {
        if (worker is CarrierWorker)
        {
            if (_workerCoroutine == null)
            {
                _workerCoroutine = StartStack(((CarrierWorker)worker).CarrySystem);

                StartCoroutine(_workerCoroutine);
            }   
        }
    }

    protected override void PlayerExit(Player player)
    {
        if (_playerCoroutine == null) return;

        StopCoroutine(_playerCoroutine);
        _playerCoroutine = null;
    }

    protected override void WorkerExit(Worker worker)
    {
        if (_workerCoroutine == null) return;

        StopCoroutine(_workerCoroutine);
        _workerCoroutine = null;
    }

    private IEnumerator StartStack(CarrySystem carrySystem)
    {
        bool isFinished = false;

        var energyToStack = carrySystem.StuckValue;

        if (energyToStack <= 0)
        {
            isFinished = true;
        }

        while (!isFinished)
        {
            yield return new WaitForSeconds(RATE);
            
            energyToStack -= _stackPerTick;

            carrySystem.UpdateEnergyStack(energyToStack);

            _energyStack.AddToStuck(_stackPerTick);

            if (energyToStack <= 0)
            {
                isFinished = true;
                carrySystem.ClearAll();
            }
        }
    }
}
