using UnityEngine;

public class PlayerStats
{
    private PlayerSO _playerSO;
    private float _speedBuff;

    public float Speed => _playerSO.Speed + _speedBuff;
    public float RotationSpeed => _playerSO.RotationSpeed;

    public PlayerStats(PlayerSO playerSO)
    {
        _playerSO = playerSO;
    }

    public void ApplySpeedBuff(float speed, float time, MonoBehaviour monobehaviour)
    {
        Timer timer = new Timer(monobehaviour);

        _speedBuff = speed;

        timer.Set(time);

        timer.StartCountingTime();

        timer.TimeIsOver += Timer_TimeIsOver;
    }

    private void Timer_TimeIsOver()
    {
        _speedBuff = 0;
    }

    public bool IsBuffed()
    {
        if (_speedBuff == 0) return false;

        return true;
    }
}
