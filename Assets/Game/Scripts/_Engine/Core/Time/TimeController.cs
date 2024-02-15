//using System;
//using UnityEngine;

//public struct GameDateTime
//{
//    public int day;
//    public int hour;
//    public int minute;

//    public GameDateTime(int day, int hour, int minute)
//    {
//        this.day = day;
//        this.hour = hour;
//        this.minute = minute;
//    }

//    public override string ToString()
//    {
//        return string.Format("Day {0}, {1:D2}:{2:D2}", day, hour, minute);
//    }
//}

//public class TimeController : SingletonMonobehaviour<TimeController>
//{
//    public float dayLengthInSeconds = 300f; // 5 real minutes
//    public float nightLengthInSeconds = 120f; // 2 real minutes

//    private float currentCycleTime = 0f; // Current time of the day-night cycle
//    private int daysPassed = 0; // Number of days passed
//    private GameDateTime currentGameTime; // Current in-game date and time

//    public GameDateTime CurrentGameTime => currentGameTime;
//    public float GameHourInSeconds => currentGameTime;
//    public event Action OnHourPassed;

//    void Update()
//    {
//        UpdateGameTime();
//    }

//    private void UpdateGameTime()
//    {
//        currentCycleTime += Time.deltaTime;

//        // Calculate the current phase of the cycle (0 to 1)
//        float totalCycleLength = dayLengthInSeconds + nightLengthInSeconds;
//        float cycleProgress = currentCycleTime / totalCycleLength;

//        if (cycleProgress >= 1f)
//        {
//            currentCycleTime -= totalCycleLength; // Subtract total cycle length to handle overflow
//            daysPassed++; // Increment the number of days passed
//        }

//        // Convert cycle progress to in-game time
//        float inGameMinutesPerDay = 24f * 60f; // Total in-game minutes per day
//        float currentTotalMinutesInGameDay = daysPassed * inGameMinutesPerDay + cycleProgress * inGameMinutesPerDay;

//        int currentInGameHour = Mathf.FloorToInt(currentTotalMinutesInGameDay / 60f) % 24; // Modulo 24 to keep it within 0-23
//        int currentInGameMinute = Mathf.FloorToInt(currentTotalMinutesInGameDay % 60f);

//        if(currentGameTime.hour != currentInGameHour)
//        {
//            OnHourPassed?.Invoke();
//        }

//        // Update current game date and time
//        currentGameTime = new GameDateTime(daysPassed, currentInGameHour, currentInGameMinute);
//    }
//}

using UnityEngine;
//using System;

public class TimeController : SingletonMonobehaviour<TimeController>
{
    // Time variables
    private int currentDay = 1;
    private int currentHour = 0;
    private int currentMinute = 0;

    // Duration settings
    private float realSecondsPerGameDay = 7f * 60f; // 7 real minutes per in-game day
    private float realSecondsPerGameHour;

    // Struct to hold game date time information
    public struct GameDateTime
    {
        public int day;
        public int hour;
        public int minute;
    }

    private float nextUpdateTime = 0f;

    // Property to determine if it's currently night or day
    public bool IsNight
    {
        get { return currentHour < 6 || currentHour >= 18; }
    }

    // Property to get the real duration of an in-game hour
    public float RealDurationOfInGameHour
    {
        get { return realSecondsPerGameHour; }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time >= nextUpdateTime)
        {
            UpdateTime();
            nextUpdateTime = Time.time + (RealDurationOfInGameHour / 60f); // Update every in-game minute
        }
    }

    void UpdateTime()
    {
        currentMinute++;

        if (currentMinute == 60)
        {
            currentHour++;
            currentMinute = 0;
        }

        if (currentHour == 24)
        {
            currentDay++;
            currentHour = 0;

        }

        // UpdateUI(); // You can add a method here to update UI with current time if needed
    }

    // Method to get current game date time
    public GameDateTime GetCurrentGameDateTime()
    {
        GameDateTime currentTime;
        currentTime.day = currentDay;
        currentTime.hour = currentHour;
        currentTime.minute = currentMinute;
        return currentTime;
    }

    void Start()
    {
        CalculateRealDurationOfHour();
    }

    void CalculateRealDurationOfHour()
    {
        // Calculate real duration of an in-game hour
        realSecondsPerGameHour = realSecondsPerGameDay / 24f;
    }
}