using UnityEngine;

/// <summary>
/// Level formula link - https://oldschool.runescape.wiki/w/Experience
/// </summary>
public class ExponentialAlgorithm : LevelAlgorithm
{
    private float _additionMultiplayer = 300f;
    private float _powerMultiplayer = 2f;
    private float _powDivisionMultiplayer = 7f;
    private int _divisionMultiplayer = 4;

    public override long GetTotalXpToLevel(int level)
    {
        long solveForXpToNextLevel = 0;

        for (int l = 1; l <= level - 1; l++)
        {
            solveForXpToNextLevel += (long)Mathf.Floor(l + _additionMultiplayer * Mathf.Pow(_powerMultiplayer, l / _powDivisionMultiplayer));
        }

        return solveForXpToNextLevel / _divisionMultiplayer;
    }
}
