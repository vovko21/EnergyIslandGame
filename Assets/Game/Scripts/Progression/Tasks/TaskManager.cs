using UnityEngine;

public class TaskManager : MonoBehaviour, IInteractable
{
    private GameTask _currentTask;

    private void Start()
    {
        
    }

    private GameTask GenerateTask()
    {
        var targetProgress = Random.Range(1, 2);
        return new GameTask("1", "2", targetProgress);
    }

    public void Interact(Player player)
    {
        _currentTask = GenerateTask();
    }
}
