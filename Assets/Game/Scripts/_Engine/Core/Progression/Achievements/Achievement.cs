using UnityEngine;

public struct AchievementEvent
{
    public Achievement Achievement;

    public AchievementEvent(Achievement achievement)
    {
        Achievement = achievement;
    }

    static AchievementEvent e;

    public static void Trigger(Achievement achievement)
    {
        e.Achievement = achievement;
        EventManager.TriggerEvent(e);
    }
}

[System.Serializable]
public class Achievement
{
    public enum AchievementType
    {
        Simple = 0,
        Progress = 1
    }

    [Header("Base properties")]
    [SerializeField] private string _id;
    [SerializeField] private AchievementType _type;
    [SerializeField] private string _name;
    [SerializeField] private string _description;

    [SerializeField] private bool _isUnlocked;

    [Header("Progress")]
    [SerializeField] private int _progressTarget;
    [SerializeField] private int _progressCurrent;

    public string Id => _id;
    public AchievementType Type => _type;
    public string Name => _name;
    public string Description => _description;
    public bool IsUnlocked => _isUnlocked;
    public int ProgressTarget => _progressTarget;
    public int ProgressCurrent => _progressCurrent;

    public virtual void UnlockAchievement()
    {
        if (_isUnlocked)
        {
            return;
        }

        Debug.Log(_name + " UNLOCKED");

        _isUnlocked = true;
    }

    public virtual void LockAchievement()
    {
        _isUnlocked = false;
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
            UnlockAchievement();
        }
    }
}
