using System.IO;
using UnityEngine;

public sealed class JsonDataContext : DataContext
{
    private string _filepath = Application.persistentDataPath + "/data.json";

    public override async System.Threading.Tasks.Task LoadAsync()
    {
        if (!File.Exists(_filepath)) return;
        using var reader = new StreamReader(_filepath);
        var json = await reader.ReadToEndAsync();
        JsonUtility.FromJsonOverwrite(json, _data);
    }

    public override async System.Threading.Tasks.Task SaveAsync()
    {
        var json = JsonUtility.ToJson(_data);
        using var writer = new StreamWriter(_filepath);
        await writer.WriteAsync(json);
    }
}
