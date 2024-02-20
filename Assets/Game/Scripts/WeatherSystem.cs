using System;
using System.Collections.Generic;
using UnityEngine;

public enum WeatherType
{
    Default = 0,
    Sunny = 1,
    Winddy = 2,
}

[System.Serializable]
public struct Weather
{
    public WeatherType type;
    public float weight;
}

public struct WeatherEvent
{
    public Weather weather;
}

public class WeatherSystem : SingletonMonobehaviour<WeatherSystem>
{
    [Header("Weather settings")]
    [SerializeField] private List<Weather> _weathers;
    [SerializeField] private int _minutesBetweenWeatherRand;

    [Header("Wind settings")]
    [SerializeField] private float _minWindSpeed;
    [SerializeField] private float _maxWindSpeed;
    [SerializeField] private float _windDayCoefficient;

    [Header("Sun settings")]
    [SerializeField] private float _minSunlight;
    [SerializeField] private float _maxSunlight;
    [SerializeField] private float _sunnyDayCoefficient;

    [Header("Info")]
    #region ReadOnly
#if UNITY_EDITOR
    [ReadOnly]
    [SerializeField]
#endif
    #endregion
    private float _windSpeedKmh;
    #region ReadOnly
#if UNITY_EDITOR
    [ReadOnly]
    [SerializeField]
#endif
    #endregion
    private float _sunlight;
    #region ReadOnly
#if UNITY_EDITOR
    [ReadOnly]
    [SerializeField]
#endif
    #endregion
    private float _totalWeight;

    private Weather _currentWeather;
    private InGameDateTime _lastTime;

    public Weather CurrentWeather => _currentWeather;
    public float WindSpeedKmh => _windSpeedKmh;
    public float Sunlight => _sunlight;
    public float SunnyDayCoefficient => _sunnyDayCoefficient;

    public event Action<Weather> OnWeatherChanged;

    private void Start()
    {
        CalculateTotalWeight();

        _lastTime = TimeManager.Instance.CurrentDateTime;
        _lastTime.AdvanceMinutes(_minutesBetweenWeatherRand);
        TimeManager.Instance.OnDateTimeChanged += OnDateTimeChanged;
    }

    private void OnDisable()
    {
        TimeManager.Instance.OnDateTimeChanged -= OnDateTimeChanged;
    }

    private void OnDateTimeChanged(InGameDateTime dateTime)
    {
        if ((_lastTime.Hour + _lastTime.Minute / 60f) == (dateTime.Hour + dateTime.Minute / 60f))
        {
            _lastTime.AdvanceMinutes(_minutesBetweenWeatherRand);
            OnTimePassed();
        }
    }

    private void OnTimePassed()
    {
        UpdateWeather();

        UpdateWindSpeed();

        UpdateSun();
    }

    private void UpdateWeather()
    {
        var pointer = UnityEngine.Random.Range(0f, _totalWeight);
        var currenWeight = 0f;
        foreach (var weather in _weathers)
        {
            currenWeight += weather.weight;

            if (currenWeight > pointer)
            {
                _currentWeather = weather;
                OnWeatherChanged?.Invoke(weather);
                EventManager.TriggerEvent(new WeatherEvent() { weather = _currentWeather });
                break;
            }
        }
    }

    private void UpdateWindSpeed()
    {
        _windSpeedKmh = UnityEngine.Random.Range(_minWindSpeed, _maxWindSpeed);

        if(_currentWeather.type == WeatherType.Winddy)
        {
            _windSpeedKmh *= _windDayCoefficient;
        }
    }

    private void UpdateSun()
    {
        _sunlight = UnityEngine.Random.Range(_minSunlight, _maxSunlight);

        if (_currentWeather.type == WeatherType.Sunny)
        {
            _sunlight *= _sunnyDayCoefficient;
        }
    }

    private void CalculateTotalWeight()
    {
        _totalWeight = 0;
        foreach (var weather in _weathers)
        {
            _totalWeight += weather.weight;
        }
    }

    private void OnValidate()
    {
        CalculateTotalWeight();
    }
}
