using UnityEngine;

public class GoodDealEvent : MonoBehaviour, IEventListener<GameEvent>
{
    [Header("Event settings")]
    [SerializeField] private GameObject _area;
    [SerializeField] private FollowBySpline _boat;
    [SerializeField] private float _activeTime;

    [Header("Event stats")]
    [SerializeField] private EnergyBank _energyBank;
    [SerializeField] private int _energyDeffaultCount;

    private bool _isActive;
    private Timer _timer;

    public int EnergyDeffaultCount => _energyDeffaultCount;
    public EnergyBank EnergyBank => _energyBank;

    private void Awake()
    {
        _timer = new Timer(this);
    }

    private void OnEnable()
    {
        this.StartListeningEvent();

        _timer.TimeIsOver += TimeIsOver;
        _energyBank.OnAdded += OnAddedInBank;
    }

    private void OnDisable()
    {
        this.StopListeningEvent();

        _timer.TimeIsOver -= TimeIsOver;
        _energyBank.OnAdded -= OnAddedInBank;
    }

    private void Start()
    {
        _area.SetActive(false);
    }

    public void OnEvent(GameEvent eventType)
    {
        if (eventType.type != GameEventType.GoodDeal) return;
        if (_isActive) return;

        _isActive = true;

        _boat.GoToShoer(() =>
        {
            CameraController.Instance.FollowPlayer();
            _area.SetActive(true);
        });

        _timer.Set(_activeTime);
        _timer.StartCountingTime();

        CameraController.Instance.FollowEvent();
    }

    private void TimeIsOver()
    {
        _area.SetActive(false);

        _boat.GoFromShoer(() =>
        {
            //CameraController.Instance.FollowPlayer(); 
            _isActive = false;
        });
    }

    private void OnAddedInBank()
    {
        if (_energyBank.Energy < _energyDeffaultCount) return;

        _timer.StopCountingTime();

        _area.SetActive(false);

        ProgressionManager.Instance.Wallet.AddDollars((int)(StockMarket.Instance.EnergyPrice * _energyBank.Energy) * 2);

        _energyBank.ClearEnergy();

        _boat.GoFromShoer(() =>
        {
            _isActive = false;
        });
    }
}
