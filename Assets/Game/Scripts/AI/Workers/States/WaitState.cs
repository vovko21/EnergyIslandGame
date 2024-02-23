using UnityEngine;

public class WaitState : IState
{
    private Worker _worker;
    private Transform _waitPosition;

    private bool _pathSetted;
    private bool _isOnPosition;

    public WaitState(Worker worker)
    {
        _worker = worker;   
    }

    public WaitState(Worker worker, Transform waitPosition)
    {
        _worker = worker;
        _waitPosition = waitPosition;
    }

    public void OnEnter()
    {
        _worker.AnimationController.SetIdle();
    }

    public void OnExit()
    {
        _pathSetted = false;
        _isOnPosition = false;
    }

    public void Tick()
    {
        if(_waitPosition == null) return;

        if (_isOnPosition) return;

        if (!_pathSetted)
        {
            _worker.AnimationController.SetWalk();

            _worker.Agent.SetDestination(_waitPosition.position);

            _pathSetted = true;
        }

        if (!_worker.Agent.pathPending)
        {
            if (_worker.Agent.remainingDistance <= _worker.Agent.stoppingDistance)
            {
                if (!_worker.Agent.hasPath || _worker.Agent.velocity.sqrMagnitude == 0f)
                {
                    _worker.AnimationController.SetIdle();

                    _isOnPosition = true;
                }
            }
        }
    }

    public Color GetGizmosColor()
    {
        return Color.grey;
    }
}
