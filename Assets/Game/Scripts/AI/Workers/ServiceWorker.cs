using System;
using UnityEngine;

public class ServiceWorker : Worker
{
    [SerializeField] private Transform _waitPosition;

    protected MaintenanceState _maintenceState;
    protected int _currentState = 0;

    private void Start()
    {
        //STATES
        var waitState = new WaitState(this, _waitPosition);
        _maintenceState = new MaintenanceState();

        //TRANSITIONS
        Any(waitState, () => _currentState == 0);

        At(waitState, _maintenceState, () => _currentState == 1);

        //START STATE
        _stateMachine.SetState(waitState);

        //FUNCTIONS & CONDITIONALS
        void At(IState from, IState to, Func<bool> conditional) => _stateMachine.AddTransition(from, to, conditional);
        void Any(IState to, Func<bool> conditional) => _stateMachine.AddAnyTransition(to, conditional);

        InvokeRepeating(nameof(CheckStates), 0.5f, 0.5f);
    }

    public void NextState()
    {
        if (_currentState + 1 > 1)
        {
            _currentState = 0;
        }
        else
        {
            _currentState++;
        }
    }

    public void CheckStates()
    {
        if (_currentState == 0)
        {
            foreach (var building in BuildingManager.Instance.ActiveBuildings)
            {
                if (building.Status == BuildingStatus.Maintenance)
                {
                    _maintenceState.Initialize(this, building.GatherPoint, building.CurrentStats.MaintenanceTime);

                    NextState();

                    break;
                }
            }
        }
    }
}
