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

    protected override void Awake()
    {
        base.Awake();

        _dataContext = new AesEncryptorDataContext();
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

    public void AddOrUpdateActiveBuilding(string id)
    {
        var data = _unitOfWork.ActiveBuildingsRepository.GetById(id);

        if (data == null)
        {
            data = new BuildingData() { id = id };
            _unitOfWork.ActiveBuildingsRepository.Add(data);
        }
        else
        {
            data = new BuildingData() { id = id };
            _unitOfWork.ActiveBuildingsRepository.Modify(data);
        }
    }

    public ReadOnlyResource GetResource(ResourceType type)
    {
        var resource = _unitOfWork.ResourcesRepository.GetById(type.ToString());
        return new ReadOnlyResource(resource.id, resource.value, resource.type);
    }

    public List<BuildingData> GetActiveBuildings()
    {
        return _unitOfWork.ActiveBuildingsRepository.GetAll();
    }

    public bool DeleteResource(ResourceType type)
    {
        return _unitOfWork.ResourcesRepository.Delete(type.ToString());
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