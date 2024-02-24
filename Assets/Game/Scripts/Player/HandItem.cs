using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HandItemType
{
    None = 0,
    Wrench = 1
}

[System.Serializable]
public class HandItem : MonoBehaviour
{
    [SerializeField] private GameObject _item;
    [SerializeField] private HandItemType _type;

    public HandItemType Type => _type;

    public void Activate()
    {
        _item.SetActive(true);
    }

    public void Deactivate() 
    {
        _item.SetActive(false);
    }
}
