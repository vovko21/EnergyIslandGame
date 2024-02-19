using UnityEngine;
using UnityEngine.UI;

public class BuildingStatusUI : MonoBehaviour
{
    [Header("Progress status")]
    [SerializeField] protected ProductionBuilding _building;
    [SerializeField] protected Image _progressImage;

    [Header("Max status")]
    [SerializeField] protected GameObject _maxStatus;

    protected float _timeToProduce;
    protected float _progressFill;
    protected float _timePassed;

    protected virtual void Start()
    {
        _timeToProduce = (60f / TimeManager.Instance.MinutesPerTick) * TimeManager.Instance.TimeBetweenTicks;

        _maxStatus.SetActive(false);
    }

    protected virtual void Update()
    {
        if(_building.Status == BuildingStatus.Producing)
        {
            UpdateProgress();
        }

        if (_building.Status == BuildingStatus.MaxedOut)
        {
            _maxStatus.SetActive(true);
        }
        else
        {
            _maxStatus.SetActive(false);
        }
    }

    protected void UpdateProgress()
    {
        _timePassed += Time.deltaTime;

        _progressFill = Mathf.Clamp01(_timePassed / _timeToProduce);

        _progressImage.fillAmount = Mathf.Lerp(0, 1, _progressFill);

        if (_timePassed >= _timeToProduce)
        {
            _timePassed -= _timeToProduce;
        }
    }
}
