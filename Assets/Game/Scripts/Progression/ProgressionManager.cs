public class ProgressionManager : SingletonMonobehaviour<ProgressionManager>
{
    private Wallet _wallet;

    public Wallet Wallet => _wallet;

    private void Start()
    {
        _wallet = new Wallet();

        LoadProgress();
    }

    private void LoadProgress()
    {
        _wallet.AddDollars(999999);
    }
}
