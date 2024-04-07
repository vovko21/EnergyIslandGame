using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

public class TimeManager : SingletonMonobehaviour<TimeManager>
{
    public struct ServerDateTime
    {
        public string datetime;
        public string utc_datetime;
    }

    private DateTime _localDateTime;
    private DateTime _utcDateTime;

    public DateTime LocalDateTime => _localDateTime.AddSeconds(Time.realtimeSinceStartup);
    public DateTime UTCDateTime => _utcDateTime.AddSeconds(Time.realtimeSinceStartup);
    public bool IsServerTimeSuccess { get; private set; }

    private void Start()
    {
        StartCoroutine(GetTimeFromServer());
    }

    private IEnumerator GetTimeFromServer()
    {
        UnityWebRequest request = UnityWebRequest.Get("https://worldtimeapi.org/api/ip");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            ServerDateTime serverDateTime = JsonUtility.FromJson<ServerDateTime>(request.downloadHandler.text);

            _localDateTime = ParseToDateTime(serverDateTime.datetime);
            _utcDateTime = ParseToDateTime(serverDateTime.utc_datetime);

            IsServerTimeSuccess = true;
        }
        else
        {
            IsServerTimeSuccess = false;
        }
    }

    private DateTime ParseToDateTime(string value)
    {
        string date = Regex.Match(value, @"^\d{4}-\d{2}-\d{2}").Value;
        string time = Regex.Match(value, @"\d{2}:\d{2}:\d{2}").Value;
        return DateTime.Parse($"{date} {time}");
    }
}