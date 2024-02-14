using UnityEngine;

public class BuildArea : BuyArea
{
    [Header("Building parametrs")]
    [SerializeField] private GameObject _buildingPrefab;
    [SerializeField] private Transform _buildingPosition;
    
    protected override void OnBuyed()
    {
        Instantiate(_buildingPrefab, _buildingPosition.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
