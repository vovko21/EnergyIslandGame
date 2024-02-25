using UnityEngine;

public enum HandItemType
{
    None = 0,
    Wrench = 1,
    Saw = 2
}

[System.Serializable]
public class HandItem : MonoBehaviour
{
    [SerializeField] private HandItemType _type;

    public HandItemType Type => _type;

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
