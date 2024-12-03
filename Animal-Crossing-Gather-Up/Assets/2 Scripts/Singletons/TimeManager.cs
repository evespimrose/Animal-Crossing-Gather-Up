using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class TimeManager : SingletonManager<TimeManager>
{
    public float DayNightCycleDuration = 1440f; 
    public float CurrentTime { get; private set; }

    [SerializeField] private Light directionalLight;
    [SerializeField] private AmbientMode ambientMode = AmbientMode.Skybox;
    [SerializeField] private float nightIntensity = 0.1f;
    [SerializeField] private float dayIntensity = 1f;

    [SerializeField] private Material dawnSkybox;
    [SerializeField] private Material morningSkybox;
    [SerializeField] private Material daySkybox;
    [SerializeField] private Material eveningSkybox;
    [SerializeField] private Material nightSkybox;

    private Material currentSkybox;
    private Material nextSkybox;
    private float blendFactor = 0f;
    private const float SKYBOX_UPDATE_INTERVAL = 0.1f;
    private float lastSkyboxUpdateTime;

    // 옵저버 패턴 구현을 위한 이벤트들
    public event Action OnNightToDayChanged;  // bool: true=낮, false=밤 시간대가 변경될 때 호출
    private TimeOfDay currentTimeOfDay;
    private bool wasNight;
  

    public enum TimeOfDay
    {
        Dawn,    // 새벽 (00:00-04:59)
        Morning, // 아침 (05:00-08:59)
        Day,     // 낮   (09:00-16:59)
        Evening, // 저녁 (17:00-20:59)
        Night    // 밤   (21:00-23:59)
    }

    protected override void Awake()
    {
        base.Awake();
        wasNight = IsNight();
        SetInitialTimeOfDay();
    }
    private void Start()
    {
        if (!directionalLight)
            directionalLight = FindObjectOfType<Light>();

        RenderSettings.ambientMode = ambientMode;
        ValidateSkyboxMaterials();

    }

    private void Update()
    {
        UpdateTime();
        UpdateEnvironment();

        if (Time.time - lastSkyboxUpdateTime >= SKYBOX_UPDATE_INTERVAL)
        {
            UpdateSkybox();
            lastSkyboxUpdateTime = Time.time;
        }

        CheckNightToDayTransition();
        UpdateTimeOfDay();
    }

    private void UpdateTime()
    {
        CurrentTime += Time.deltaTime;
        if (CurrentTime >= DayNightCycleDuration)
            CurrentTime = 0;
                         
    }

    private void UpdateEnvironment()
    {
        // 태양 회전
        float rotationDegree = (CurrentTime / DayNightCycleDuration) * 360f;
        directionalLight.transform.rotation = Quaternion.Euler(rotationDegree, -30f, 0f);

        // 빛 강도 조절
        float timeRatio = CurrentTime / DayNightCycleDuration;
        float intensity = Mathf.Lerp(dayIntensity, nightIntensity,
            Mathf.Abs(Mathf.Cos(timeRatio * Mathf.PI)));
        directionalLight.intensity = intensity;

        // 환경광 조절
        Color ambientColor = IsDay()
            ? Color.white
            : new Color(0.2f, 0.2f, 0.3f);
        RenderSettings.ambientLight = ambientColor;
    }

    private void UpdateSkybox()
    {
        float hourOfDay = (CurrentTime / 60f);

        // 시간대에 따른 스카이박스 전환
        if (hourOfDay >= 0 && hourOfDay < 5)         // 새벽 (0시-5시)
        {
            SetSkyboxTransition(nightSkybox, dawnSkybox, hourOfDay / 5f);
        }
        else if (hourOfDay >= 5 && hourOfDay < 9)    // 아침 (5시-9시)
        {
            SetSkyboxTransition(dawnSkybox, morningSkybox, (hourOfDay - 5) / 4f);
        }
        else if (hourOfDay >= 9 && hourOfDay < 17)   // 낮 (9시-17시)
        {
            SetSkyboxTransition(morningSkybox, daySkybox, (hourOfDay - 9) / 8f);
        }
        else if (hourOfDay >= 17 && hourOfDay < 21)  // 저녁 (17시-21시)
        {
            SetSkyboxTransition(daySkybox, eveningSkybox, (hourOfDay - 17) / 4f);
        }
        else                                         // 밤 (21시-24시)
        {
            SetSkyboxTransition(eveningSkybox, nightSkybox, (hourOfDay - 21) / 3f);
        }

        if (currentSkybox != null && nextSkybox != null)
        {
            RenderSettings.skybox.Lerp(currentSkybox, nextSkybox, blendFactor);
        }
    }

    private void SetSkyboxTransition(Material from, Material to, float blend)
    {
        currentSkybox = from;
        nextSkybox = to;
        blendFactor = Mathf.Clamp01(blend);
    }

    private void CheckNightToDayTransition()
    {
        bool isNight = IsNight();
        if (wasNight && !isNight)  // 밤에서 낮으로 바뀌는 순간
        {
            OnNightToDayChanged?.Invoke();
        }
        wasNight = isNight;
    }
    private void SetInitialTimeOfDay()
    {
        float timeRatio = CurrentTime / DayNightCycleDuration;
        currentTimeOfDay = GetTimeOfDayFromRatio(timeRatio);
    }
    private void UpdateTimeOfDay()
    {
        float timeRatio = CurrentTime / DayNightCycleDuration;
        TimeOfDay newTimeOfDay = GetTimeOfDayFromRatio(timeRatio);

        if (newTimeOfDay != currentTimeOfDay)
        {
            currentTimeOfDay = newTimeOfDay;
        }
    }
    private TimeOfDay GetTimeOfDayFromRatio(float ratio)
    {
        float hourOfDay = ratio * 24f;

        if (hourOfDay >= 0 && hourOfDay < 5) return TimeOfDay.Dawn;    // 새벽
        if (hourOfDay >= 5 && hourOfDay < 9) return TimeOfDay.Morning; // 아침
        if (hourOfDay >= 9 && hourOfDay < 17) return TimeOfDay.Day;    // 낮
        if (hourOfDay >= 17 && hourOfDay < 21) return TimeOfDay.Evening; // 저녁
        return TimeOfDay.Night;  // 밤
    }

    private void ValidateSkyboxMaterials()
    {
        if (dawnSkybox == null || morningSkybox == null || daySkybox == null ||
            eveningSkybox == null || nightSkybox == null) { }
       
    }

    // 현재 밤인지 확인
    private bool IsNight()
    {
        TimeOfDay currentTime = GetCurrentTimeOfDay();
        return currentTime == TimeOfDay.Night || currentTime == TimeOfDay.Dawn;
    }

    
    public bool IsDay()
    {
        float hourOfDay = (CurrentTime / 60f);
        return hourOfDay >= 9 && hourOfDay < 17;  // 9시-17시를 낮으로 간주
    }

    public string GetTimeString()
    {
        float totalHours = (CurrentTime / 60f);
        int hours = (int)totalHours;
        int minutes = (int)((totalHours - hours) * 60);

        return $"{hours:D2}:{minutes:D2}";
    }

    public TimeOfDay GetCurrentTimeOfDay() => currentTimeOfDay;
}
