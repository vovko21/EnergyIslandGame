using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    public List<ResourceData> Resources = new List<ResourceData>();
    public List<BuildingData> ActiveBuildings = new List<BuildingData>();

    public bool Initialized;
}
