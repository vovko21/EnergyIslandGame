using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RenewableBuildingStatusUI : BuildingStatusUI
{
    [Header("Maintenance status")]
    [SerializeField] private GameObject _maintenanceStatus;
    [SerializeField] private Image _maintenanceFillImage;

    protected override void OnEnable()
    {
        base.OnEnable();

        _interactArea.OnMaintenceChanged += OnMaintenceChanged;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        _interactArea.OnMaintenceChanged -= OnMaintenceChanged;
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

    private void OnMaintenceChanged(bool condition)
    {
        if(condition)
        {
            StartCoroutine(UpdateMaintenance());
        }
        else
        {
            StopAllCoroutines();
        }
    }

    private IEnumerator UpdateMaintenance()
    {
        bool isFinished = false;
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
