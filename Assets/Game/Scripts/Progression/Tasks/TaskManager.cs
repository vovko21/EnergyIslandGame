using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TaskManager : SingletonMonobehaviour<TaskManager>
{
    [Header("All tasks setup")]
    [SerializeField] private List<GameTask> _tasks;

    [Header("Tasks suffle settings")]
    [SerializeField] private int _tasksPerSuffle;
    [SerializeField] private List<GameTask> _activeTasks;

    public List<GameTask> ActiveTasks => _activeTasks;

    protected override void Awake()
    {
        base.Awake();

        ShuffleNewTasks();
    }

    public void Initialize()
    {
        var activeTasks = StorageService.Instance.GetActiveTasks();

        _activeTasks.Clear();
        foreach (var task in activeTasks) 
        {
            _activeTasks.Add(new GameTask(task));
        }
    }

    public void AddProgress(string taskID, int newProgress)
    {
        var task = Contains(taskID);
        if (task != null)
        {
            task.AddProgress(newProgress);
        }
    }

    public void SetProgress(string taskID, int newProgress)
    {
        var task = Contains(taskID);
        if (task != null)
        {
            task.SetProgress(newProgress);
        }
    }

    private GameTask Contains(string searchedID)
    {
        if (_activeTasks.Count == 0)
        {
            return null;
        }

        foreach (GameTask task in _activeTasks)
        {
            if (task.Id == searchedID)
            {
                return task;
            }
        }

        return null;
    }

    private List<GameTask> ShuffleTasks()
    {
        int count = _tasks.Count;
        System.Random rng = new System.Random();
        while (count > 1)
        {
            count--;
            int k = rng.Next(count + 1);
            var value = _tasks[k];
            _tasks[k] = _tasks[count];
            _tasks[count] = value;
        }

        return _tasks.Take(_tasksPerSuffle).ToList();
    }

    public void ResetTasksProgress()
    {
        foreach (var task in _tasks)
        {
            task.Reset();
        }
    }

    public void ShuffleNewTasks()
    {
        ResetTasksProgress();

        _activeTasks = ShuffleTasks();
    }
}
