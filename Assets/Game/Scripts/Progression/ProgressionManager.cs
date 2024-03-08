using UnityEngine;

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

    public string GetFormatedValue(int value)
    {
        string[] suffixes = { "", "K", "M", "B", "T" };

        int suffixIndex = 0;
        float num = value;
        while (Mathf.Abs(num) >= 1000 && suffixIndex < suffixes.Length - 1)
        {
            num /= 1000;
            suffixIndex++;
        }

        string formattedNumber = $"{num:0.##}{suffixes[suffixIndex]}";

        return formattedNumber;
    }
}