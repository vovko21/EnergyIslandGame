public enum ResourceType
{
    Dollars = 0,
    Energy = 1,
    Diamonds = 2
}

[System.Serializable]
public class ResourceData : Model
{
    public int value;
    public ResourceType type;
}
