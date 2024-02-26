using System;
using System.Collections;
using UnityEngine;

public class InteractWithBuildingArea : InteractableArea
{
    [SerializeField] private ProductionBuilding _productionBuilding;

    private IEnumerator _coroutine;
    private float _timePassed;

    public event Action<bool> OnMaintenceChanged;
    public event Action<bool> OnFixingChanged;

    protected override void ContactWithPlayer(Player player)
    {
        if(_productionBuilding.Status == BuildingStatus.Maintenance)
        {
            if (_coroutine == null && _productionBuilding is RenewableEnergyBuilding)
            {
                _coroutine = StartMaintenance();

                StartCoroutine(_coroutine);
            }
        }
        else if(_productionBuilding.Status == BuildingStatus.Broken)
        {
            if (player.Hands.CurrentItem?.Type == HandItemType.Wrench)
            {
                if (_coroutine == null)
                {
                    _coroutine = StartFixing();

                    StartCoroutine(_coroutine);
                }
            }
        }
    }

    protected override void ContactWithWorker(Worker worker)
    {
        if (_productionBuilding.Status != BuildingStatus.Maintenance) return;

        if(_coroutine != null) StopCoroutine(_coroutine);

        _coroutine = StartMaintenance();

        StartCoroutine(_coroutine);
    }

    protected override void PlayerExit(Player other)
    {
        if (_coroutine == null) return;

        if (_productionBuilding.Status == BuildingStatus.Maintenance)
        {
            OnMaintenceChanged?.Invoke(false);
        }
        else if (_productionBuilding.Status == BuildingStatus.Broken)
        {
            OnFixingChanged?.Invoke(false);
        }

        StopCoroutine(_coroutine);
        _coroutine = null;
    }

    private IEnumerator StartMaintenance()
    {
        OnMaintenceChanged?.Invoke(true);
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

        ((RenewableEnergyBuilding)_productionBuilding).Maintenanced();
    }

    private IEnumerator StartFixing()
    {
        OnFixingChanged?.Invoke(true);
        bool isFinished = false;
        var timeToFix = _productionBuilding.CurrentStats.MaintenanceTime;

        while (!isFinished)
        {
            _timePassed += Time.deltaTime;

            if (_timePassed >= timeToFix)
            {
                isFinished = true;
                _timePassed = 0;
            }

            yield return null;
        }

        _productionBuilding.Fix();
    }
}
