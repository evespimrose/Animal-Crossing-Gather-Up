using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherSystem : MonoBehaviour
{
    [Header("���� ȿ�� ������")]
    [SerializeField] private GameObject rainPrefab;
    [SerializeField] private GameObject snowPrefab;

    [Header("���� ����")]
    [SerializeField] private float weatherCheckInterval = 1800f;  // 30�и��� üũ
    [SerializeField][Range(0f, 100f)] private float precipitationChance = 30f;

    //�׽�Ʈ��
    [SerializeField] private KeyCode rainKey = KeyCode.B;
    [SerializeField] private KeyCode snowKey = KeyCode.N;
    [SerializeField] private KeyCode clearKey = KeyCode.M;

    private GameObject currentRainInstance;
    private GameObject currentSnowInstance;
    private float nextWeatherCheck;
    private SeasonSystem seasonSystem;
    private bool isRaining;
    private bool isSnowing;

    private void Start()
    {
        seasonSystem = FindObjectOfType<SeasonSystem>();
        nextWeatherCheck = TimeManager.Instance.CurrentTime + weatherCheckInterval;
        InitializeWeatherPrefabs();
    }

    public void Initialize(bool rain, bool snow, float nextCheck)
    {
        isRaining = rain;
        isSnowing = snow;
        nextWeatherCheck = nextCheck;

        InitializeWeatherPrefabs();
        UpdateWeatherEffects();
    }

    private void InitializeWeatherPrefabs()
    {
        if (currentRainInstance == null)
            currentRainInstance = Instantiate(rainPrefab, transform);
        if (currentSnowInstance == null)
            currentSnowInstance = Instantiate(snowPrefab, transform);

        currentRainInstance.SetActive(false);
        currentSnowInstance.SetActive(false);
    }

    private void UpdateWeatherEffects()
    {
        if (currentRainInstance != null)
            currentRainInstance.SetActive(isRaining);
        if (currentSnowInstance != null)
            currentSnowInstance.SetActive(isSnowing);
    }

    private void Update()
    {
        if (TimeManager.Instance.CurrentTime >= nextWeatherCheck)
        {
            UpdateWeather();
            nextWeatherCheck = TimeManager.Instance.CurrentTime + weatherCheckInterval;
        }
        //�׽�Ʈ
        if (Input.GetKeyDown(rainKey))
            StartRain();
        else if (Input.GetKeyDown(snowKey))
            StartSnow();
        else if (Input.GetKeyDown(clearKey))
            StopAllWeather();

        // ���� ���� üũ ����
        if (TimeManager.Instance.CurrentTime >= nextWeatherCheck)
        {
            UpdateWeather();
            nextWeatherCheck = TimeManager.Instance.CurrentTime + weatherCheckInterval;
        }
    }
    [Header("���� ���� ����")]
    [SerializeField] private bool manualControl = false;  // ���� ���� ���

    public void StartRain()
    {
        if (!manualControl) return;
        isRaining = true;
        isSnowing = false;
        UpdateWeatherEffects();
    }

    public void StartSnow()
    {
        if (!manualControl) return;
        isRaining = false;
        isSnowing = true;
        UpdateWeatherEffects();
    }

    public void StopAllWeather()
    {
        if (!manualControl) return;
        isRaining = false;
        isSnowing = false;
        UpdateWeatherEffects();
    }

    private void UpdateWeather()
    {
        float random = UnityEngine.Random.value * 100f;
        bool shouldPrecipitate = random <= precipitationChance;

        switch (seasonSystem.CurrentSeason)
        {
            case SeasonSystem.Season.Summer:
                isRaining = shouldPrecipitate;
                isSnowing = false;
                break;

            case SeasonSystem.Season.Winter:
                isRaining = false;
                isSnowing = shouldPrecipitate;
                break;

            default: // ��, ����
                isRaining = shouldPrecipitate && random <= precipitationChance * 0.5f;
                isSnowing = false;
                break;
        }

        UpdateWeatherEffects();
    }

    private void OnDestroy()
    {
        if (currentRainInstance != null)
            Destroy(currentRainInstance);
        if (currentSnowInstance != null)
            Destroy(currentSnowInstance);
    }


    public bool IsRaining => isRaining;
    public bool IsSnowing => isSnowing;
    public float NextWeatherCheck => nextWeatherCheck;






}
