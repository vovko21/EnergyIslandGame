using System.Collections.Generic;
using UnityEngine;

public struct ProducedEvent
{
    public ProductionBuilding building;
}

public enum BuildingStatus
{
    Producing = 0,
    NotProducing = 1,
    Broken = 2,
    MaxedOut = 3
}

public class ProductionBuilding : MonoBehaviour
{
    [Header("Stats settings")]
    [SerializeField] protected List<BuildingSO> _levels;

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
    protected BuildingStatus _status;
    #region ReadOnly
#if UNITY_EDITOR
    [ReadOnly]
    [SerializeField]
#endif
    #endregion  
    protected int _currentLevelIndex;
    protected InGameDateTime _lastHourTime;

    public int Produced => _produced;
    public int MinGatherAmount => _minGatherAmount;
    public Transform GatherPoint => _getherPoint;
    public BuildingStatus Status => _status;
    public BuildingSO CurrentStats => _levels[_currentLevelIndex];
    public bool IsMaxLevel => _levels.Count - 1 == _currentLevelIndex;
    public int CurrentLevelIndex => _currentLevelIndex;
    public int LevelsCount => _levels.Count;

    private void Awake()
    {
        _status = BuildingStatus.Producing;
    }

    private void OnEnable()
    {
        if (TimeManager.Instance == null)
        {
            return;
        }

        _lastHourTime = TimeManager.Instance.CurrentDateTime;
        _lastHourTime.AdvanceMinutes(60);
        TimeManager.Instance.OnDateTimeChanged += OnDateTimeChanged;
    }

    private void OnDisable()
    {
        if (TimeManager.Instance == null)
        {
            return;
        }

        TimeManager.Instance.OnDateTimeChanged -= OnDateTimeChanged;
    }

    private void OnDateTimeChanged(InGameDateTime dateTime)
    {
        if ((_lastHourTime.Hour + _lastHourTime.Minute / 60f) == (dateTime.Hour + dateTime.Minute / 60f))
        {
            _lastHourTime.AdvanceMinutes(60);
            OnHourPassed();
        }
    }

    protected virtual void OnHourPassed()
    {
        Produce();

        if (_produced >= CurrentStats.MaxSupply)
        {
            _status = BuildingStatus.MaxedOut;
            return;
        }
    }

    protected void Produce()
    {
        if (_produced >= CurrentStats.MaxSupply) return;

        _produced += CurrentStats.ProductionPerGameHour;

        EventManager.TriggerEvent(new ProducedEvent() { building = this });
    }

    public virtual void Upgrade()
    {
        if (IsMaxLevel) return;

        _currentLevelIndex++;
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
}
