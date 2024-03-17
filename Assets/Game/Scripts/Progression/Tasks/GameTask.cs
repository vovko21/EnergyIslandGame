using UnityEngine;

[System.Serializable]
public class GameTask
{
    [Header("Base properties")]
    [SerializeField] private string _id;
    [SerializeField] private string _name;
    [SerializeField] private string _description;

    [SerializeField] private bool _isCompleted;

    [Header("Progress")]
    [SerializeField] private int _progressTarget;
    [SerializeField] private int _progressCurrent;

    [Header("Reward")]
    [SerializeField] private ResourceType _resourceType;
    [SerializeField] private int _rewardValue;

    public bool isTook = false;

    public string Id => _id;
    public string Name => _name;
    public string Description => _description;
    public bool IsCompleted => _isCompleted;
    public int ProgressTarget => _progressTarget;
    public int ProgressCurrent => _progressCurrent;
    public ResourceType ResourceType => _resourceType;
    public int RewardValue => _rewardValue;

    public GameTask(string name, string description, int progressTarget)
    {
        _name = name;
        _description = description;
        _progressTarget = progressTarget;
    }

    public GameTask(GameTaskData gameTaskData)
    {
        _id = gameTaskData.id; 
        _name = gameTaskData.name; 
        _description = gameTaskData.description;
        _isCompleted = gameTaskData.isCompleted;
        _progressTarget = gameTaskData.progressTarget;
        _progressCurrent = gameTaskData.progressCurrent;
        _resourceType = gameTaskData.resourceType;
        _rewardValue = gameTaskData.rewardValue;
        isTook = gameTaskData.isTook;
    }

    public virtual void AddProgress(int newProgress)
    {
        _progressCurrent += newProgress;
        EvaluateProgress();
    }

    public void SetProgress(int newProgress)
    {
        _progressCurrent = newProgress;
        EvaluateProgress();
    }

    private void EvaluateProgress()
    {
        if (_progressCurrent >= _progressTarget)
        {
            _progressCurrent = _progressTarget;
            CompleteTask();
        }
    }

    public virtual void CompleteTask()
    {
        if (_isCompleted)
        {
            return;
        }

        Debug.Log(_name + " UNLOCKED");

        _isCompleted = true;
    }

    public void Reset()
    {
        _progressCurrent = 0;
        _isCompleted = false;
        isTook = false;
    }
}
