using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Experimental.GlobalIllumination;

public class CelestialController : MonoBehaviour
{
    [Header("Celestial Bodies")]
    [SerializeField] private Transform celestialPivot;
    [SerializeField] private Light directionalLight;
    [SerializeField] private GameObject sunPrefab;
    [SerializeField] private GameObject moonPrefab;

    [Header("Light Settings")]
    [SerializeField] private float maxSunIntensity = 1f;
    [SerializeField] private float maxMoonIntensity = 0.5f;
    [SerializeField] private Color sunColor = Color.white;
    [SerializeField] private Color moonColor = new Color(0.6f, 0.6f, 0.8f, 1f);

    private GameObject sunObject;
    private GameObject moonObject;
    private void Start()
    {
        if (!directionalLight)
        {
            Debug.LogError("CelestialController: Directional Light not assigned!");
            return;
        }
        InitializeCelestialBodies();
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

    public void UpdateCelestialBodies(float currentTime, float sunriseHour, float sunsetHour)
    {
        UpdateRotation(currentTime, sunriseHour, sunsetHour);
        UpdateLighting(currentTime, sunriseHour, sunsetHour);
    }

    private void UpdateRotation(float currentTime, float sunriseHour, float sunsetHour)
    {
        float rotation = 0f;

        if (currentTime >= sunriseHour && currentTime <= sunsetHour) // 撤
        {
            float dayDuration = sunsetHour - sunriseHour;
            rotation = -(currentTime - sunriseHour) * 180f / dayDuration;

            if (celestialPivot)
            {
                celestialPivot.rotation = Quaternion.Euler(rotation, 0f, 0f);
                if (sunObject) sunObject.SetActive(true);
                if (moonObject) moonObject.SetActive(false);
            }
        }
        else // 广
        {
            float nightDuration = 24f - sunsetHour + sunriseHour;
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
    }

    private void UpdateLighting(float currentTime, float sunriseHour, float sunsetHour)
    {
        float lightIntensity = 0f;
        Color lightColor;

        if (currentTime >= sunriseHour && currentTime <= sunsetHour)
        {
            if (currentTime <= sunriseHour + 2f) // 老免
            {
                float t = (currentTime - sunriseHour) / 2f;
                lightIntensity = Mathf.Lerp(maxMoonIntensity, maxSunIntensity, t);
                lightColor = Color.Lerp(moonColor, sunColor, t);
            }
            else if (currentTime >= sunsetHour - 2f) // 老隔
            {
                float t = (currentTime - (sunsetHour - 2f)) / 2f;
                lightIntensity = Mathf.Lerp(maxSunIntensity, maxMoonIntensity, t);
                lightColor = Color.Lerp(sunColor, moonColor, t);
            }
            else // 撤
            {
                lightIntensity = maxSunIntensity;
                lightColor = sunColor;
            }
        }
        else // 广
        {
            lightIntensity = maxMoonIntensity;
            lightColor = moonColor;
            UnityEngine.RenderSettings.ambientLight = moonColor * 1.2f;
        }

        if (directionalLight)
        {
            directionalLight.intensity = lightIntensity;
            directionalLight.color = lightColor;
        }
    }

    private void OnDestroy()
    {
        if (sunObject) Destroy(sunObject);
        if (moonObject) Destroy(moonObject);
    }
}
