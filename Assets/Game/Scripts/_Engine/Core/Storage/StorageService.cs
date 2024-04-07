using System;
using System.Collections.Generic;
using UnityEngine;

public struct ReadOnlyResource
{
    public string id;
    public int value;
    public ResourceType type;

    public ReadOnlyResource(string id, int value, ResourceType type)
    {
        this.id = id;
        this.value = value;
        this.type = type;
    }
}

public class StorageService : SingletonMonobehaviour<StorageService>
{
    private DataContext _dataContext;
    private UnitOfWork _unitOfWork;

    public bool Initialized => _dataContext.Data.Initialized;
    public int InGameMinutesPassed => _dataContext.Data.InGameMinutesPassed;
    public int DaysBonusClaimedInRow => _dataContext.Data.DaysBonusClaimedInRow;
    public DateTime? LastClaimBonusTime => StringToDateTime(_dataContext.Data.LastClaimBonusTime);
    public DateTime? NextTasksTime => StringToDateTime(_dataContext.Data.NextTasksTime);

    protected override void Awake()
    {
        base.Awake();

        _dataContext = new JsonDataContext();
        _unitOfWork = new UnitOfWork(_dataContext);
    }

    public ReadOnlyResource AddOrUpdateResource(ResourceType type, int value)
    {
        var data = _unitOfWork.ResourcesRepository.GetById(type.ToString());
        if (data == null)
        {
            data = new ResourceData() { value = value, type = type };
            _unitOfWork.ResourcesRepository.Add(data);
        }
        else
        {
            data = new ResourceData() { id = type.ToString(), value = value, type = type };
            _unitOfWork.ResourcesRepository.Modify(data);
        }

        return new ReadOnlyResource(data.id, data.value, data.type);
    }

    public void AddOrUpdateActiveBuilding(string id, int produced, int productionLevelIndex, int maxSupplyLevelIndex, BuildingStatus status)
    {
        var data = _unitOfWork.ActiveBuildingsRepository.GetById(id);

        if (data == null)
        {
            data = new BuildingData() { id = id, produced = produced, productionLevelIndex = productionLevelIndex, maxSupplyLevelIndex = maxSupplyLevelIndex, status = status };
            _unitOfWork.ActiveBuildingsRepository.Add(data);
        }
        else
        {
            data = new BuildingData() { id = id, produced = produced, productionLevelIndex = productionLevelIndex, maxSupplyLevelIndex = maxSupplyLevelIndex, status = status };
            _unitOfWork.ActiveBuildingsRepository.Modify(data);
        }
    }

    public void AddOrUpdateActiveTask(GameTask gameTask)
    {
        var data = _unitOfWork.ActiveTasks.GetById(gameTask.Id);

        if (data == null)
        {
            data = new GameTaskData()
            {
                id = gameTask.Id,
                isCompleted = gameTask.IsCompleted,
                name = gameTask.Name,
                description = gameTask.Description,
                progressCurrent = gameTask.ProgressCurrent,
                progressTarget = gameTask.ProgressTarget,
                resourceType = gameTask.ResourceType,
                rewardValue = gameTask.RewardValue,
                isTook = gameTask.isTook
            };
            _unitOfWork.ActiveTasks.Add(data);
        }
        else
        {
            data = new GameTaskData()
            {
                id = gameTask.Id,
                isCompleted = gameTask.IsCompleted,
                name = gameTask.Name,
                description = gameTask.Description,
                progressCurrent = gameTask.ProgressCurrent,
                progressTarget = gameTask.ProgressTarget,
                resourceType = gameTask.ResourceType,
                rewardValue = gameTask.RewardValue,
                isTook = gameTask.isTook
            };
            _unitOfWork.ActiveTasks.Modify(data);
        }
    }

    public void SetInGameMinutesPassed(int inGameMinutesPassed)
    {
        _dataContext.Data.InGameMinutesPassed = inGameMinutesPassed;
    }

    public void SetDaysClaimed(int daysBonusClaimedInRow)
    {
        _dataContext.Data.DaysBonusClaimedInRow = daysBonusClaimedInRow;
    }

    public void SetLastClaimedBonusTime(DateTime? dateTime)
    {
        if(dateTime.HasValue)
        {
            _dataContext.Data.LastClaimBonusTime = dateTime.Value.ToString();
        }
    }

    public void SetNextTasksTime(DateTime? dateTime)
    {
        if (dateTime.HasValue)
        {
            _dataContext.Data.NextTasksTime = dateTime.Value.ToString();
        }
    }

    public ReadOnlyResource GetResource(ResourceType type)
    {
        var resource = _unitOfWork.ResourcesRepository.GetById(type.ToString());
        if (resource == null)
        {
            return new ReadOnlyResource("", 0, 0);
        }
        return new ReadOnlyResource(resource.id, resource.value, resource.type);
    }

    public List<BuildingData> GetActiveBuildings()
    {
        return _unitOfWork.ActiveBuildingsRepository.GetAll();
    }

    public List<GameTaskData> GetActiveTasks()
    {
        return _unitOfWork.ActiveTasks.GetAll();
    }

    public bool DeleteResource(ResourceType type)
    {
        return _unitOfWork.ResourcesRepository.Delete(type.ToString());
    }

    public void DeleteAllTasks()
    {
        var allTaks = _unitOfWork.ActiveTasks.GetAll();

        if (allTaks == null || allTaks.Count == 0) return;

        foreach (var task in allTaks)
        {
            _unitOfWork.ActiveTasks.Delete(task.id);
        }
    }

    private DateTime? StringToDateTime(string dateTime)
    {
        DateTime parsed = new DateTime();

        if (!DateTime.TryParse(dateTime, out parsed))
        {
            return null;
        }

        return parsed;
    }

    public async System.Threading.Tasks.Task LoadDataAsync()
    {
        await _unitOfWork.LoadAsync();
    }

    public async System.Threading.Tasks.Task SaveDataAsync()
    {
        await _unitOfWork.SaveAsync();

        Debug.Log("Saved");
    }
}