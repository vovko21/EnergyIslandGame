using System.Threading.Tasks;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [Header("Refferences")]
    [SerializeField] private DailyRewardsUI _dailyRewardsUI;
    [SerializeField] private TasksUI _tasksUI;

    [Header("Save settings")]
    [SerializeField] private int _saveCycle = 5;

    private void Start()
    {
        InvokeRepeating(nameof(SaveAll), _saveCycle, _saveCycle);
    }

    // Save Methods
    private async Task SaveAll()
    {
        SaveResources();

        SaveBuildings();

        SaveGameDateTime();

        SaveDailyBonuses();

        SaveTasks();

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

    private void SaveDailyBonuses()
    {
        StorageService.Instance.SetDaysClaimed(_dailyRewardsUI.DaysInRow);

        StorageService.Instance.SetLastClaimedBonusTime(_dailyRewardsUI.LastClaimTime);
    }

    private void SaveTasks()
    {
        //Save next time to reshuffle tasks
        StorageService.Instance.SetNextTasksTime(_tasksUI.NextDateTime);

        //Save active tasks
        StorageService.Instance.DeleteAllTasks();

        foreach (var task in TaskManager.Instance.ActiveTasks)
        {
            StorageService.Instance.AddOrUpdateActiveTask(task);
        }
    }
}