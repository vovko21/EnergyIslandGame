public enum ResourceType
{
    Dollars = 0,
    Diamonds = 1
}

[System.Serializable]
public class ResourceData : Model
{
    public int value;
    public ResourceType type;
}
