using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DateWeatherSeasonManager : SingletonManager<DateWeatherSeasonManager>
{
    [System.Serializable]
    public struct GameState
    {
        public int month;
        public int day;
        public SeasonSystem.Season currentSeason;
        public bool isRaining;
        public bool isSnowing;
        public float nextWeatherCheck;
    }

    private GameState savedState;

    protected override void Awake()
    {
        base.Awake();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SaveCurrentState();  // 현재 상태 저장
        RestoreState();      // 새 씬에 상태 복원
    }

    private void SaveCurrentState()
    {
        var dateSystem = FindObjectOfType<DateSystem>();
        var seasonSystem = FindObjectOfType<SeasonSystem>();
        var weatherSystem = FindObjectOfType<WeatherSystem>();

        if (dateSystem != null && seasonSystem != null && weatherSystem != null)
        {
            savedState = new GameState
            {              
                month = dateSystem.CurrentMonth,
                day = dateSystem.CurrentDay,
                currentSeason = seasonSystem.CurrentSeason,
                isRaining = weatherSystem.IsRaining,
                isSnowing = weatherSystem.IsSnowing,
                nextWeatherCheck = weatherSystem.NextWeatherCheck
            };
        }
    }

    private void RestoreState()
    {
        var dateSystem = FindObjectOfType<DateSystem>();
        var seasonSystem = FindObjectOfType<SeasonSystem>();
        var weatherSystem = FindObjectOfType<WeatherSystem>();

        if (dateSystem != null)
            dateSystem.SetDate(savedState.month, savedState.day);

        if (seasonSystem != null)
            seasonSystem.Initialize(savedState.currentSeason);

        if (weatherSystem != null)
            weatherSystem.Initialize(savedState.isRaining, savedState.isSnowing, savedState.nextWeatherCheck);
    }
}
