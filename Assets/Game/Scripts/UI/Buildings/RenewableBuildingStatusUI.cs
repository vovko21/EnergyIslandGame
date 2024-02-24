using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RenewableBuildingStatusUI : BuildingStatusUI
{
    [Header("Maintenance status")]
    [SerializeField] protected GameObject _maintenanceStatus;
    [SerializeField] protected Image _maintenanceFillImage;

    protected override void OnEnable()
    {
        base.OnEnable();

        _interactArea.OnMaintenceStart += OnMaintenceStart;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        _interactArea.OnMaintenceStart -= OnMaintenceStart;
    }

    protected override void Start()
    {
        base.Start();

        _maintenanceStatus.SetActive(false);
    }

    protected override void OnBuildingStatusChanged(BuildingStatus status)
    {
        base.OnBuildingStatusChanged(status);

        if (status == BuildingStatus.Maintenance)
        {
            _maintenanceStatus.SetActive(true);
        }
        else
        {
            _maintenanceStatus.SetActive(false);
            _maintenanceFillImage.fillAmount = 1;
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
}
