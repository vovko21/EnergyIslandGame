using System;
using UnityEngine;

public class Level
{
    protected LevelData _levelData;
    protected LevelAlgorithm _algorithm;

    public LevelData Data => _levelData;

    public event Action<int> OnLevelUp;

    public Level(LevelAlgorithm algorithm)
    {
        if (algorithm == null) throw new ArgumentNullException(nameof(algorithm));

        _algorithm = algorithm;

        _levelData.number = 1;
        _levelData.xpToNextLevel = GetXpForNextLevel();
    }

    public Level(LevelData levelData, LevelAlgorithm algorithm)
    {
        if (algorithm == null) throw new ArgumentNullException(nameof(algorithm));

        _algorithm = algorithm;
        _levelData = levelData;
    }

    protected long GetXpForNextLevel()
    {
        var totalXpToNextLevel = _algorithm.GetTotalXpToLevel(_levelData.number + 1);
        return totalXpToNextLevel - _levelData.totalXp;
    }

    public virtual void AddXp(long amount)
    {
        _levelData.currentXp += amount;
        _levelData.totalXp += amount;
        while (_levelData.currentXp >= _levelData.xpToNextLevel)
        {
            LevelUp();
        }
    }

    protected void LevelUp()
    {
        _levelData.number++;

        _levelData.currentXp = Mathf.RoundToInt(_levelData.currentXp - _levelData.xpToNextLevel);

        _levelData.xpToNextLevel = GetXpForNextLevel();

        OnLevelUp?.Invoke(_levelData.number);
    }

    public void Clear()
    {
        _levelData.number = 1;
        _levelData.currentXp = 0;
        _levelData.xpToNextLevel = GetXpForNextLevel();
    }
}
