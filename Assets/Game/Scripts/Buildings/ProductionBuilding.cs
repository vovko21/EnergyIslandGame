using System;
using UnityEngine;

public struct BuildingUpdatedEvent
{
    public ProductionBuilding productionBuilding;
    public bool upgraded;
}

public enum BuildingStatus
{
    None = 0,
    Producing = 1,
    NotProducing = 2,
    Maintenance = 3,
    MaxedOut = 4,
    Broken = 5
}

public class BuildingStats
{
    private BuildingStatsSO _stats;

    private int _productionLevelIndex = 0;
    private int _maxSupplyLevelIndex = 0;

    private float _coefficient = 1;

    public int ProductionPerGameHour => (int)(CurrentProductionLevel.Value * _coefficient);
    public int MaxSupply => CurrentSupplyLevel.Value;
    public float MaintenanceTime => _stats.MaintainingTime;
    public int Consumption => _stats.Consumption;
    public BuildingStat CurrentProductionLevel => _stats.ProductionPerGameHour[_productionLevelIndex];
    public BuildingStat CurrentSupplyLevel => _stats.MaxSupply[_maxSupplyLevelIndex];
    public BuildingStat NextProductionLevel => !IsProductionLevelMax ? _stats.ProductionPerGameHour[_productionLevelIndex + 1] : null;
    public BuildingStat NextSupplyLevel => !IsSupplyLevelMax ? _stats.MaxSupply[_maxSupplyLevelIndex + 1] : null;

    public bool IsProductionLevelMax => _productionLevelIndex == _stats.ProductionPerGameHour.Count - 1;
    public bool IsSupplyLevelMax => _maxSupplyLevelIndex == _stats.MaxSupply.Count - 1;

    public BuildingStats(BuildingStatsSO stats)
    {
        _stats = stats;
    }

    public void ApplyCoefficient(float coefficient)
    {
        _coefficient = coefficient;
    }

    public bool UpgradeProduction()
    {
        if (IsProductionLevelMax) return false;

        _productionLevelIndex++;

        return true;
    }

    public bool UpgradeSupply()
    {
        if (IsSupplyLevelMax) return false;

        _maxSupplyLevelIndex++;

        return true;
    }
}

public class ProductionBuilding : MonoBehaviour
{
    [Header("Stats settings")]
    [SerializeField] private string _id;
    [SerializeField] private BuildingStatsSO _stats;

    [Header("Gather settings")]
    [SerializeField] protected int _minGatherAmount;
    [SerializeField] protected Transform _getherPoint;

    [Header("Production Info")]
    #region ReadOnly
#if UNITY_EDITOR
    [ReadOnly]
    [SerializeField]
#endif
    #endregion  
    protected int _produced;
    #region ReadOnly
#if UNITY_EDITOR
    [ReadOnly]
    [SerializeField]
#endif
    #endregion  
    private BuildingStatus _status;
    #region ReadOnly
#if UNITY_EDITOR
    [ReadOnly]
    [SerializeField]
#endif
    #endregion  
    protected int _currentLevelIndex;

    protected InGameDateTime _nextHourTime;
    protected BuildingStats _currentStats;

    public string Id => _id;
    public BuildingStats CurrentStats => _currentStats;
    public int Produced => _produced;
    public int MinGatherAmount => _minGatherAmount;
    public Transform GatherPoint => _getherPoint;
    public BuildingStatus Status
    {
        get => _status;
        protected set
        {
            _status = value;
            OnStatusChanged?.Invoke(_status);
        }
    }

    public event Action<BuildingStatus> OnStatusChanged;

    private void Awake()
    {
        _currentStats = new BuildingStats(_stats);
    }

    protected virtual void OnEnable()
    {
        if(GameTimeManager.Instance == null)
        {
            return;
        }

        _nextHourTime = GameTimeManager.Instance.CurrentDateTime;
        _nextHourTime.AdvanceMinutes(60);
        GameTimeManager.Instance.OnDateTimeChanged += OnDateTimeChanged;

        Status = BuildingStatus.Producing;
    }

    protected virtual void OnDisable()
    {
        GameTimeManager.Instance.OnDateTimeChanged -= OnDateTimeChanged;
    }

    private void OnDateTimeChanged(InGameDateTime dateTime)
    {
        if (_nextHourTime == dateTime)
        {
            if (Status == BuildingStatus.None) return;
            if (Status == BuildingStatus.Broken) return;
            OnHourPassed();
        }
    }

    protected virtual void OnHourPassed()
    {
        if (_produced >= CurrentStats.MaxSupply)
        {
            Status = BuildingStatus.MaxedOut;
            return;
        }

        if (Status == BuildingStatus.Producing)
        {
            Produce();
        }
    }

    protected virtual void Produce()
    {
        if (_produced >= CurrentStats.MaxSupply) return;

        _produced += CurrentStats.ProductionPerGameHour;
    }

    public virtual void Gather(int overflow)
    {
        if (overflow >= 0)
        {
            if (overflow == 0)
            {
                _produced = 0;
            }
            else
            {
                _produced = overflow;
            }
        }
    }

    public void Brake()
    {
        Status = BuildingStatus.Broken;
    }

    public void Fix()
    {
        _nextHourTime = GameTimeManager.Instance.CurrentDateTime;
        _nextHourTime.AdvanceMinutes(60);
        Status = BuildingStatus.Producing;
    }
}