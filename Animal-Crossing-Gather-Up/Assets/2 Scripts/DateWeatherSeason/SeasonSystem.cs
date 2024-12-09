using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonSystem : MonoBehaviour
{
    public enum Season { Spring, Summer, Fall, Winter }

    [System.Serializable]
    public class SeasonSettings
    {
        public Season season;
        public float sunriseHour;    // 계절별 일출 시간
        public float sunsetHour;     // 계절별 일몰 시간
        public Color ambientColor;    // 계절별 환경광 색상
    }

    [SerializeField] private SeasonSettings[] seasonSettings;
    private Season currentSeason;

    public static event Action<Season> OnSeasonChanged;
    public Season CurrentSeason => currentSeason;

    private void OnEnable()
    {
        DateSystem.OnMonthChanged += UpdateSeason;
    }

    private void OnDisable()
    {
        DateSystem.OnMonthChanged -= UpdateSeason;
    }

    private void UpdateSeason(int month)
    {
        // 3개월마다 계절 변경
        Season newSeason = (Season)((month - 1) / 3);
        if (newSeason != currentSeason)
        {
            currentSeason = newSeason;
            ApplySeasonSettings();
            OnSeasonChanged?.Invoke(currentSeason);
        }
    }

    private void ApplySeasonSettings()
    {
        var settings = seasonSettings[(int)currentSeason];

        // TimeManager 설정 업데이트
        TimeManager.Instance.sunriseHour = settings.sunriseHour;
        TimeManager.Instance.sunsetHour = settings.sunsetHour;

        // 환경 설정 적용
        RenderSettings.ambientLight = settings.ambientColor;
    }
    public void Initialize(Season season)
    {
        currentSeason = season;
        ApplySeasonSettings();
        OnSeasonChanged?.Invoke(currentSeason);
    }
}
