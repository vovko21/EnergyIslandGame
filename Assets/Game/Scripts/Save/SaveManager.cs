using System.Threading.Tasks;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private int _saveCycleInGameMinutes = 360;

    private InGameDateTime _saveDateTime;

    private void OnEnable()
    {
        _saveDateTime = GameTimeManager.Instance.CurrentDateTime;
        _saveDateTime.AdvanceMinutes(_saveCycleInGameMinutes);
        GameTimeManager.Instance.OnDateTimeChanged += OnDateTimeChanged;
    }

    private void OnDisable()
    {
        GameTimeManager.Instance.OnDateTimeChanged -= OnDateTimeChanged;
    }

    private async void OnDateTimeChanged(InGameDateTime dateTime)
    {
        if (_saveDateTime == dateTime)
        {
            await Save();
            _saveDateTime.AdvanceMinutes(_saveCycleInGameMinutes);
        }
    }

    private async Task Save()
    {
        SaveResources();

        SaveBuildings();

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
            StorageService.Instance.AddOrUpdateActiveBuilding(building.Id);
        }
    }
}
