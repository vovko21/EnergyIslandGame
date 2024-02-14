public enum ResourceType
{
    Coins = 0,
    Energy = 1,
    Diamonds = 2
}

public class ResourceData : Model
{
    public int value;
    public ResourceType type;
}
