using System;
using UnityEngine;

[RequireComponent(typeof(CarrySystem))]

public class CarrierWorker : Worker
{
    [Header("Sell Setup")]
    [SerializeField] private Transform _sellPosition;

    protected TakeProducedState _takeProducedState;
    protected int _currentState = 0;

    public CarrySystem CarrySystem { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        CarrySystem = GetComponent<CarrySystem>();
    }

    protected virtual void OnEnable()
    {
        CarrySystem.OnStackChanged += CarrySystem_OnChange;
    }

    protected virtual void OnDisable()
    {
        CarrySystem.OnStackChanged -= CarrySystem_OnChange;
    }

    private void Start()
    {
        //STATES
        var waitState = new WaitState(this);
        _takeProducedState = new TakeProducedState();
        var sellState = new SellState(this, _sellPosition);

        //TRANSITIONS
        Any(waitState, () => _currentState == 0);

        At(waitState, _takeProducedState, () => _currentState == 1);
        At(_takeProducedState, sellState, () => _currentState == 2);

        //START STATE
        _stateMachine.SetState(waitState);

        //FUNCTIONS & CONDITIONALS
        void At(IState from, IState to, Func<bool> conditional) => _stateMachine.AddTransition(from, to, conditional);
        void Any(IState to, Func<bool> conditional) => _stateMachine.AddAnyTransition(to, conditional);

        InvokeRepeating(nameof(CheckStates), 0.5f, 0.5f);
    }

    public void NextState()
    {
        if (_currentState + 1 > 2)
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
                if (building.Status == BuildingStatus.Producing && building.Produced >= building.MinGatherAmount)
                {
                    _takeProducedState.Initialize(this, building.GatherPoint);

                    NextState();

                    break;
                }
            }
        }
    }

    private void CarrySystem_OnChange(CarrySystem system)
    {
        AnimationController.SetCarrying(system.IsCarrying);
    }
}
