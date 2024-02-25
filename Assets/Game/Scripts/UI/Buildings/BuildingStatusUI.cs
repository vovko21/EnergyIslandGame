using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BuildingStatusUI : MonoBehaviour
{
    [Header("Main settings")]
    [SerializeField] protected ProductionBuilding _building;
    [SerializeField] protected InteractWithBuildingArea _interactArea;

    [Header("Progress status")]
    [SerializeField] protected Image _progressImage;

    [Header("Max status")]
    [SerializeField] protected GameObject _maxStatus;

    [Header("Broken status")]
    [SerializeField] protected GameObject _brokenStatus;
    [SerializeField] protected Image _brokenFillImage;

    protected float _timeToProduce;
    protected float _progressFill;
    protected float _timePassed;

    protected IEnumerator _progressCoroutine;

    protected virtual void OnEnable()
    {
        _building.OnStatusChanged += OnBuildingStatusChanged;
        _interactArea.OnFixingStart += OnFixingStart;

        var camera = Camera.main;

        transform.LookAt(camera.transform, Vector3.up);
    }

    protected virtual void OnDisable()
    {
        _building.OnStatusChanged -= OnBuildingStatusChanged;
        _interactArea.OnFixingStart -= OnFixingStart;
    }

    protected virtual void Start()
    {
        _timeToProduce = (60f / GameTimeManager.Instance.MinutesPerTick) * GameTimeManager.Instance.TimeBetweenTicks;

        _brokenStatus.SetActive(false);
        _maxStatus.SetActive(false);

        OnBuildingStatusChanged(_building.Status);
    }

    protected virtual void OnBuildingStatusChanged(BuildingStatus status)
    {
        if (_progressCoroutine != null)
        {
            StopCoroutine(_progressCoroutine);
            _progressCoroutine = null;
        }
        
        if (status == BuildingStatus.Broken)
        {
            _brokenStatus.SetActive(true);
        }
        else
        {
            _brokenStatus.SetActive(false);
            _brokenFillImage.fillAmount = 1;
        }

        if (status == BuildingStatus.MaxedOut)
        {
            _maxStatus.SetActive(true);
        }
        else
        {
            _maxStatus.SetActive(false);
        }

        if (status == BuildingStatus.Producing)
        {
            if(_progressCoroutine == null)
            {
                _progressCoroutine = UpdateProgress();
                StartCoroutine(_progressCoroutine);
            }
            else
            {
                StopCoroutine(_progressCoroutine);
                _progressCoroutine = UpdateProgress();
                StartCoroutine(_progressCoroutine);
            }
        }
        else
        {
            if (_progressCoroutine != null) StopCoroutine(_progressCoroutine);
            _progressCoroutine = null;
            _progressImage.fillAmount = 0;
        }
    }

    protected IEnumerator UpdateProgress()
    {
        _timePassed = 0;
        while (_progressCoroutine != null)
        {
            _timePassed += Time.deltaTime;

            _progressFill = Mathf.Clamp01(_timePassed / _timeToProduce);

            _progressImage.fillAmount = Mathf.Lerp(0, 1, _progressFill);

            if (_timePassed >= _timeToProduce)
            {
                _timePassed = 0;
            }

            yield return null;
        }
    }

    private void OnFixingStart()
    {
        StartCoroutine(UpdateFixing());
    }
    
    private IEnumerator UpdateFixing()
    {
        bool isFinished = false;
        _timePassed = 0;
        while (!isFinished)
        {
            _timePassed += Time.deltaTime;

            _progressFill = Mathf.Clamp01(_timePassed / _building.CurrentStats.MaintenanceTime);

            _brokenFillImage.fillAmount = Mathf.Lerp(0, 1, 1f - _progressFill);

            if (_timePassed >= _building.CurrentStats.MaintenanceTime)
            {
                _timePassed = 0;

                isFinished = true;
            }

            yield return null;
        }
    }
}
