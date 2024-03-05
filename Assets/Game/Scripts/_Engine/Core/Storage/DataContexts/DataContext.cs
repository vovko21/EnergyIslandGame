using System;
using System.Collections.Generic;

[Serializable]
public abstract class DataContext
{
    protected GameData _data = new GameData();

    public GameData Data => _data;

    public abstract System.Threading.Tasks.Task LoadAsync();
    public abstract System.Threading.Tasks.Task SaveAsync();

    public List<T> Set<T>()
    {
        if (typeof(T) == typeof(ResourceData))
        {
            return _data.Resources as List<T>;
        }
        if (typeof(T) == typeof(BuildingData))
        {
            return _data.ActiveBuildings as List<T>;
        }

        throw new NotImplementedException();
    }
}
