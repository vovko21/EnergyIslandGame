using UnityEngine;
using UnityEngine.AI;

public abstract class Worker : MonoBehaviour
{
    [Header("Brain Setup")]
    [SerializeField] protected NavMeshAgent _agent;
    [SerializeField] protected CharacterAnimationController _animationController;

    protected StateMachine _stateMachine;

    public NavMeshAgent Agent => _agent;
    public CharacterAnimationController AnimationController => _animationController;

    protected virtual void Awake()
    {
        _stateMachine = new StateMachine();
    }

    protected virtual void Update()
    {
        _stateMachine.Tick();
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
