using UnityEngine;

public class GameTask : MonoBehaviour
{
    [Header("Base properties")]
    [SerializeField] private string _name;
    [SerializeField] private string _description;

    [SerializeField] private bool _isCompleted;

    [Header("Progress")]
    [SerializeField] private int _progressTarget;
    [SerializeField] private int _progressCurrent;

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

}
