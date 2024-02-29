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

    public string Id => _id;
    public string Name => _name;
    public string Description => _description;
    public bool IsCompleted => _isCompleted;
    public int ProgressTarget => _progressTarget;
    public int ProgressCurrent => _progressCurrent;

    public GameTask(string name, string description, int progressTarget)
    {
        _name = name;
        _description = description;
        _progressTarget = progressTarget;
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
}
