using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class TimeManager : SingletonManager<TimeManager>
{
    public event Action OnDayNightChanged;
    private bool wasNight;

    [Header("Time Settings")]
    [SerializeField] private float timeMultiplier = 60f;  // 1분(현실) = 1시간(게임)
    [SerializeField] private float startHour = 6f;        // 시작 시간
    [SerializeField][Range(1f, 20f)] private float timeScale = 1f;  // 시간 배속 (1~20배)

    [Header("Celestial Bodies")]
    [SerializeField] private Transform sunPivot;          // 태양 피봇
    [SerializeField] private Transform moonPivot;         // 달 피봇
    [SerializeField] private Light sunLight;              // 태양 광원
    [SerializeField] private Light moonLight;             // 달 광원
    [SerializeField] private SpriteRenderer sunRenderer;  // 태양 스프라이트
    [SerializeField] private SpriteRenderer moonRenderer; // 달 스프라이트

    [Header("Light Settings")]
    [SerializeField] private float sunriseHour = 6f;      // 일출 시간
    [SerializeField] private float sunsetHour = 18f;      // 일몰 시간
    [SerializeField] private float maxSunIntensity = 1f;  // 태양 최대 광도
    [SerializeField] private float maxMoonIntensity = 0.3f; // 달 최대 광도
    [SerializeField] private Color sunColor = Color.white;
    [SerializeField] private Color moonColor = new Color(0.6f, 0.6f, 0.8f);

    private float currentTime;    // 현재 시간 (시간)

    public bool IsNight => currentTime < sunriseHour || currentTime > sunsetHour;

    protected override void Awake()
    {
        base.Awake();
        currentTime = startHour;
        wasNight = IsNight;
        InitializeCelestialBodies();
    }

    private void InitializeCelestialBodies()
    {
        // 태양과 달의 초기 설정
        sunLight.intensity = 0;
        moonLight.intensity = 0;

        // 스프라이트 설정
        if (sunRenderer)
        {
            sunRenderer.color = sunColor;
            sunRenderer.transform.localScale = Vector3.one * 10f;  // 크기 조절
        }

        if (moonRenderer)
        {
            moonRenderer.color = moonColor;
            moonRenderer.transform.localScale = Vector3.one * 8f;  // 크기 조절
        }
    }

    private void Update()
    {
        UpdateTime();
        UpdateCelestialBodies();
        CheckDayNightTransition();
    }

    private void UpdateTime()
    {
        currentTime += Time.deltaTime * timeMultiplier * timeScale;
        if (currentTime >= 24f) currentTime = 0f;
    }

    private void UpdateCelestialBodies()
    {
        // 태양과 달의 회전 (180도는 지평선 아래)
        float sunRotation = (currentTime - sunriseHour) * 180f / (sunsetHour - sunriseHour);
        float moonRotation = sunRotation + 180f;  // 달은 태양의 반대편

        // 피봇 회전
        sunPivot.rotation = Quaternion.Euler(sunRotation, 170f, 0f);
        moonPivot.rotation = Quaternion.Euler(moonRotation, 170f, 0f);

        // 광원 강도 계산
        float sunIntensity = 0f;
        float moonIntensity = 0f;

        if (currentTime >= sunriseHour && currentTime <= sunsetHour)
        {
            // 낮 시간
            if (currentTime <= sunriseHour + 2f) // 일출
            {
                float t = (currentTime - sunriseHour) / 2f;
                sunIntensity = Mathf.Lerp(0f, maxSunIntensity, t);
                moonIntensity = Mathf.Lerp(maxMoonIntensity, 0f, t);
            }
            else if (currentTime >= sunsetHour - 2f) // 일몰
            {
                float t = (currentTime - (sunsetHour - 2f)) / 2f;
                sunIntensity = Mathf.Lerp(maxSunIntensity, 0f, t);
                moonIntensity = Mathf.Lerp(0f, maxMoonIntensity, t);
            }
            else // 낮
            {
                sunIntensity = maxSunIntensity;
                moonIntensity = 0f;
            }
        }
        else // 밤
        {
            sunIntensity = 0f;
            moonIntensity = maxMoonIntensity;
        }

        // 광원 업데이트
        sunLight.intensity = sunIntensity;
        moonLight.intensity = moonIntensity;

        // 스프라이트 알파값 업데이트
        if (sunRenderer)
        {
            Color sunColor = sunRenderer.color;
            sunColor.a = sunIntensity;
            sunRenderer.color = sunColor;
        }

        if (moonRenderer)
        {
            Color moonColor = moonRenderer.color;
            moonColor.a = moonIntensity * 2f; // 달은 좀 더 잘 보이게
            moonRenderer.color = moonColor;
        }

        // 환경광 업데이트
        RenderSettings.ambientLight = Color.Lerp(moonColor * moonIntensity, sunColor * sunIntensity, sunIntensity);
    }

    private void CheckDayNightTransition()
    {
        bool isNight = IsNight;
        if (wasNight != isNight)
        {
            wasNight = isNight;
            OnDayNightChanged?.Invoke();
        }
    }

    public string GetTimeString()
    {
        int hours = Mathf.FloorToInt(currentTime);
        int minutes = Mathf.FloorToInt((currentTime - hours) * 60f);
        return $"{hours:00}:{minutes:00}";
    }

}