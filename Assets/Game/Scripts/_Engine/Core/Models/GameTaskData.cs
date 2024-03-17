[System.Serializable]
public class GameTaskData : Model
{
    public string name;
    public string description;
    public bool isCompleted;
    public int progressTarget;
    public int progressCurrent;
    public ResourceType resourceType;
    public int rewardValue;
    public bool isTook = false;
}