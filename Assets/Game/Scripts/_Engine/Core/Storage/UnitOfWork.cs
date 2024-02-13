using System.Threading.Tasks;

public class UnitOfWork
{
    private DataContext _dataContext;
    private ResourcesRepository _resourcesRepository;

    public ResourcesRepository ResourcesRepository => _resourcesRepository;

    public UnitOfWork(DataContext dataContext)
    {
        _dataContext = dataContext;
        _resourcesRepository = new ResourcesRepository(dataContext);
    }

    public async Task LoadAsync()
    {
        await _dataContext.LoadAsync();
    }

    public async Task SaveAsync()
    {
        await _dataContext.SaveAsync();
    }
}
