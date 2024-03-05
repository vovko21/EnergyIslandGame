public class ProgressionManager : SingletonMonobehaviour<ProgressionManager>
{
    private Wallet _wallet;

    public Wallet Wallet => _wallet;

    protected override void Awake()
    {
        base.Awake();

        _wallet = new Wallet();
    }

    public void InitializeData()
    {
        if (!StorageService.Instance.Initialized)
        {
            return;
        }

        var dollars = StorageService.Instance.GetResource(ResourceType.Dollars);

        _wallet.AddDollars(dollars.value);
    }
}