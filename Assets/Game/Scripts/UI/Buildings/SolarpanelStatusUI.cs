using UnityEngine;

public class SolarpanelStatusUI : BuildingStatusUI
{
    [Header("Night status")]
    [SerializeField] private GameObject _nightStatus;

    protected override void Start()
    {
        base.Start();

        _nightStatus.SetActive(false);
    }

    protected override void Update()
    {
        base.Update();

        if (_building.Status == BuildingStatus.NotProducing)
        {
            _nightStatus.SetActive(true);
        }
        else
        {
            _nightStatus.SetActive(false);
        }
    }
}
