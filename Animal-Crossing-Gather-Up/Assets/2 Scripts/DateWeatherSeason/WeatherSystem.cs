using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherSystem : MonoBehaviour
{
    [Header("³¯¾¾ È¿°ú ÇÁ¸®ÆÕ")]
    [SerializeField] private GameObject rainPrefab;
    [SerializeField] private GameObject snowPrefab;

    [Header("³¯¾¾ ¼³Á¤")]
    [SerializeField] private float weatherCheckInterval = 1800f;  // 30ºÐ¸¶´Ù Ã¼Å©
    [SerializeField][Range(0f, 100f)] private float precipitationChance = 30f;

    private float nextWeatherCheck;
    private SeasonSystem seasonSystem;

    private GameObject currentRainInstance;
    private GameObject currentSnowInstance;

    private void Start()
    {
        seasonSystem = FindObjectOfType<SeasonSystem>();
        nextWeatherCheck = TimeManager.Instance.CurrentTime + weatherCheckInterval;
    
        currentRainInstance = Instantiate(rainPrefab, transform);
        currentSnowInstance = Instantiate(snowPrefab, transform);
        currentRainInstance.SetActive(false);
        currentSnowInstance.SetActive(false);
    }

    private void Update()
    {
        if (TimeManager.Instance.CurrentTime >= nextWeatherCheck)
        {
            UpdateWeather();
            nextWeatherCheck = TimeManager.Instance.CurrentTime + weatherCheckInterval;
        }
    }

    private void UpdateWeather()
    {
        float random = UnityEngine.Random.value * 100f;

        switch (seasonSystem.CurrentSeason)
        {
            case SeasonSystem.Season.Summer:
                currentRainInstance.SetActive(random <= precipitationChance);
                currentSnowInstance.SetActive(false);
                break;

            case SeasonSystem.Season.Winter:
                currentSnowInstance.SetActive(random <= precipitationChance);
                currentRainInstance.SetActive(false);
                break;

            default: // º½, °¡À»
                currentRainInstance.SetActive(random <= precipitationChance * 0.5f);
                currentSnowInstance.SetActive(false);
                break;
        }
    }
}
