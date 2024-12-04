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
    [SerializeField] private Transform celestialPivot;    // �ϳ��� ȸ����
    [SerializeField] private Light directionalLight;      // �ϳ��� �𷺼ų� ����Ʈ
    [SerializeField] private GameObject sunPrefab;        // �¾� ���־�
    [SerializeField] private GameObject moonPrefab;       // �� ���־�

    [Header("Light Settings")]
    [SerializeField] private float sunriseHour = 6f;
    [SerializeField] private float sunsetHour = 18f;
    [SerializeField] private float maxSunIntensity = 1f;
    [SerializeField] private float maxMoonIntensity = 0.5f;
    [SerializeField] private Color sunColor = Color.white;
    [SerializeField] private Color moonColor = new Color(0.6f, 0.6f, 0.8f, 1f);
    
    private GameObject sunObject;    // ������ �¾� ������Ʈ
    private GameObject moonObject;   // ������ �� ������Ʈ
    private float currentTime;       // ���� �ð�
  

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
        if (!directionalLight)
        {
            Debug.LogError("TimeManager: Directional Light not assigned!");
            return;
        }
        UpdateEnvironment();
    }

    private void InitializeCelestialBodies()
    {
        if (celestialPivot)
        {
            if (sunPrefab)
            {
                sunObject = Instantiate(sunPrefab, celestialPivot);
                sunObject.transform.localPosition = Vector3.forward * 20f;
            }
            if (moonPrefab)
            {
                moonObject = Instantiate(moonPrefab, celestialPivot);
                moonObject.transform.localPosition = Vector3.forward * 20f;
            }
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

    

    private void UpdateEnvironment()
    {
        float rotation = 0f;

        if (currentTime >= sunriseHour && currentTime <= sunsetHour) // ��
        {
            // �ذ� ����(0��)���� ����(180��)����
            float dayDuration = sunsetHour - sunriseHour;  // ���� ����
            rotation = -(currentTime - sunriseHour) * 180f / dayDuration;

            if (celestialPivot)
            {
                celestialPivot.rotation = Quaternion.Euler(rotation, 0f, 0f);
                if (sunObject) sunObject.SetActive(true);
                if (moonObject) moonObject.SetActive(false);
            }
        }
        else // ��
        {

            float nightDuration = 24f - sunsetHour + sunriseHour; // ���� �� ����
            float nightTime;

            if (currentTime > sunsetHour)
            {
                nightTime = currentTime - sunsetHour;
            }
            else
            {
                nightTime = currentTime + (24f - sunsetHour);
            }

            rotation = -(nightTime * 180f / nightDuration);

            if (celestialPivot)
            {
                celestialPivot.rotation = Quaternion.Euler(rotation, 0f, 0f);
                if (sunObject) sunObject.SetActive(false);
                if (moonObject) moonObject.SetActive(true);
            }
        }
        // �� ������ ���� ������Ʈ
        float lightIntensity = 0f;
        Color lightColor;

        if (currentTime >= sunriseHour && currentTime <= sunsetHour)
        {
            if (currentTime <= sunriseHour + 2f) // ����
            {
                float t = (currentTime - sunriseHour) / 2f;
                lightIntensity = Mathf.Lerp(maxMoonIntensity, maxSunIntensity, t);
                lightColor = Color.Lerp(moonColor, sunColor, t);
            }
            else if (currentTime >= sunsetHour - 2f) // �ϸ�
            {
                float t = (currentTime - (sunsetHour - 2f)) / 2f;
                lightIntensity = Mathf.Lerp(maxSunIntensity, maxMoonIntensity, t);
                lightColor = Color.Lerp(sunColor, moonColor, t);

                // �ϸ� �� �ʱ� ��ġ�� ����
                if (t >= 1f && celestialPivot)
                {
                    celestialPivot.rotation = Quaternion.Euler(0f, 0f, 0f);
                }
            }
            else // ��
            {
                lightIntensity = maxSunIntensity;
                lightColor = sunColor;
            }
        }
        else // ��
        {
            lightIntensity = maxMoonIntensity;
            lightColor = moonColor;
            RenderSettings.ambientLight = moonColor * 1.2f;
        }

        // �𷺼ų� ����Ʈ ������Ʈ
        if (directionalLight)
        {
            directionalLight.intensity = lightIntensity;
            directionalLight.color = lightColor;
        }

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
 

    private void OnDestroy()
    {
        if (sunObject) Destroy(sunObject);
        if (moonObject) Destroy(moonObject);
    }

   

}