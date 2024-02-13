using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    [SerializeField] private List<Achievement> _achievements;

    public void Initialllize(List<Achievement> achievements)
    {
        _achievements = achievements;
    }

    public void UnlockAchievement(string achievementID)
    {
        var _achievement = Contains(achievementID);
        if (_achievement != null)
        {
            _achievement.UnlockAchievement();
        }
    }

    public void LockAchievement(string achievementID)
    {
        var _achievement = Contains(achievementID);
        if (_achievement != null)
        {
            _achievement.LockAchievement();
        }
    }

    public void AddProgress(string achievementID, int newProgress)
    {
        var _achievement = Contains(achievementID);
        if (_achievement != null)
        {
            _achievement.AddProgress(newProgress);
        }
    }

    public void SetProgress(string achievementID, int newProgress)
    {
        var _achievement = Contains(achievementID);
        if (_achievement != null)
        {
            _achievement.SetProgress(newProgress);
        }
    }

    private Achievement Contains(string searchedID)
    {
        if (_achievements.Count == 0)
        {
            return null;
        }

        foreach (Achievement achievement in _achievements)
        {
            if (achievement.Id == searchedID)
            {
                return achievement;
            }
        }

        return null;
    }
}
