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

    public async System.Threading.Tasks.Task LoadAsync()
    {
        await _dataContext.LoadAsync();
    }

    public async System.Threading.Tasks.Task SaveAsync()
    {
        if(!_dataContext.Data.Initialized)
        {
            _dataContext.Data.Initialized = true;
        }

        await _dataContext.SaveAsync();
    }
}
