using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[Serializable]
public abstract class DataContext
{
    protected GameData _data = new GameData();
    public GameData Data => _data;

    public abstract Task LoadAsync();
    public abstract Task SaveAsync();

    public List<T> Set<T>()
    {
        if (typeof(T) == typeof(ResourceData))
        {
            return _data.Resources as List<T>;
        }

        return null;
    }
}
