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

    public IReadOnlyList<ResourceData> Resources => _dataContext.Data.Resources;
    public bool Initialized => _dataContext.Data.Initialized;

    protected override void Awake()
    {
        base.Awake();

        _dataContext = new AesEncryptorDataContext();
        _unitOfWork = new UnitOfWork(_dataContext);
    }

    public ReadOnlyResource AddOrUpdateResource(int value, ResourceType type)
    {
        var data = _unitOfWork.ResourcesRepository.GetById(type.ToString());
        if(data == null)
        {
            data = new ResourceData() { value = value, type = type };
            _unitOfWork.ResourcesRepository.Add(data);     
        }
        else
        {
            data = new ResourceData() { id = type.ToString(), value = value, type = type };
            _unitOfWork.ResourcesRepository.Modify(data);
        }

        return new ReadOnlyResource(data.id, value, type);
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
        Debug.Log("Save");

        await _unitOfWork.SaveAsync();
    }

    private async void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            AddOrUpdateResource(ProgressionManager.Instance.Wallet.Dollars, ResourceType.Dollars);

            await SaveDataAsync();
        }
    }
}