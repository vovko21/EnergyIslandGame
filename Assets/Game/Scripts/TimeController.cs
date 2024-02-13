using System;
using UnityEngine;

public struct GameDateTime
{
    public int day;
    public int hour;
    public int minute;

    public GameDateTime(int day, int hour, int minute)
    {
        this.day = day;
        this.hour = hour;
        this.minute = minute;
    }

    public override string ToString()
    {
        return string.Format("Day {0}, {1:D2}:{2:D2}", day, hour, minute);
    }
}

public class TimeController : SingletonMonobehaviour<TimeController>
{
    public float dayLengthInSeconds = 300f; // 5 real minutes
    public float nightLengthInSeconds = 120f; // 2 real minutes

    private float currentCycleTime = 0f; // Current time of the day-night cycle
    private int daysPassed = 0; // Number of days passed
    private GameDateTime currentGameTime; // Current in-game date and time

    public GameDateTime CurrentGameTime => currentGameTime;
    public event Action OnHourPassed;

    void Update()
    {
        UpdateGameTime();
    }

    private void UpdateGameTime()
    {
        currentCycleTime += Time.deltaTime;

        // Calculate the current phase of the cycle (0 to 1)
        float totalCycleLength = dayLengthInSeconds + nightLengthInSeconds;
        float cycleProgress = currentCycleTime / totalCycleLength;

        if (cycleProgress >= 1f)
        {
            currentCycleTime -= totalCycleLength; // Subtract total cycle length to handle overflow
            daysPassed++; // Increment the number of days passed
        }

        // Convert cycle progress to in-game time
        float inGameMinutesPerDay = 24f * 60f; // Total in-game minutes per day
        float currentInGameMinutes = daysPassed * inGameMinutesPerDay + cycleProgress * inGameMinutesPerDay;

        int currentInGameHour = Mathf.FloorToInt(currentInGameMinutes / 60f) % 24; // Modulo 24 to keep it within 0-23
        int currentInGameMinute = Mathf.FloorToInt(currentInGameMinutes % 60f);

        if(currentGameTime.hour != currentInGameHour)
        {
            OnHourPassed?.Invoke();
        }

        // Update current game date and time
        currentGameTime = new GameDateTime(daysPassed, currentInGameHour, currentInGameMinute);
    }
}