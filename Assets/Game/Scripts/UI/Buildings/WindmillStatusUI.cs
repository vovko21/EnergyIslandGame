using UnityEngine;

public class WindmillStatusUI : BuildingStatusUI
{
    [Header("Wind status")]
    [SerializeField] private GameObject _windStatus;

    protected override void Start()
    {
        base.Start();

        _windStatus.SetActive(false);
    }

    protected override void Update()
    {
        base.Update();

        if (_building.Status == BuildingStatus.NotProducing)
        {
            _windStatus.SetActive(true);
        }
        else
        {
            _windStatus.SetActive(false);
        }
    }
}
