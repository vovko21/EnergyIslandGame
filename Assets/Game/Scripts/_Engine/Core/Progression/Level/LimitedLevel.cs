using System;

public class LimitedLevel : Level
{
    private int _maxLevel;
    public int MaxLevel { get => _maxLevel; }

    public Action OnMaxLevelReach;

    public LimitedLevel(LevelAlgorithm algorithm, int maxLevel) : base(algorithm)
    {
        if (maxLevel <= 1) throw new ArgumentException();

        _maxLevel = maxLevel;
    }

    public LimitedLevel(LevelData levelData, LevelAlgorithm algorithm, int maxLevel) : base(levelData, algorithm)
    {
        if (maxLevel <= 1) throw new ArgumentException();

        _maxLevel = maxLevel;
    }

    public override void AddXp(long amount)
    {
        if (_levelData.number == _maxLevel) throw new LevelMaxException();

        _levelData.currentXp += amount;
        _levelData.totalXp += amount;
        while (_levelData.currentXp >= _levelData.xpToNextLevel)
        {
            if (_levelData.number == _maxLevel)
            {
                _levelData.number = _maxLevel;
                _levelData.currentXp = 0;
                _levelData.xpToNextLevel = 0;
                OnMaxLevelReach?.Invoke();
                break;
            }
            LevelUp();
        }
    }

    public bool IsMax()
    {
        if (_levelData.number < _maxLevel) return false;

        return true;
    }
}
