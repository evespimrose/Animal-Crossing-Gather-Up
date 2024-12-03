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
    [SerializeField] private float transitionDuration = 2f; // ��ȯ �ð�
    private Material currentSkybox;
    private Coroutine skyboxTransitionCoroutine;


    // ������ ���� ������ ���� �̺�Ʈ��
    public event Action OnNightToDayChanged;  // bool: true=��, false=�� �ð��밡 ����� �� ȣ��
    private TimeOfDay currentTimeOfDay;
    private bool wasNight;
  

    public enum TimeOfDay
    {
        Dawn,    // ���� (00:00-04:59)
        Morning, // ��ħ (05:00-08:59)
        Day,     // ��   (09:00-16:59)
        Evening, // ���� (17:00-20:59)
        Night    // ��   (21:00-23:59)
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

        // �ʱ� ��ī�̹ڽ� ����
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
        // �¾� ȸ��
        float rotationDegree = (CurrentTime / DayNightCycleDuration) * 360f;
        rotationDegree = Mathf.Repeat(rotationDegree, 360f);
        directionalLight.transform.rotation = Quaternion.Euler(rotationDegree, 170f, 0f);

        // �� ���� ����
        float timeRatio = CurrentTime / DayNightCycleDuration;
        float intensity = Mathf.Lerp(dayIntensity, nightIntensity,
            Mathf.Abs(Mathf.Cos(timeRatio * Mathf.PI)));
        directionalLight.intensity = intensity;

        // ȯ�汤 ����
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
            // ���� ��ȯ�� ���� ���̸� �ߴ�
            if (skyboxTransitionCoroutine != null)
            {
                StopCoroutine(skyboxTransitionCoroutine);
            }

            // ���ο� ��ȯ ����
            skyboxTransitionCoroutine = StartCoroutine(SmoothSkyboxTransition(targetSkybox));
        }
    }


    private IEnumerator SmoothSkyboxTransition(Material targetSkybox)
    {
        float elapsedTime = 0f;
        Material startSkybox = currentSkybox;

        // ù ��ȯ�� ���
        if (startSkybox == null)
        {
            RenderSettings.skybox = targetSkybox;
            currentSkybox = targetSkybox;
            yield break;
        }

        // ���� ��ī�̹ڽ� ���̵� �ƿ�
        float startExposure = startSkybox.GetFloat("_Exposure");
        while (elapsedTime < transitionDuration * 0.5f)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / (transitionDuration * 0.5f);

            float exposure = Mathf.Lerp(startExposure, 0f, t);
            startSkybox.SetFloat("_Exposure", exposure);

            yield return null;
        }

        // Ÿ�� ��ī�̹ڽ��� ����
        RenderSettings.skybox = targetSkybox;
        currentSkybox = targetSkybox;

        // �� ��ī�̹ڽ� ���̵� ��
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
        if (wasNight && !isNight)  // �㿡�� ������ �ٲ�� ����
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

        if (hourOfDay >= 0 && hourOfDay < 5) return TimeOfDay.Dawn;    // ����
        if (hourOfDay >= 5 && hourOfDay < 9) return TimeOfDay.Morning; // ��ħ
        if (hourOfDay >= 9 && hourOfDay < 17) return TimeOfDay.Day;    // ��
        if (hourOfDay >= 17 && hourOfDay < 21) return TimeOfDay.Evening; // ����
        return TimeOfDay.Night;  // ��
    }

    private void ValidateSkyboxMaterials()
    {
        if (dawnSkybox == null || morningSkybox == null ||
            daySkybox == null || eveningSkybox == null ||
            nightSkybox == null) { }
        
           
        

    }

    // ���� ������ Ȯ��
    private bool IsNight()
    {
        TimeOfDay currentTime = GetCurrentTimeOfDay();
        return currentTime == TimeOfDay.Night || currentTime == TimeOfDay.Dawn;
    }

    
    public bool IsDay()
    {
        float hourOfDay = (CurrentTime / 60f);
        return hourOfDay >= 9 && hourOfDay < 17;  // 9��-17�ø� ������ ����
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
