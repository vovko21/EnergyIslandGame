using UnityEngine;

public class BuildArea : BuyArea
{
    [Header("Building parametrs")]
    [SerializeField] private GameObject _building;
    [SerializeField] private GameObject _triggerArea;

    private void Start()
    {
        _building.SetActive(false);
    }

    protected override void OnBuyed()
    {
        _building.SetActive(true);

        Destroy(_triggerArea);
    }
}
