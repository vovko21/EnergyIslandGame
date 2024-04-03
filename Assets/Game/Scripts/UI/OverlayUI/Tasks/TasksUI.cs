using System;
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
    private IEnumerator _timerCoroutine;
    private DateTime? _nextDateTime;

    public DateTime? NextDateTime => _nextDateTime;

    private void OnEnable()
    {
        if (TimeManager.Instance == null) return;

        if (TimeManager.Instance.IsServerTimeSuccess)
        {
            if (_timerCoroutine == null)
            {
                _timerCoroutine = StartCoutTime();
                StartCoroutine(_timerCoroutine);
            }
            else
            {
                StopCoroutine(_timerCoroutine);
                _timerCoroutine = StartCoutTime();
                StartCoroutine(_timerCoroutine);
            }
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

    public void Initialize(DateTime? nextDateTime)
    {
        _nextDateTime = nextDateTime;    
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
        if (_nextDateTime == null)
        {
            _nextDateTime = NextTimeFrom(TimeManager.Instance.LocalDateTime);
        }

        while (true)
        {
            var dateTimeDifference = _nextDateTime.Value - TimeManager.Instance.LocalDateTime;
            if (dateTimeDifference.Ticks >= 0)
            {
                //_timeText.text = dateTimeDifference.ToString("hh\\:mm\\:ss");
                _timeText.text = dateTimeDifference.ToString("hh\\:mm");
            }
            if (dateTimeDifference.Ticks <= 0)
            {
                TaskManager.Instance.ShuffleNewTasks();

                Deinitialize();
                Initialize();

                _nextDateTime = NextTimeFrom(TimeManager.Instance.LocalDateTime);
            }
            yield return new WaitForSeconds(1f);
        }
    }

    private DateTime NextTimeFrom(DateTime dateTime)
    {
        return dateTime.AddDays(1);
    }
}
