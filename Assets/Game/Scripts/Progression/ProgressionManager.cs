public class ProgressionManager : SingletonMonobehaviour<ProgressionManager>
{
    private Wallet _wallet;

    public Wallet Wallet => _wallet;

    protected override void Awake()
    {
        base.Awake();

        _wallet = new Wallet();
    }

    private void Start()
    {
        LoadProgress();
    }

    private void LoadProgress()
    {
        _wallet.AddDollars(120);
    }
}
