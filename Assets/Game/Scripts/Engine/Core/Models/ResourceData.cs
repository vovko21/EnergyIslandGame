public enum ResourceType
{
    Coins = 0,
    Diamonds = 1
}

public class ResourceData : Model
{
    public int value;
    public ResourceType type;
}
