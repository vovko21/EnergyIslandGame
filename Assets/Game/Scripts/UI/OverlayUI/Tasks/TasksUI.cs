using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TasksUI : MonoBehaviour
{
    [Header("Tasks time")]
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private int _shuffleCycleTimeSeconds;

    [Header("Tasks group")]
    [SerializeField] private GameObject _container;
    [SerializeField] private TaskItemUI _itemPrefab;

    private List<TaskItemUI> _instantiatedItems = new List<TaskItemUI>();

    private void OnEnable()
    {
        if (TimeManager.Instance == null) return;

        if (TimeManager.Instance.IsServerTimeSuccess)
        {
            StartCoroutine(StartCoutTime());
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void Initialize()
    {
        foreach (var task in TaskManager.Instance.ActiveTasks)
        {
            var item = Instantiate(_itemPrefab.gameObject).GetComponent<TaskItemUI>();
            item.transform.SetParent(_container.transform);
            item.SetData(task);

            _instantiatedItems.Add(item);
        }
    }

    public void Deinitialize()
    {
        foreach (var item in _instantiatedItems)
        {
            Destroy(item.gameObject);
        }

        _instantiatedItems.Clear();
    }

    private IEnumerator StartCoutTime()
    {
        var tomorrow = TimeManager.Instance.Today.AddDays(1);

        while (true)
        {
            var dateTimeDifference = tomorrow - TimeManager.Instance.LocalDateTime;
            _timeText.text = dateTimeDifference.ToString("hh\\:mm");
            if (dateTimeDifference.Ticks <= 0)
            {
                TaskManager.Instance.ShuffleNewTasks();
                Deinitialize();
                Initialize();
                tomorrow = TimeManager.Instance.Today.AddDays(1);
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
