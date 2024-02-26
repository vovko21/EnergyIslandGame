using UnityEngine;

public class PlayerStats
{
    private PlayerSO _playerSO;
    private float _speedAddative;
    
    public float Speed => _playerSO.Speed + _speedAddative;
    public float RotationSpeed => _playerSO.RotationSpeed;
    
    public PlayerStats(PlayerSO playerSO)
    {
        _playerSO = playerSO;
    }

    public void AddSpeed(float speed)
    {
        _speedAddative += speed;
    }
}
