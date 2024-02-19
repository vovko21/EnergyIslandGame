using UnityEngine;

public class WaitState : IState
{
    private Worker _worker;

    public WaitState(Worker worker)
    {
        _worker = worker;   
    }

    public void OnEnter()
    {
        _worker.AnimationController.SetIdle();
    }

    public void OnExit()
    {
        
    }

    public void Tick()
    {

    }

    public Color GetGizmosColor()
    {
        return Color.grey;
    }
}
