using UnityEngine;

public class TakeProducedState : IState
{
    private Worker _worker;
    private Transform _destination;

    private bool _pathSetted = false;

    public void Initialize(Worker worker, Transform destination)
    {
        _worker = worker;
        _destination = destination;
    }

    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }

    public void Tick()
    {
        if (_worker == null) return;

        if (!_pathSetted)
        {
            _worker.AnimationController.SetWalk();

            _worker.Agent.SetDestination(_destination.position);

            _pathSetted = true;
        }

        if (!_worker.Agent.pathPending)
        {
            if (_worker.Agent.remainingDistance <= _worker.Agent.stoppingDistance)
            {
                if (!_worker.Agent.hasPath || _worker.Agent.velocity.sqrMagnitude == 0f)
                {
                    _worker.AnimationController.SetIdle();

                    _pathSetted = false;

                    _worker.NextState();
                }
            }
        }
    }

    public Color GetGizmosColor()
    {
        return Color.blue;
    }
}
