using System;
using UnityEngine;

public class GameTimeManager : SingletonMonobehaviour<GameTimeManager>
{
    [Header("Date time settings")]
    [SerializeField][Range(1, 28)] private int _dateInMonth;
    [SerializeField][Range(1, 4)] private int _season;
    [SerializeField][Range(1, 99)] private int _year;
    [SerializeField][Range(1, 24)] private int _hour;
    [SerializeField][Range(0, 59)] private int _minuts;

    private InGameDateTime _currentDateTime;

    [Header("Tick settings")]
    [SerializeField] private int _minutesPerTick = 10;
    [SerializeField] private float _timeBetweenTicks = 1;
    private float _currentTimeBetweenTicks = 0;

    public event System.Action<InGameDateTime> OnDateTimeChanged;

    public InGameDateTime CurrentDateTime => _currentDateTime;
    public int MinutesPerTick => _minutesPerTick;
    public float TimeBetweenTicks => _timeBetweenTicks;

    protected override void Awake()
    {
        base.Awake();

        _currentDateTime = new InGameDateTime(_dateInMonth, _season - 1, _year, _hour, _minutesPerTick * 10);
    }

    private void Start()
    {
        //OnDateTimeChanged?.Invoke(_currentDateTime);
    }

    public void Initialize(int minutesPassed)
    {
        _currentDateTime = new InGameDateTime(1, 0, 1, 0, 0);
        _currentDateTime.AdvanceMinutes(minutesPassed);
        OnDateTimeChanged?.Invoke(_currentDateTime);
    }

    private void Update()
    {
        _currentTimeBetweenTicks += Time.deltaTime;

        if (_currentTimeBetweenTicks >= _timeBetweenTicks)
        {
            _currentTimeBetweenTicks = 0;
            Tick();
        }
    }

    private void Tick()
    {
        AdvanceTime();
    }

    private void AdvanceTime()
    {
        _currentDateTime.AdvanceMinutes(_minutesPerTick);

        OnDateTimeChanged?.Invoke(_currentDateTime);
    }
}



[System.Serializable]
public struct InGameDateTime
{
    #region Fields
    private Days _day;
    private int _date;
    private int _year;

    private int _hour;
    private int _minutes;

    private Season _season;

    private int _totalNumDays;
    private int _totalNumWeeks;
    #endregion

    #region Properties
    public Days Day => _day;
    public int Date => _date;
    public int Hour => _hour;
    public int Minute => _minutes;
    public Season Season => _season;
    public int Year => _year;
    public int TotalNumDays => _totalNumDays;
    public int TotalNumWeeks => _totalNumWeeks;
    public int TotalNumMinutes => ((_totalNumDays - 1) * 24 * 60) + (_hour * 60) + _minutes;
    public int CurrentWeek => _totalNumWeeks % 16 == 0 ? 16 : _totalNumWeeks % 16;
    #endregion

    #region Events
    public event System.Action<InGameDateTime> OnHourChanged;
    public event System.Action<InGameDateTime> OnDayChanged;
    public event System.Action<InGameDateTime> OnSeasonChanged;
    public event System.Action<InGameDateTime> OnYearChanged;
    #endregion

    #region Constructors
    public InGameDateTime(int date, int season, int year, int hour, int minute)
    {
        _day = (Days)(date % 7);
        if (_day == 0) _day = (Days)7;
        _date = date;
        _year = year;
        _season = (Season)season;

        _hour = hour;
        _minutes = minute;

        _totalNumDays = date + (28 * (int)_season) + (112 * (year - 1));
        _totalNumWeeks = 1 + _totalNumDays / 7;

        OnHourChanged = null;
        OnDayChanged = null;
        OnSeasonChanged = null;
        OnYearChanged = null;
    }
    #endregion

    #region TimeAdvancement
    public void AdvanceMinutes(int secondsToAdvanceBy)
    {
        if (secondsToAdvanceBy < 0) return;

        if (_minutes + secondsToAdvanceBy >= 60)
        {
            int hours = 0;
            int minutes = _minutes;

            _minutes = (minutes + secondsToAdvanceBy) % 60;
            hours = Mathf.FloorToInt((minutes + secondsToAdvanceBy) / 60f);

            while (hours > 0)
            {
                AdvanceHour();
                hours--;
            }
        }
        else
        {
            _minutes += secondsToAdvanceBy;
        }

        //if (_minutes + secondsToAdvanceBy >= 60)
        //{
        //    _minutes = (_minutes + secondsToAdvanceBy) % 60;
        //    AdvanceHour();
        //}
        //else
        //{
        //    _minutes += secondsToAdvanceBy;
        //}
    }

    private void AdvanceHour()
    {
        if ((_hour + 1) == 24)
        {
            _hour = 0;
            AdvanceDay();
        }
        else
        {
            _hour++;
        }

        OnHourChanged?.Invoke(this);
    }

    private void AdvanceDay()
    {
        _day++;

        if (_day > (Days)7)
        {
            _day = (Days)1;
            _totalNumWeeks++;
        }

        _date++;

        if (_date % 29 == 0)
        {
            AdvanceSeason();
            _date = 1;
        }

        _totalNumDays++;

        OnDayChanged?.Invoke(this);
    }

    private void AdvanceSeason()
    {
        if (Season == Season.Winter)
        {
            _season = Season.Spring;
            AdvanceYear();
        }
        else
        {
            _season++;
        }

        OnSeasonChanged?.Invoke(this);
    }

    private void AdvanceYear()
    {
        _date = 1;
        _year++;

        OnYearChanged?.Invoke(this);
    }
    #endregion

    #region Checks

    public bool IsNight()
    {
        return _hour > 20 || _hour < 6;
    }

    public bool IsMorning()
    {
        return _hour >= 6 && _hour <= 12;
    }

    public bool IsAfternoon()
    {
        return _hour > 12 && _hour < 18;
    }

    public bool IsWeekend()
    {
        return _day > Days.Friday ? true : false;
    }

    public bool IsParticularDay(Days day)
    {
        return _day == day;
    }

    #endregion

    #region Key Dates

    public InGameDateTime NewYearsDay(int year)
    {
        if (year == 0) year = 1;
        return new InGameDateTime(1, 0, year, 6, 0);
    }

    public InGameDateTime SummerSolstice(int year)
    {
        if (year == 0) year = 1;
        return new InGameDateTime(28, 1, year, 6, 0);
    }

    public InGameDateTime PumpkinHarvest(int year)
    {
        if (year == 0) year = 1;
        return new InGameDateTime(28, 2, year, 6, 0);
    }

    #endregion

    #region Start of season
    public InGameDateTime StartOfSeason(int season, int year)
    {
        season = Mathf.Clamp(season, 0, 3);
        if (year == 0) year = 1;

        return new InGameDateTime(1, season, year, 6, 0);
    }
    #endregion

    #region To string

    public override string ToString()
    {
        return $"Date: {DateToString()} Season: {_season} Time: {TimeToString()} Totals days: {_totalNumDays} Totals weeks: {_totalNumWeeks} ";
    }
    public string DateToString()
    {
        return $"{Day} {Date} {Year.ToString("D2")}";
    }

    public string TimeToString()
    {
        int adjustedHour = 0;

        if (_hour == 0)
        {
            adjustedHour = 12;
        }
        else if (_hour == 24)
        {
            adjustedHour = 12;
        }
        else if (_hour >= 13)
        {
            adjustedHour = _hour - 12;
        }
        else
        {
            adjustedHour = _hour;
        }

        string AmPm = Hour == 0 || Hour < 12 ? "AM" : "PM";

        return $"{adjustedHour.ToString("D2")}:{_minutes.ToString("D2")} {AmPm}";
    }

    #endregion

    #region Operators
    public static bool operator ==(InGameDateTime a, InGameDateTime b)
    {
        return (a.Year == b.Year && a.Date == b.Date && a.Hour == b.Hour && a.Minute == b.Minute);
    }

    public static bool operator !=(InGameDateTime a, InGameDateTime b)
    {
        return !(a.Year == b.Year && a.Date == b.Date && a.Hour == b.Hour && a.Minute == b.Minute);
    }
    #endregion
}

[System.Serializable]
public enum Days
{
    Null = 0,
    Monday = 1,
    Tueday = 2,
    Wedday = 3,
    Thuday = 4,
    Friday = 5,
    Saturday = 6,
    Sunday = 7
}

[System.Serializable]
public enum Season
{
    Spring = 0,
    Summer = 1,
    Autumn = 2,
    Winter = 3
}
