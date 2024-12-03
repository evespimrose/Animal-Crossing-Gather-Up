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
    [SerializeField] private float timeMultiplier = 1f;  // 1��(����) = 1�ð�(����)
    [SerializeField] private float startHour = 6f;        // ���� �ð�
    [SerializeField][Range(1f, 60f)] private float timeScale = 60f;  // �ð� ���

    [Header("Celestial Bodies")]
    [SerializeField] private Transform sunPivot;          // �¾� ȸ����
    [SerializeField] private Transform moonPivot;         // �� ȸ����
    [SerializeField] private Light sunLight;              // �¾� ����
    [SerializeField] private Light moonLight;             // �� ����
    [SerializeField] private GameObject sunPrefab;        // �¾� ������
    [SerializeField] private GameObject moonPrefab;       // �� ������

    [Header("Light Settings")]
    [SerializeField] private float sunriseHour = 6f;      // ���� �ð�
    [SerializeField] private float sunsetHour = 18f;      // �ϸ� �ð�
    [SerializeField] private float maxSunIntensity = 1f;  // �¾� �ִ� ���
    [SerializeField] private float maxMoonIntensity = 0.3f; // �� �ִ� ���
    [SerializeField] private Color sunColor = Color.white;
    [SerializeField] private Color moonColor = new Color(0.6f, 0.6f, 0.8f);

    private GameObject sunObject;    // ������ �¾� ������Ʈ
    private GameObject moonObject;   // ������ �� ������Ʈ
    private float currentTime;       // ���� �ð�

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

        // Y�� ȸ������ -90���� �����Ͽ� ���ʿ��� �����ϵ��� ����
        if (sunPivot) sunPivot.rotation = Quaternion.Euler(sunRotation, 0f, 0f);
        if (moonPivot) moonPivot.rotation = Quaternion.Euler(moonRotation, 0f, 0f);

        // ���� ���� ���
        float sunIntensity = 0f;
        float moonIntensity = 0f;

        if (sunPointLight) sunPointLight.intensity = sunIntensity * 2f;
        if (moonPointLight) moonPointLight.intensity = moonIntensity * 1.5f;

       


            if (currentTime >= sunriseHour && currentTime <= sunsetHour)
        {
            if (currentTime <= sunriseHour + 2f) // ����
            {
                float t = (currentTime - sunriseHour) / 2f;
                sunIntensity = Mathf.Lerp(0f, maxSunIntensity, t);
                moonIntensity = Mathf.Lerp(maxMoonIntensity, 0f, t);
            }
            else if (currentTime >= sunsetHour - 2f) // �ϸ�
            {
                float t = (currentTime - (sunsetHour - 2f)) / 2f;
                sunIntensity = Mathf.Lerp(maxSunIntensity, 0f, t);
                moonIntensity = Mathf.Lerp(0f, maxMoonIntensity, t);
            }
            else // ��
            {
                sunIntensity = maxSunIntensity;
                moonIntensity = 0f;
            }
        }
        else // ��
        {
            sunIntensity = 0f;
            moonIntensity = maxMoonIntensity;
        }

        // ���� ������Ʈ
        if (sunLight) sunLight.intensity = sunIntensity;
        if (moonLight) moonLight.intensity = moonIntensity;
        
        // ȯ�汤 ������Ʈ
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

    // �ð� ���� �޼��� (����׳� ���� �����)
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