using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;
using UnityEngine.Rendering;

public class TimeManager : SingletonManager<TimeManager>
{
    public event Action OnSunrise;  // 일출 이벤트
    public event Action OnSunset;   // 일몰 이벤트
    private bool wasNight;

    [Header("Time Settings")]
    [SerializeField] private float timeMultiplier = 1f;
    [SerializeField] private float startHour = 6f;
    [SerializeField][Range(1f, 60f)] private float timeScale = 60f;
    [SerializeField][Range(0f, 24f)] private float currentTime;
    [SerializeField] private float sunriseHour = 6f;
    [SerializeField] private float sunsetHour = 18f;

    private float previousTime;  // Inspector 시간 변경 체크용
    private CelestialController celestialController;

    public bool IsNight => currentTime < sunriseHour || currentTime > sunsetHour;
    public float CurrentTime => currentTime;
    protected override void Awake()
    {
        base.Awake();
        currentTime = startHour;
        wasNight = IsNight;
        celestialController = GetComponent<CelestialController>();
    }
    private void Start()
    {
        previousTime = currentTime;
    }


    private void Update()
    {
        // Inspector에서 시간 변경 체크
        if (previousTime != currentTime)
        {
            celestialController.UpdateCelestialBodies(currentTime, sunriseHour, sunsetHour);
            CheckDayNightTransition();
            previousTime = currentTime;
        }

        UpdateTime();
        celestialController.UpdateCelestialBodies(currentTime, sunriseHour, sunsetHour);
        CheckDayNightTransition();
    }

    private void UpdateTime()
    {
        currentTime += (Time.deltaTime / 60f) * timeScale;
        if (currentTime >= 24f) currentTime = 0f;
        previousTime = currentTime; // 수동 조정 후에도 시간이 계속 흐르도록
    }
    

    private void CheckDayNightTransition()
    {
        bool isNight = IsNight;
        if (wasNight != isNight)
        {
            if (isNight)
            {
                OnSunset?.Invoke();  // 일몰 시점
               
            }
            else
            {
                OnSunrise?.Invoke(); // 일출 시점
               
            }
            wasNight = isNight;
        }
    }
   
}