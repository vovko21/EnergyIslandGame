using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintsManager : MonoBehaviour, IEventListener<BuildingUpdatedEvent>, IEventListener<GameEvent>
{
    [Header("Main settings")]
    [SerializeField] private Player _player;
    [SerializeField] private UserInterface _ui;

    [Header("Hint settings")]
    [SerializeField] private GameObject _hint;
    [SerializeField] private float _height;

    [Header("Interactive Objects")]
    [SerializeField] private Transform[] _trashAreas;
    [SerializeField] private Transform _firstBuilding;
    [SerializeField] private Transform _werehouse;
    [SerializeField] private Transform _stackPosition;
    [SerializeField] private Transform _sellPosition;

    private List<GameObject> _showedHints = new();
    private bool _isFirstGameHint = false;
    private bool _firstLoad = false;

    private void OnEnable()
    {
        this.StartListeningEvent<BuildingUpdatedEvent>();
        this.StartListeningEvent<GameEvent>();
    }

    private void OnDisable()
    {
        this.StopListeningEvent<BuildingUpdatedEvent>();
        this.StopListeningEvent<GameEvent>();
    }

    public void Initialize(bool firstLoad)
    {
        _firstLoad = firstLoad;

        if (_firstLoad)
        {
            _player.Hands.OnStackChanged += Before_SellHint_OnStackChanged;
        }
    }

    private void Before_SellHint_OnStackChanged(CarrySystem system)
    {
        if (system.StuckValue > 0)
        {
            StartSellHint();
        }
    }

    //Building build Event
    public void OnEvent(BuildingUpdatedEvent eventType)
    {
        if (eventType.upgraded == true) return;

        if (_isFirstGameHint)
        {
            ClearShowedHints();
            _isFirstGameHint = false;
        }
    }

    //Broke Event
    public void OnEvent(GameEvent eventType)
    {
        if (eventType.type != GameEventType.BuildingBroken) return;

        StartFixHints();
    }

    public void StartFirstGameHints()
    {
        CreateHint(_firstBuilding.position);
        _isFirstGameHint = true;

        _ui.ShowHint("Build first windmill");
    }

    #region Fix hints
    public void StartFixHints()
    {
        CreateHint(_werehouse.position);

        _ui.HideHint();
        _ui.ShowHint("Take wrench in HOUSE to fix building <br> <color=grey>(free your hands first)</color>");

        _player.Hands.Inventory.OnItemTaken += FixHint_OnItemTaken;
    }

    private void FixHint_OnItemTaken(HandItem obj)
    {
        _player.Hands.Inventory.OnItemTaken -= FixHint_OnItemTaken;

        ClearShowedHints();

        _ui.HideHint();
        _ui.ShowHint("Interact with building to fix");

        StartCoroutine(TrashHintsDelayed());
    }

    private IEnumerator TrashHintsDelayed()
    {
        yield return new WaitForSeconds(10);

        ShowTrashHint();

        _player.Hands.Inventory.OnItemHiden += FixHint_OnItemHiden;
    }

    private void FixHint_OnItemHiden()
    {
        _player.Hands.Inventory.OnItemHiden -= FixHint_OnItemHiden;

        ClearShowedHints();
    }

    private void ShowTrashHint()
    {
        for (int i = 0; i < _trashAreas.Length; i++)
        {
            CreateHint(_trashAreas[i].position);
        }
    }
    #endregion

    #region Sell hints
    private void StartSellHint()
    {
        _ui.HideHint();
        _ui.ShowHint("Go to the right shoer to stack postion");

        CreateHint(_stackPosition.position);

        _player.Hands.OnStackChanged += SellHint_OnStackChanged;
    }

    private void SellHint_OnStackChanged(CarrySystem system)
    {
        if (system.StuckValue > 0) return;

        _player.Hands.OnStackChanged -= SellHint_OnStackChanged;
        ClearShowedHints();

        _ui.HideHint();
        _ui.ShowHint("Go to the sell postion");

        StartCoroutine(SellHintDelayed());
    }

    private IEnumerator SellHintDelayed()
    {
        CreateHint(_sellPosition.position);

        yield return new WaitForSeconds(10);

        _ui.HideHint();
        ClearShowedHints();

        _player.Hands.OnStackChanged -= Before_SellHint_OnStackChanged;
    }
    #endregion

    private void CreateHint(Vector3 position)
    {
        var hint = Instantiate(_hint, transform);
        hint.transform.position = position + (Vector3.up * _height);
        _showedHints.Add(hint);
    }

    private void ClearShowedHints()
    {
        foreach (var hint in _showedHints)
        {
            Destroy(hint);
        }
        _showedHints.Clear();
    }
}