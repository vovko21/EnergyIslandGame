using System.Collections;
using UnityEngine;

public class MaintenanceState : IState
{
    private ServiceWorker _worker;
    private Transform _destination;
    private float _timeToWait;

    private bool _pathSetted = false;
    private IEnumerator _coroutine;

    public void Initialize(ServiceWorker worker, Transform destination, float timeToWait)
    {
        _worker = worker;
        _destination = destination;
        _timeToWait = timeToWait;
    }

    public void OnEnter()
    {
    }

    public void OnExit()
    {
        _pathSetted = false;

        _coroutine = null;
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
                    if (_coroutine == null)
                    {
                        _coroutine = WaitCoroutine();
                        _worker.StartCoroutine(_coroutine);
                    }
                }
            }
        }
    }

    private IEnumerator WaitCoroutine()
    {
        Debug.Log("wait");

        _worker.AnimationController.SetIdle();

        yield return new WaitForSeconds(_timeToWait);

        _worker.NextState();
    }

    public Color GetGizmosColor()
    {
        return Color.blue;
    }
}
