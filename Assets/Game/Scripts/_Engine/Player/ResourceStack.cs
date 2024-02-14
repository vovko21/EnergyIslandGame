using System.Collections.Generic;
using UnityEngine;

public class ResourceStack : MonoBehaviour
{
    [Header("Object Settings")]
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _valuePerObject;

    [Header("Stack Settings")]
    [SerializeField] private Grid _grid;
    [SerializeField] private int _coll;
    [SerializeField] private int _row;

    private List<GameObject> _resources = new List<GameObject>();
    private int _height;
    private int _stuckValue;

    public int StuckValue => _stuckValue;

    public void Stack(int stuckValue)
    {
        ClearStack();

        _stuckValue = stuckValue;

        int objectCount = _stuckValue / _valuePerObject;

        _height = 1;
        bool finished = false;
        for (int y = 0; y < _height; y++)
        {
            for (int z = 0; z < _row; z++)
            {
                for (int x = 0; x < _coll; x++)
                {
                    if (objectCount <= 0)
                    {
                        finished = true;
                    }

                    var position = _grid.GetCellCenterWorld(new Vector3Int(x, y, z));
                    _resources.Add(Instantiate(_prefab, position, Quaternion.identity, this.transform));

                    objectCount--;
                }
            }

            if(!finished)
            {
                _height++;
            }
        }
    }

    private void ClearStack()
    {
        foreach (var item in _resources)
        {
            Destroy(item);
        }
        _stuckValue = 0;
        _resources.Clear();
    }
}
