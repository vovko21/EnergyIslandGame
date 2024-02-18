using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceStack : MonoBehaviour
{
    [Header("Object Settings")]
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _valuePerObject;

    [Header("Stack Settings")]
    [SerializeField] private Grid _grid;
    [SerializeField] private int _colls;
    [SerializeField] private int _rows;
    [SerializeField] private int _maxObjectsCount;
    [SerializeField] private bool _spawnOnStart;

    private List<GameObject> _resources = new List<GameObject>();
    private int _height;
    #region ReadOnly
#if UNITY_EDITOR
    [ReadOnly]
        [SerializeField]
#endif
    #endregion
    private int _stuckValue;

    public int StuckValue => _stuckValue;
    public int MaxStuckValue => _maxObjectsCount * _valuePerObject;
    public bool IsMax => _resources.Count == _maxObjectsCount;

    public event Action OnStuckChange;

    private void Start()
    {
        if(_spawnOnStart) SpawnStack();
    }

    public void Initialize(int maxStackCount)
    {
        _maxObjectsCount = maxStackCount;
    }

    public int AddToStuck(int stuckValue)
    {
        if(IsMax)
        {
            return -1;
        }

        if(_stuckValue + stuckValue > MaxStuckValue)
        {
            var overflow = (_stuckValue + stuckValue) - MaxStuckValue;

            _stuckValue = MaxStuckValue;

            SpawnStack();

            return overflow;
        }

        _stuckValue += stuckValue;

        OnStuckChange?.Invoke();

        SpawnStack();

        return 0;
    }

    public int UpdateStack(int newStackValue)
    {
        if(newStackValue > MaxStuckValue || newStackValue <= 0)
        {
            return -1;
        }

        _stuckValue = newStackValue;

        OnStuckChange?.Invoke();

        SpawnStack();

        return 0;
    }

    private void SpawnStack()
    {
        DestroySpawnedStack();

        int objectCountToSpawn = _stuckValue / _valuePerObject;
        objectCountToSpawn = Mathf.Clamp(objectCountToSpawn, 0, _maxObjectsCount);

        if (objectCountToSpawn <= 0)
        {
            return;
        }

        _height = 1;
        bool finished = false;
        for (int y = 0; y < _height; y++)
        {
            for (int z = 0; z < _rows; z++)
            {
                for (int x = 0; x < _colls; x++)
                {
                    if (objectCountToSpawn <= 0)
                    {
                        finished = true;
                        break;
                    }

                    var position = _grid.GetCellCenterWorld(new Vector3Int(x, y, z));
                    _resources.Add(Instantiate(_prefab, position, _prefab.transform.rotation, this.transform));

                    objectCountToSpawn--;
                }
            }

            if (!finished)
            {
                _height++;
            }
        }
    }

    public void ClearStack()
    {
        DestroySpawnedStack();

        _stuckValue = 0;

        OnStuckChange?.Invoke();
    }

    private void DestroySpawnedStack()
    {
        foreach (var item in _resources)
        {
            Destroy(item);
        }

        _resources.Clear();
    }
}
