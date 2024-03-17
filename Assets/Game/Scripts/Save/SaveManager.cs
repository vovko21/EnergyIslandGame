using System.Threading.Tasks;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private int _saveCycle = 5;

    private void Start()
    {
        InvokeRepeating(nameof(Save), _saveCycle, _saveCycle);
    }

    // Save Methods
    private async Task Save()
    {
        SaveResources();

        SaveBuildings();

        SaveGameDateTime();

        SaveActiveTasks();

        await StorageService.Instance.SaveDataAsync();
    }

    private void SaveResources()
    {
        StorageService.Instance.AddOrUpdateResource(ResourceType.Dollars, ProgressionManager.Instance.Wallet.Dollars);
    }

    private void SaveBuildings()
    {
        if (BuildingManager.Instance.ActiveBuildings.Count == 0) return;

        foreach (var building in BuildingManager.Instance.ActiveBuildings)
        {
            StorageService.Instance.AddOrUpdateActiveBuilding(building.Id, building.Produced, building.CurrentStats.ProductionLevelIndex, building.CurrentStats.MaxSupplyLevelIndex, building.Status);
        }
    }

    private void SaveGameDateTime()
    {
        StorageService.Instance.SetInGameMinutesPassed(GameTimeManager.Instance.CurrentDateTime.TotalNumMinutes);
    }

    private void SaveActiveTasks()
    {
        StorageService.Instance.DeleteAllTasks();

        foreach (var task in TaskManager.Instance.ActiveTasks)
        {
            Debug.Log("at");
            StorageService.Instance.AddOrUpdateActiveTask(task);
        }
    }
}
