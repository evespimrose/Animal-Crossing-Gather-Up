using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class TimeManager : SingletonManager<TimeManager>
{
    public event Action OnDayNightChanged;
    private bool wasNight;

    [Header("Time Settings")]
    [SerializeField] private float timeMultiplier = 60f;  // 1��(����) = 1�ð�(����)
    [SerializeField] private float startHour = 6f;        // ���� �ð�
    [SerializeField][Range(1f, 20f)] private float timeScale = 1f;  // �ð� ��� (1~20��)

    [Header("Celestial Bodies")]
    [SerializeField] private Transform sunPivot;          // �¾� �Ǻ�
    [SerializeField] private Transform moonPivot;         // �� �Ǻ�
    [SerializeField] private Light sunLight;              // �¾� ����
    [SerializeField] private Light moonLight;             // �� ����
    [SerializeField] private SpriteRenderer sunRenderer;  // �¾� ��������Ʈ
    [SerializeField] private SpriteRenderer moonRenderer; // �� ��������Ʈ

    [Header("Light Settings")]
    [SerializeField] private float sunriseHour = 6f;      // ���� �ð�
    [SerializeField] private float sunsetHour = 18f;      // �ϸ� �ð�
    [SerializeField] private float maxSunIntensity = 1f;  // �¾� �ִ� ����
    [SerializeField] private float maxMoonIntensity = 0.3f; // �� �ִ� ����
    [SerializeField] private Color sunColor = Color.white;
    [SerializeField] private Color moonColor = new Color(0.6f, 0.6f, 0.8f);

    private float currentTime;    // ���� �ð� (�ð�)

    public bool IsNight => currentTime < sunriseHour || currentTime > sunsetHour;

    protected override void Awake()
    {
        base.Awake();
        currentTime = startHour;
        wasNight = IsNight;
        InitializeCelestialBodies();
    }

    private void InitializeCelestialBodies()
    {
        // �¾�� ���� �ʱ� ����
        sunLight.intensity = 0;
        moonLight.intensity = 0;

        // ��������Ʈ ����
        if (sunRenderer)
        {
            sunRenderer.color = sunColor;
            sunRenderer.transform.localScale = Vector3.one * 10f;  // ũ�� ����
        }

        if (moonRenderer)
        {
            moonRenderer.color = moonColor;
            moonRenderer.transform.localScale = Vector3.one * 8f;  // ũ�� ����
        }
    }

    private void Update()
    {
        UpdateTime();
        UpdateCelestialBodies();
        CheckDayNightTransition();
    }

    private void UpdateTime()
    {
        currentTime += Time.deltaTime * timeMultiplier * timeScale;
        if (currentTime >= 24f) currentTime = 0f;
    }

    private void UpdateCelestialBodies()
    {
        // �¾�� ���� ȸ�� (180���� ���� �Ʒ�)
        float sunRotation = (currentTime - sunriseHour) * 180f / (sunsetHour - sunriseHour);
        float moonRotation = sunRotation + 180f;  // ���� �¾��� �ݴ���

        // �Ǻ� ȸ��
        sunPivot.rotation = Quaternion.Euler(sunRotation, 170f, 0f);
        moonPivot.rotation = Quaternion.Euler(moonRotation, 170f, 0f);

        // ���� ���� ���
        float sunIntensity = 0f;
        float moonIntensity = 0f;

        if (currentTime >= sunriseHour && currentTime <= sunsetHour)
        {
            // �� �ð�
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
        sunLight.intensity = sunIntensity;
        moonLight.intensity = moonIntensity;

        // ��������Ʈ ���İ� ������Ʈ
        if (sunRenderer)
        {
            Color sunColor = sunRenderer.color;
            sunColor.a = sunIntensity;
            sunRenderer.color = sunColor;
        }

        if (moonRenderer)
        {
            Color moonColor = moonRenderer.color;
            moonColor.a = moonIntensity * 2f; // ���� �� �� �� ���̰�
            moonRenderer.color = moonColor;
        }

        // ȯ�汤 ������Ʈ
        RenderSettings.ambientLight = Color.Lerp(moonColor * moonIntensity, sunColor * sunIntensity, sunIntensity);
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

    public string GetTimeString()
    {
        int hours = Mathf.FloorToInt(currentTime);
        int minutes = Mathf.FloorToInt((currentTime - hours) * 60f);
        return $"{hours:00}:{minutes:00}";
    }

}