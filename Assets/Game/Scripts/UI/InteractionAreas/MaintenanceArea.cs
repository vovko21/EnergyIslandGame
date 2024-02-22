using System;
using System.Collections;
using UnityEngine;

public class MaintenanceArea : InteractableArea
{
    [SerializeField] private ProductionBuilding _productionBuilding;

    private IEnumerator _coroutine;
    private float _timePassed;

    public event Action OnMaintenceStart;

    protected override void ContactWithPlayer(Player other)
    {
        if (_productionBuilding.Status != BuildingStatus.Maintenance) return;

        if (_coroutine == null)
        {
            _coroutine = StartMaintenance();

            StartCoroutine(_coroutine);
        }
    }

    protected override void PlayerExit(Player other)
    {
        if (_coroutine == null) return;

        StopCoroutine(_coroutine);
        _coroutine = null;
    }

    private IEnumerator StartMaintenance()
    {
        OnMaintenceStart?.Invoke();
        bool isFinished = false;
        var timeToMaintenance = _productionBuilding.CurrentStats.MaintenanceTime;

        while (!isFinished)
        {
            _timePassed += Time.deltaTime;

            if (_timePassed >= timeToMaintenance)
            {
                isFinished = true;
                _timePassed = 0;
            }

            yield return null;
        }

        _productionBuilding.Maintenanced();
    }
}
