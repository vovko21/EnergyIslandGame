using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    public List<ResourceData> Resources = new List<ResourceData>();
    public List<BuildingData> ActiveBuildings = new List<BuildingData>();
    public List<GameTaskData> ActiveTasks = new List<GameTaskData>();

    public string LastClaimBonusTime;
    public string NextTasksTime;
    public int DaysBonusClaimedInRow = 0;
    public int InGameMinutesPassed = 0;
    public bool Initialized = false;
}