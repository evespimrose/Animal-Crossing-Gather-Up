using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherSystem : MonoBehaviour
{
    [Header("³¯¾¾ È¿°ú")]
    [SerializeField] private ParticleSystem rainEffect;
    [SerializeField] private ParticleSystem snowEffect;

    [Header("³¯¾¾ ¼³Á¤")]
    [SerializeField] private float weatherCheckInterval = 1800f;  // 30ºÐ¸¶´Ù Ã¼Å©
    [SerializeField][Range(0f, 100f)] private float precipitationChance = 30f;

    private float nextWeatherCheck;
    private SeasonSystem seasonSystem;

    private void Start()
    {
        seasonSystem = FindObjectOfType<SeasonSystem>();
        nextWeatherCheck = TimeManager.Instance.CurrentTime + weatherCheckInterval;
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
                rainEffect.gameObject.SetActive(random <= precipitationChance);
                snowEffect.gameObject.SetActive(false);
                break;

            case SeasonSystem.Season.Winter:
                snowEffect.gameObject.SetActive(random <= precipitationChance);
                rainEffect.gameObject.SetActive(false);
                break;

            default: // º½, °¡À»
                rainEffect.gameObject.SetActive(random <= precipitationChance * 0.5f);
                snowEffect.gameObject.SetActive(false);
                break;
        }
    }
}
