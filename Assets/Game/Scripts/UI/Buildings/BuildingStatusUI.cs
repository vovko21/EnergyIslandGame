using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BuildingStatusUI : MonoBehaviour
{
    [Header("Main settings")]
    [SerializeField] protected InteractArea _interactArea;

    [Header("Progress status")]
    [SerializeField] protected ProductionBuilding _building;
    [SerializeField] protected Image _progressImage;

    [Header("Maintenance status")]
    [SerializeField] protected GameObject _maintenanceStatus;
    [SerializeField] protected Image _maintenanceFillImage;

    [Header("Max status")]
    [SerializeField] protected GameObject _maxStatus;

    [Header("Broken status")]
    [SerializeField] protected GameObject _brokenStatus;
    [SerializeField] protected Image _brokenFillImage;

    protected float _timeToProduce;
    protected float _progressFill;
    protected float _timePassed;

    private void OnEnable()
    {
        _building.OnStatusChanged += OnBuildingStatusChanged;
        _interactArea.OnMaintenceStart += OnMaintenceStart;
        _interactArea.OnFixingStart += OnFixingStart;

        var camera = Camera.main;

        transform.LookAt(camera.transform, Vector3.up);
    }

    private void OnDisable()
    {
        _building.OnStatusChanged -= OnBuildingStatusChanged;
        _interactArea.OnMaintenceStart -= OnMaintenceStart;
        _interactArea.OnFixingStart -= OnFixingStart;
    }

    protected virtual void Start()
    {
        _timeToProduce = (60f / GameTimeManager.Instance.MinutesPerTick) * GameTimeManager.Instance.TimeBetweenTicks;

        _brokenStatus.SetActive(false);
        _maxStatus.SetActive(false);
        _maintenanceStatus.SetActive(false);

        OnBuildingStatusChanged(_building.Status);
    }

    protected virtual void OnBuildingStatusChanged(BuildingStatus status)
    {
        StopAllCoroutines();
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

        if (status == BuildingStatus.Maintenance)
        {
            _maintenanceStatus.SetActive(true);
        }
        else
        {
            _maintenanceStatus.SetActive(false);
            _maintenanceFillImage.fillAmount = 1;
        }

        if (status == BuildingStatus.Producing)
        {
            StartCoroutine(UpdateProgress());
        }
        else
        {
            _progressImage.fillAmount = 0;
        }
    }

    protected IEnumerator UpdateProgress()
    {
        bool isFinished = false;
        _timePassed = 0;
        while (!isFinished)
        {
            _timePassed += Time.deltaTime;

            _progressFill = Mathf.Clamp01(_timePassed / _timeToProduce);

            _progressImage.fillAmount = Mathf.Lerp(0, 1, _progressFill);

            if (_timePassed >= _timeToProduce)
            {
                _timePassed = 0;

                isFinished = true;
            }

            yield return null;
        }
    }

    private void OnMaintenceStart()
    {
        StartCoroutine(UpdateMaintenance());
    }

    private IEnumerator UpdateMaintenance()
    {
        bool isFinished = false;
        _timePassed = 0;
        while (!isFinished)
        {
            _timePassed += Time.deltaTime;

            _progressFill = Mathf.Clamp01(_timePassed / _building.CurrentStats.MaintenanceTime);

            _maintenanceFillImage.fillAmount = Mathf.Lerp(0, 1, 1f - _progressFill);

            if (_timePassed >= _building.CurrentStats.MaintenanceTime)
            {
                _timePassed = 0;

                isFinished = true;
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
