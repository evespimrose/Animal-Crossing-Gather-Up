using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;
using UnityEngine.Rendering;

public class TimeManager : SingletonManager<TimeManager>
{
    public event Action OnDayNightChanged;
    private bool wasNight;

    [Header("Time Settings")]
    [SerializeField] private float timeMultiplier = 1f;  // 1분(현실) = 1시간(게임)
    [SerializeField] private float startHour = 6f;        // 시작 시간
    [SerializeField][Range(1f, 60f)] private float timeScale = 60f;  // 시간 배속

    [Header("Celestial Bodies")]
    [SerializeField] private Transform sunPivot;          // 태양 회전축
    [SerializeField] private Transform moonPivot;         // 달 회전축
    [SerializeField] private Light sunLight;              // 태양 광원
    [SerializeField] private Light moonLight;             // 달 광원
    [SerializeField] private GameObject sunPrefab;        // 태양 프리팹
    [SerializeField] private GameObject moonPrefab;       // 달 프리팹

    [Header("Light Settings")]
    [SerializeField] private float sunriseHour = 6f;      // 일출 시간
    [SerializeField] private float sunsetHour = 18f;      // 일몰 시간
    [SerializeField] private float maxSunIntensity = 1f;  // 태양 최대 밝기
    [SerializeField] private float maxMoonIntensity = 0.3f; // 달 최대 밝기
    [SerializeField] private Color sunColor = Color.white;
    [SerializeField] private Color moonColor = new Color(0.6f, 0.6f, 0.8f);

    private GameObject sunObject;    // 생성된 태양 오브젝트
    private GameObject moonObject;   // 생성된 달 오브젝트
    private float currentTime;       // 현재 시간

    private Light sunPointLight;
    private Light moonPointLight;

    public bool IsNight => currentTime < sunriseHour || currentTime > sunsetHour;
    public float CurrentTime => currentTime;

    protected override void Awake()
    {
        base.Awake();
        currentTime = startHour;
        wasNight = IsNight;
        InitializeCelestialBodies();
    }

    private void Start()
    {
        if (!sunLight || !moonLight)
        {
            Debug.LogError("TimeManager: Lights not assigned!");
            return;
        }
        UpdateEnvironment();
    }

    private void InitializeCelestialBodies()
    {
        if (sunPrefab && !sunObject && sunPivot)
        {
            sunObject = Instantiate(sunPrefab, sunPivot);
            sunObject.transform.localPosition = Vector3.forward * 20f;
            sunPointLight = sunObject.GetComponentInChildren<Light>();
        }

        if (moonPrefab && !moonObject && moonPivot)
        {
            moonObject = Instantiate(moonPrefab, moonPivot);
            moonObject.transform.localPosition = Vector3.forward * 20f;
            moonPointLight = moonObject.GetComponentInChildren<Light>();
        }
    }

    private void Update()
    {
        UpdateTime();
        UpdateEnvironment();
        CheckDayNightTransition();
    }

    private void UpdateTime()
    {
        currentTime += (Time.deltaTime / 60f) * timeScale;
        if (currentTime >= 24f) currentTime = 0f;
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

    private void UpdateEnvironment()
    {
        float sunRotation = -(currentTime - sunriseHour) * 180f / (sunsetHour - sunriseHour);
        float moonRotation = sunRotation + 180f;

        // Y축 회전값을 -90으로 변경하여 동쪽에서 시작하도록 수정
        if (sunPivot) sunPivot.rotation = Quaternion.Euler(sunRotation, 0f, 0f);
        if (moonPivot) moonPivot.rotation = Quaternion.Euler(moonRotation, 0f, 0f);

        // 광원 강도 계산
        float sunIntensity = 0f;
        float moonIntensity = 0f;

        if (sunPointLight) sunPointLight.intensity = sunIntensity * 2f;
        if (moonPointLight) moonPointLight.intensity = moonIntensity * 1.5f;

       


            if (currentTime >= sunriseHour && currentTime <= sunsetHour)
        {
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
        if (sunLight) sunLight.intensity = sunIntensity;
        if (moonLight) moonLight.intensity = moonIntensity;
        
        // 환경광 업데이트
        RenderSettings.ambientLight = Color.Lerp(moonColor * moonIntensity, sunColor * sunIntensity, sunIntensity);
    }

    public string GetTimeString()
    {
        int hours = Mathf.FloorToInt(currentTime);
        int minutes = Mathf.FloorToInt((currentTime - hours) * 60f);
        return $"{hours:00}:{minutes:00}";
    }

    private void OnDestroy()
    {
        if (sunObject) Destroy(sunObject);
        if (moonObject) Destroy(moonObject);
    }

    // 시간 설정 메서드 (디버그나 게임 진행용)
    public void SetTime(float hour)
    {
        currentTime = Mathf.Clamp(hour, 0f, 24f);
        UpdateEnvironment();
    }

    public void SetTimeScale(float scale)
    {
        timeScale = Mathf.Clamp(scale, 1f, 20f);
    }

}