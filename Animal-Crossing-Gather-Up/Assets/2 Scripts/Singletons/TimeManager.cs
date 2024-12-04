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
    [SerializeField] private Transform celestialPivot;    // 하나의 회전축
    [SerializeField] private Light directionalLight;      // 하나의 디렉셔널 라이트
    [SerializeField] private GameObject sunPrefab;        // 태양 비주얼
    [SerializeField] private GameObject moonPrefab;       // 달 비주얼

    [Header("Light Settings")]
    [SerializeField] private float sunriseHour = 6f;
    [SerializeField] private float sunsetHour = 18f;
    [SerializeField] private float maxSunIntensity = 1f;
    [SerializeField] private float maxMoonIntensity = 0.5f;
    [SerializeField] private Color sunColor = Color.white;
    [SerializeField] private Color moonColor = new Color(0.6f, 0.6f, 0.8f, 1f);
    
    private GameObject sunObject;    // 생성된 태양 오브젝트
    private GameObject moonObject;   // 생성된 달 오브젝트
    private float currentTime;       // 현재 시간
  

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

        if (currentTime >= sunriseHour && currentTime <= sunsetHour) // 낮
        {
            // 해가 동쪽(0도)에서 서쪽(180도)으로
            float dayDuration = sunsetHour - sunriseHour;  // 낮의 길이
            rotation = -(currentTime - sunriseHour) * 180f / dayDuration;

            if (celestialPivot)
            {
                celestialPivot.rotation = Quaternion.Euler(rotation, 0f, 0f);
                if (sunObject) sunObject.SetActive(true);
                if (moonObject) moonObject.SetActive(false);
            }
        }
        else // 밤
        {

            float nightDuration = 24f - sunsetHour + sunriseHour; // 밤의 총 길이
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
        // 빛 강도와 색상 업데이트
        float lightIntensity = 0f;
        Color lightColor;

        if (currentTime >= sunriseHour && currentTime <= sunsetHour)
        {
            if (currentTime <= sunriseHour + 2f) // 일출
            {
                float t = (currentTime - sunriseHour) / 2f;
                lightIntensity = Mathf.Lerp(maxMoonIntensity, maxSunIntensity, t);
                lightColor = Color.Lerp(moonColor, sunColor, t);
            }
            else if (currentTime >= sunsetHour - 2f) // 일몰
            {
                float t = (currentTime - (sunsetHour - 2f)) / 2f;
                lightIntensity = Mathf.Lerp(maxSunIntensity, maxMoonIntensity, t);
                lightColor = Color.Lerp(sunColor, moonColor, t);

                // 일몰 시 초기 위치로 리셋
                if (t >= 1f && celestialPivot)
                {
                    celestialPivot.rotation = Quaternion.Euler(0f, 0f, 0f);
                }
            }
            else // 낮
            {
                lightIntensity = maxSunIntensity;
                lightColor = sunColor;
            }
        }
        else // 밤
        {
            lightIntensity = maxMoonIntensity;
            lightColor = moonColor;
            RenderSettings.ambientLight = moonColor * 1.2f;
        }

        // 디렉셔널 라이트 업데이트
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