using System.Collections.Generic;
using UnityEngine;

public class HintsManager : MonoBehaviour, IEventListener<BuildingUpdatedEvent>
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

    private List<GameObject> _showedHints = new();
    private bool _isFirstGameHint = false;

    private void OnEnable()
    {
        this.StartListeningEvent();
    }

    private void OnDisable()
    {
        this.StopListeningEvent();
    }

    public void OnEvent(BuildingUpdatedEvent eventType)
    {
        if (eventType.upgraded == true) return;

        if(_isFirstGameHint)
        {
            ClearShowedHints();
            _isFirstGameHint = false;
        }
    }

    public void StartFirstGameHints()
    {
        CreateHint(_firstBuilding.position);
        _isFirstGameHint = true;

        _ui.ShowHint("Build first windmill");
    }

    public void StartFixHints()
    {
        if (_player.Hands.CurrentItem == null)
        {
            ShowTrashHint();
        }
    }

    private void ShowTrashHint()
    {
        for (int i = 0; i < _trashAreas.Length; i++)
        {
            CreateHint(_trashAreas[i].position);
        }
    }

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