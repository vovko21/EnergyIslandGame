using System.Collections.Generic;
using System.Threading.Tasks;
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

public class StorageService : MonoBehaviour
{
    public static StorageService Instance { get; private set; }

    private DataContext _dataContext;
    private UnitOfWork _unitOfWork;

    public IReadOnlyList<ResourceData> Resources => _dataContext.Data.Resources;

    private void Awake()
    {
        _dataContext = new AesEncryptorDataContext();
        _unitOfWork = new UnitOfWork(_dataContext);

        if (Instance == null)
        {
            Instance = this;
        }
    }

    public ReadOnlyResource AddResource(int value, ResourceType type)
    {
        var data = new ResourceData() { value = value, type = type };
        _unitOfWork.ResourcesRepository.Add(data);

        return new ReadOnlyResource(data.id, value, type);
    }

    public void ModifyResource(ReadOnlyResource resource)
    {
        var data = new ResourceData() { id = resource.id, value = resource.value, type = resource.type };
        _unitOfWork.ResourcesRepository.Modify(data);
    }

    public bool DeleteResource(string id)
    {
        return _unitOfWork.ResourcesRepository.Delete(id);
    }

    public async Task LoadDataAsync()
    {
        await _unitOfWork.LoadAsync();
    }

    public async Task SaveAsync()
    {
        await _unitOfWork.SaveAsync();
    }
}