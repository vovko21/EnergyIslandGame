using UnityEngine;

public class TaskRules : MonoBehaviour, IEventListener<BuildingUpdatedEvent>
{
    [SerializeField] private TaskManager _taskManager;

    private void OnEnable()
    {
        this.StartListeningEvent<BuildingUpdatedEvent>();
    }

    private void OnDisable()
    {
        this.StopListeningEvent<BuildingUpdatedEvent>();
    }

    public void OnEvent(BuildingUpdatedEvent eventType)
    {
        if (eventType.upgraded == false)
        {
            _taskManager.AddProgress("Builded", 1);
        }

        if (eventType.upgraded == true)
        {
            _taskManager.AddProgress("Upgraded", 1);
        }
    }
}
