using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class TimeManager : SingletonManager<TimeManager>
{
    public event Action OnSunrise;  // ���� �̺�Ʈ
    public event Action OnSunset;   // �ϸ� �̺�Ʈ
    private bool wasNight;

    [Header("Time Settings")]
    [SerializeField] private float startHour = 6f;
    [SerializeField][Range(1f, 60f)] private float timeScale = 60f;
    [SerializeField][Range(0f, 24f)] private float currentTime;
    [SerializeField] public float sunriseHour = 6f;
    [SerializeField] public float sunsetHour = 18f;


    private float previousTime;  // Inspector �ð� ���� üũ��
    private CelestialController celestialController;

    public bool IsNight => currentTime < sunriseHour || currentTime > sunsetHour;
    public float CurrentTime => currentTime;
    public float SunriseHour => sunriseHour;
    public float SunsetHour => sunsetHour;
    protected override void Awake()
    {
        // ���� �̱��� üũ
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // �� �ν��Ͻ��� �̱������� ����
        base.Awake();

        // DontDestroyOnLoad ����
        DontDestroyOnLoad(gameObject);

        // ������ �ʱ�ȭ
        currentTime = startHour;
        wasNight = IsNight;
        celestialController = GetComponent<CelestialController>();

        // �� �ε� �̺�Ʈ ����
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void Start()
    {
        previousTime = currentTime;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // �� ������ CelestialPivot ã�Ƽ� ����
        Transform newPivot = GameObject.FindGameObjectWithTag("CelestialPivot")?.transform;
        
        if (newPivot != null)
        {
            celestialController.SetNewPivot(newPivot);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    private void Update()
    {
        // Inspector���� �ð� ���� üũ
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
        previousTime = currentTime; // ���� ���� �Ŀ��� �ð��� ��� �帣����
    }
    

    private void CheckDayNightTransition()
    {
        bool isNight = IsNight;
        if (wasNight != isNight)
        {
            if (isNight)
            {
                OnSunset?.Invoke();  // �ϸ� ����
               
            }
            else
            {
                OnSunrise?.Invoke(); // ���� ����
               
            }
            wasNight = isNight;
        }
    }
   
}