using UnityEngine;

public class TaskRules : MonoBehaviour, IEventListener<BuildingUpdatedEvent>, IEventListener<SellEvent>, IEventListener<MaintenanceEvent>
{
    [SerializeField] private TaskManager _taskManager;

    private void OnEnable()
    {
        this.StartListeningEvent<BuildingUpdatedEvent>();
        this.StartListeningEvent<SellEvent>();
        this.StartListeningEvent<MaintenanceEvent>();
    }

    private void OnDisable()
    {
        this.StopListeningEvent<BuildingUpdatedEvent>();
        this.StopListeningEvent<SellEvent>();
        this.StopListeningEvent<MaintenanceEvent>();
    }

    public void OnEvent(BuildingUpdatedEvent eventType)
    {
        //Builded
        if (eventType.upgraded == false)
        {
            _taskManager.AddProgress("Build Building", 1);
        }

        //Upgraded
        if (eventType.upgraded == true)
        {
            _taskManager.AddProgress("Upgrade Building", 1);
        }
    }

    public void OnEvent(SellEvent eventType)
    {
        _taskManager.AddProgress("Sell Energy Value", eventType.energySold);
        _taskManager.AddProgress("Sell Energy Price", eventType.dollarsGet);
    }

    public void OnEvent(MaintenanceEvent eventType)
    {
        _taskManager.AddProgress("Maintein five buildings", 1);
        _taskManager.AddProgress("Maintein ten buildings", 1);
    }
}
