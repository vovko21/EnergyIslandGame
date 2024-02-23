using System;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingStatus
{
    Producing = 0,
    NotProducing = 1,
    Maintenance = 2,
    MaxedOut = 3,
    Broken = 4
}

public class ProductionStats
{
    private BuildingStatsSO _stats;
    private float _coefficient = 1;

    public int ProductionPerGameHour => (int)(_stats.ProductionPerGameHour * _coefficient);
    public int MaxSupply => _stats.MaxSupply;
    public float MaintenanceTime => _stats.MaintainingTime;

    public ProductionStats(BuildingStatsSO stats)
    {
        _stats = stats;
    }

    public void ApplyCoefficient(float coefficient)
    {
        _coefficient = coefficient;
    }
}

public class ProductionBuilding : MonoBehaviour
{
    [Header("Stats settings")]
    [SerializeField] protected List<BuildingStatsSO> _levels;

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
    protected ProductionStats _currentStats;

    public ProductionStats CurrentStats => _currentStats;
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
    public bool IsMaxLevel => _levels.Count - 1 == _currentLevelIndex;
    public int CurrentLevelIndex => _currentLevelIndex;
    public int LevelsCount => _levels.Count;

    public event Action<BuildingStatus> OnStatusChanged;

    private void Awake()
    {
        _currentStats = new ProductionStats(_levels[_currentLevelIndex]);
    }

    private void OnEnable()
    {
        if (GameTimeManager.Instance == null)
        {
            return;
        }

        _nextHourTime = GameTimeManager.Instance.CurrentDateTime;
        _nextHourTime.AdvanceMinutes(60);
        GameTimeManager.Instance.OnDateTimeChanged += OnDateTimeChanged;
    }

    private void OnDisable()
    {
        if (GameTimeManager.Instance == null)
        {
            return;
        }

        GameTimeManager.Instance.OnDateTimeChanged -= OnDateTimeChanged;
    }

    private void Start()
    {
        Status = BuildingStatus.Producing;
    }

    private void OnDateTimeChanged(InGameDateTime dateTime)
    {
        if (_nextHourTime == dateTime)
        {
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

    protected void Produce()
    {
        if (_produced >= CurrentStats.MaxSupply) return;

        _produced += CurrentStats.ProductionPerGameHour;

        Status = BuildingStatus.Maintenance;
    }

    public virtual void Upgrade()
    {
        if (IsMaxLevel) return;

        _currentLevelIndex++;

        _currentStats = new ProductionStats(_levels[_currentLevelIndex]);
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

    public void Maintenanced()
    {
        if(Status != BuildingStatus.Maintenance)
        {
            return;
        }

        _nextHourTime = GameTimeManager.Instance.CurrentDateTime;
        _nextHourTime.AdvanceMinutes(60);
        Status = BuildingStatus.Producing;
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
