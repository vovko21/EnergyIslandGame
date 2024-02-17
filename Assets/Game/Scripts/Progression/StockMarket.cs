using UnityEngine;

public class StockMarket : MonoBehaviour
{
    [Header("EnergyPrice")]
    #region ReadOnly
#if UNITY_EDITOR
    [ReadOnly]
    [SerializeField]
#endif
    #endregion
    private float _energyPrice;
    [SerializeField] private float _energyMaxPrice = 0.5f;
    [SerializeField] private float _energyMinPrice = 0.05f;

    [Header("Trends")]
    [SerializeField] private float positiveTrendChance = 10;
    [SerializeField] private float negativeTrendChance = -10;

    #region ReadOnly
#if UNITY_EDITOR
    [ReadOnly]
    [SerializeField]
#endif
    #endregion
    private int _dayTrend;
    private InGameDateTime _lastDay;

    public float EnergyPrice => _energyPrice;
    public float AveragePrice => (_energyMaxPrice + _energyMinPrice) / 2;

    private void Start()
    {
        Initialize();
        TimeManager.Instance.OnDateTimeChanged += OnDateTimeChanged;
    }

    private void OnDisable()
    {
        TimeManager.Instance.OnDateTimeChanged -= OnDateTimeChanged;
    }

    private void Initialize()
    {
        _lastDay = TimeManager.Instance.CurrentDateTime;
        _energyPrice = AveragePrice;
    }

    private void OnDateTimeChanged(InGameDateTime dateTime)
    {
        if (_lastDay.Hour != dateTime.Hour)
        {
            _dayTrend = RandDayTrend();
            _lastDay = dateTime;
        }

        CalculateEnergyPrice();
    }

    private void CalculateEnergyPrice()
    {
        if (_dayTrend == 0)
        {
            RandPrice();
        }
        if (_dayTrend >= 1)
        {
            RandPrice(positiveTrendChance);
        }
        if (_dayTrend <= -1)
        {
            RandPrice(negativeTrendChance);
        }
    }

    private void RandPrice(float addChance = 0)
    {
        var trend = 0f;
        if (addChance < 0)
        {
            trend = Random.Range(-50 + addChance, 50);
        }
        if (addChance >= 0)
        {
            trend = Random.Range(-50, 50 + addChance);
        }

        var offset = (AveragePrice / 100) * 2;

        if (IsLocalTrendPositive(trend))
        {
            SetEnergyPrive(_energyPrice + offset);
        }
        else
        {
            SetEnergyPrive(_energyPrice - offset);
        }
    }

    private int RandDayTrend()
    {
        var rand = Random.Range(0, 150);

        if (rand < 50)
        {
            return -1;
        }
        if (rand > 50 && rand < 100)
        {
            return 0;
        }
        if (rand > 100)
        {
            return 1;
        }

        return 0;
    }

    private bool IsLocalTrendPositive(float trend)
    {
        if (trend < 0)
        {
            return false;
        }

        return true;
    }

    private void SetEnergyPrive(float newPrice)
    {
        _energyPrice = Mathf.Clamp(newPrice, _energyMinPrice, _energyMaxPrice);
    }
}
