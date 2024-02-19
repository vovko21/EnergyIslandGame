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
    [Header("Production settings")]
    [SerializeField] protected int _productionPerGameHour;
    [SerializeField] protected int _maxSupply;
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

    [Header("Gather settings")]
    [SerializeField] protected int _minGatherAmount;
    [SerializeField] protected Transform _getherPoint;

    protected InGameDateTime _lastHourTime;

    public int Produced => _produced;
    public int MinGatherAmount => _minGatherAmount;
    public int MaxSupply => _maxSupply;
    public Transform GatherPoint => _getherPoint;
    public BuildingStatus Status => _status;
    public InGameDateTime LastHourTime => _lastHourTime;

    private void Awake()
    {
        _status = BuildingStatus.Producing;
    }

    private void OnEnable()
    {
        _lastHourTime = TimeManager.Instance.CurrentDateTime;
        TimeManager.Instance.OnDateTimeChanged += OnDateTimeChanged;
    }

    private void OnDisable()
    {
        TimeManager.Instance.OnDateTimeChanged -= OnDateTimeChanged;
    }

    private void OnDateTimeChanged(InGameDateTime dateTime)
    {
        if (_lastHourTime.Hour != dateTime.Hour)
        {
            _lastHourTime = dateTime;
            OnHourPassed();
        }
    }

    protected virtual void OnHourPassed()
    {
        if (_produced >= _maxSupply)
        {
            _status = BuildingStatus.MaxedOut;
            return;
        }

        Produce();
    }

    protected void Produce()
    {
        if (_produced >= _maxSupply) return;

        _produced += _productionPerGameHour;

        EventManager.TriggerEvent(new ProducedEvent() { building = this });
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
