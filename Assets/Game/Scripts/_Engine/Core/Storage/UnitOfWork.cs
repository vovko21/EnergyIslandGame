public class UnitOfWork
{
    private DataContext _dataContext;
    private ResourcesRepository _resourcesRepository;
    private ActiveBuildingsRepository _activeBuildingsRepository;
    private GameTasksRepository _activeTasks;

    public ResourcesRepository ResourcesRepository => _resourcesRepository;
    public ActiveBuildingsRepository ActiveBuildingsRepository => _activeBuildingsRepository;
    public GameTasksRepository ActiveTasks => _activeTasks;

    public UnitOfWork(DataContext dataContext)
    {
        _dataContext = dataContext;

        _resourcesRepository = new ResourcesRepository(dataContext);
        _activeBuildingsRepository = new ActiveBuildingsRepository(dataContext);
        _activeTasks = new GameTasksRepository(dataContext);
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
