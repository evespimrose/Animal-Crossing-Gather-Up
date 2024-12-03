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
    [SerializeField] private float transitionDuration = 2f; // 전환 시간
    private Material currentSkybox;
    private Coroutine skyboxTransitionCoroutine;


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

        // 초기 스카이박스 설정
        currentSkybox = GetSkyboxForCurrentTime();
        if (currentSkybox != null)
        {
            RenderSettings.skybox = currentSkybox;
        }
    }

    private void Update()
    {
        UpdateTime();
        UpdateEnvironment();
        UpdateSkybox();
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
        rotationDegree = Mathf.Repeat(rotationDegree, 360f);
        directionalLight.transform.rotation = Quaternion.Euler(rotationDegree, 170f, 0f);

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

    private Material GetSkyboxForCurrentTime()
    {
        float hourOfDay = (CurrentTime / 60f);

        if (hourOfDay >= 0 && hourOfDay < 5)
            return dawnSkybox;
        if (hourOfDay >= 5 && hourOfDay < 9)
            return morningSkybox;
        if (hourOfDay >= 9 && hourOfDay < 17)
            return daySkybox;
        if (hourOfDay >= 17 && hourOfDay < 21)
            return eveningSkybox;
        return nightSkybox;
    }
    private void UpdateSkybox()
    {
        Material targetSkybox = GetSkyboxForCurrentTime();

        if (targetSkybox != null && targetSkybox != currentSkybox)
        {
            // 이전 전환이 진행 중이면 중단
            if (skyboxTransitionCoroutine != null)
            {
                StopCoroutine(skyboxTransitionCoroutine);
            }

            // 새로운 전환 시작
            skyboxTransitionCoroutine = StartCoroutine(SmoothSkyboxTransition(targetSkybox));
        }
    }


    private IEnumerator SmoothSkyboxTransition(Material targetSkybox)
    {
        float elapsedTime = 0f;
        Material startSkybox = currentSkybox;

        // 첫 전환인 경우
        if (startSkybox == null)
        {
            RenderSettings.skybox = targetSkybox;
            currentSkybox = targetSkybox;
            yield break;
        }

        // 현재 스카이박스 페이드 아웃
        float startExposure = startSkybox.GetFloat("_Exposure");
        while (elapsedTime < transitionDuration * 0.5f)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / (transitionDuration * 0.5f);

            float exposure = Mathf.Lerp(startExposure, 0f, t);
            startSkybox.SetFloat("_Exposure", exposure);

            yield return null;
        }

        // 타겟 스카이박스로 변경
        RenderSettings.skybox = targetSkybox;
        currentSkybox = targetSkybox;

        // 새 스카이박스 페이드 인
        elapsedTime = 0f;
        while (elapsedTime < transitionDuration * 0.5f)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / (transitionDuration * 0.5f);

            float exposure = Mathf.Lerp(0f, startExposure, t);
            targetSkybox.SetFloat("_Exposure", exposure);

            yield return null;
        }

        skyboxTransitionCoroutine = null;
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
        if (dawnSkybox == null || morningSkybox == null ||
            daySkybox == null || eveningSkybox == null ||
            nightSkybox == null) { }
        
           
        

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
