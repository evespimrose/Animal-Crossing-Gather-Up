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
    public event Action<float> OnTimeChanged;
    public event Action OnSunrise;  // 일출 이벤트
    public event Action OnSunset;   // 일몰 이벤트
    private bool wasNight;

    [Header("Time Settings")]
    [SerializeField] private float startHour = 6f;
    [SerializeField][Range(1f, 60f)] private float timeScale = 60f;
    [SerializeField][Range(0f, 24f)] private float currentTime;
    [SerializeField] public float sunriseHour = 6f;
    [SerializeField] public float sunsetHour = 18f;


    private float previousTime;  // Inspector 시간 변경 체크용
    private CelestialController celestialController;

    public bool IsNight => currentTime < sunriseHour || currentTime > sunsetHour;
    public float CurrentTime => currentTime;
    public float SunriseHour => sunriseHour;
    public float SunsetHour => sunsetHour;
    protected override void Awake()
    {
        // 먼저 싱글톤 체크
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // 이 인스턴스를 싱글톤으로 설정
        base.Awake();

        // DontDestroyOnLoad 설정
        DontDestroyOnLoad(gameObject);

        // 나머지 초기화
        currentTime = startHour;
        wasNight = IsNight;
        celestialController = GetComponent<CelestialController>();

        // 씬 로드 이벤트 구독
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    private void Update()
    {
        // 씬 전환 중에는 시간 업데이트 건너뛰기
        if (Time.timeScale == 0f)
            return;

        // Inspector에서 시간 변경 체크
        if (previousTime != currentTime)
        {
            celestialController.UpdateCelestialBodies(currentTime, sunriseHour, sunsetHour);
            OnTimeChanged?.Invoke(currentTime);
            previousTime = currentTime;
        }

        // 일반 시간 업데이트
        UpdateTime();
        celestialController.UpdateCelestialBodies(currentTime, sunriseHour, sunsetHour);

        if (Time.timeScale == 0f || GameManager.Instance == null || !GameManager.Instance.enabled)
            return;

        // GameScene이나 MileIsland일 때만 CheckDayNightTransition 실행
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "GameScene" || currentScene == "MileIsland")
        {
            CheckDayNightTransition();
        }

        OnTimeChanged?.Invoke(currentTime);
    }
    private void Start()
    {
        previousTime = currentTime;
    }
    #region Scene Management
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 새 씬에서 CelestialPivot 찾아서 연결
        Transform newPivot = GameObject.FindGameObjectWithTag("CelestialPivot")?.transform;
        
        if (newPivot != null)
        {
            celestialController.SetNewPivot(newPivot);
        }
    }
    #endregion  
    #region Time Management
    private void UpdateTime()
    {
        currentTime += (Time.deltaTime / 60f) * timeScale;
        if (currentTime >= 24f) currentTime = 0f;
        previousTime = currentTime; // 수동 조정 후에도 시간이 계속 흐르도록
    }

    public void AddHours(float hours)
    {
        currentTime += hours;
        if (currentTime >= 24f)
        {
            currentTime -= 24f;
        }
        celestialController.UpdateCelestialBodies(currentTime, sunriseHour, sunsetHour);
        OnTimeChanged?.Invoke(currentTime);
    }
    #endregion
    #region Day/Night Cycle
    private void CheckDayNightTransition()
    {
        bool isNight = IsNight;
        if (wasNight == isNight) 
            return;

        wasNight = isNight;  

        if (isNight)
        {
            OnSunset?.Invoke();
        }
        else
        {
            OnSunrise?.Invoke();
            // GameManager가 준비되었을 때만 RefillCollectables 실행
            if (GameManager.Instance != null && GameManager.Instance.enabled)
            {
                GameManager.Instance.RefillCollactables();
            }
        }
    }
    #endregion

}