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
        // �¾� ȸ��
        float rotationDegree = (CurrentTime / DayNightCycleDuration) * 360f;
        directionalLight.transform.rotation = Quaternion.Euler(rotationDegree, -30f, 0f);

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

    private void UpdateSkybox()
    {
        float hourOfDay = (CurrentTime / 60f);

        // �ð��뿡 ���� ��ī�̹ڽ� ��ȯ
        if (hourOfDay >= 0 && hourOfDay < 5)         // ���� (0��-5��)
        {
            SetSkyboxTransition(nightSkybox, dawnSkybox, hourOfDay / 5f);
        }
        else if (hourOfDay >= 5 && hourOfDay < 9)    // ��ħ (5��-9��)
        {
            SetSkyboxTransition(dawnSkybox, morningSkybox, (hourOfDay - 5) / 4f);
        }
        else if (hourOfDay >= 9 && hourOfDay < 17)   // �� (9��-17��)
        {
            SetSkyboxTransition(morningSkybox, daySkybox, (hourOfDay - 9) / 8f);
        }
        else if (hourOfDay >= 17 && hourOfDay < 21)  // ���� (17��-21��)
        {
            SetSkyboxTransition(daySkybox, eveningSkybox, (hourOfDay - 17) / 4f);
        }
        else                                         // �� (21��-24��)
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
        if (dawnSkybox == null || morningSkybox == null || daySkybox == null ||
            eveningSkybox == null || nightSkybox == null) { }
       
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
