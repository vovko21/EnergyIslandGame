using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CarrySystem))]
public class Worker : MonoBehaviour, IEventListener<ProducedEvent>
{
    [Header("Brain Setup")]
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private CharacterAnimationController _animationController;

    [Header("Sell Setup")]
    [SerializeField] private Transform _sellPosition;

    private StateMachine _stateMachine;
    private TakeProducedState _takeProducedState;
    private int _currentState = 0;

    public NavMeshAgent Agent => _agent;
    public CharacterAnimationController AnimationController => _animationController;
    public CarrySystem CarrySystem { get; private set; }

    private void Awake()
    {
        _stateMachine = new StateMachine();
        CarrySystem = GetComponent<CarrySystem>();
    }

    private void OnEnable()
    {
        this.StartListeningEvent();

        CarrySystem.OnChange += CarrySystem_OnChange;
    }

    private void OnDisable()
    {
        this.StopListeningEvent();

        CarrySystem.OnChange -= CarrySystem_OnChange;
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
    }

    private void Update()
    {
        _stateMachine.Tick();
    }

    public void NextState()
    {
        if(_currentState + 1 > 2)
        {
            _currentState = 0;
        }
        else
        {
            _currentState++;
        }
    }

    public void OnEvent(ProducedEvent eventType)
    {
        if (_currentState == 0)
        {
            _takeProducedState.Initialize(this, eventType.building.GatherPoint);

            NextState();
        }
    }

    private void CarrySystem_OnChange(CarrySystem system)
    {
        AnimationController.SetCarrying(system.IsCarrying);
    }

    private void OnDrawGizmos()
    {
        if (_stateMachine != null)
        {
            Gizmos.color = _stateMachine.GetGizmosColor();
            Gizmos.DrawSphere(transform.position + Vector3.up * 2, 0.4f);
        }
    }
}
