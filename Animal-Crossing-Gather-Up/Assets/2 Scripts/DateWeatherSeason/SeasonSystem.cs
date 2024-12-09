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
        public float sunriseHour;    // ������ ���� �ð�
        public float sunsetHour;     // ������ �ϸ� �ð�
        public Color ambientColor;    // ������ ȯ�汤 ����
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
        // 3�������� ���� ����
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

        // TimeManager ���� ������Ʈ
        TimeManager.Instance.sunriseHour = settings.sunriseHour;
        TimeManager.Instance.sunsetHour = settings.sunsetHour;

        // ȯ�� ���� ����
        RenderSettings.ambientLight = settings.ambientColor;
    }
    public void Initialize(Season season)
    {
        currentSeason = season;
        ApplySeasonSettings();
        OnSeasonChanged?.Invoke(currentSeason);
    }
}
