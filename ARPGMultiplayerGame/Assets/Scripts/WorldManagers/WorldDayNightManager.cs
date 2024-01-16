using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DK
{
    public class WorldDayNightManager : MonoBehaviour
    {
        public static WorldDayNightManager Instance;

        [Range(0, 1)]
        [SerializeField] public float timeOfDay;
        [SerializeField] private float sunriseDurationInMin = 0.1f;
        [SerializeField] private float dayDurationInMin = 0.1f;
        [SerializeField] private float sunsetDurationInMin = 0.1f;
        [SerializeField] private float nightDurationInMin = 0.1f;
        [SerializeField] public float currentTime;

        [Header("Bools")]
        [SerializeField] public bool isDayTime;
        [SerializeField] public bool isSunriseTime;
        [SerializeField] public bool isSunsetTime;
        [SerializeField] public bool isNightTime;
        [SerializeField] public bool isRaining;
        [SerializeField] public bool isSnowing;

        [Header("Materials")]
        [SerializeField] private Material nightMat;
        [SerializeField] private Material sunsetMat;
        [SerializeField] private Material snowyMat;
        [SerializeField] private Material rainyMat;
        [SerializeField] private Material dayMat;
        [SerializeField] private Material sunriseMat;
        [SerializeField] private Material skyBoxMat;

        [Header("Lights")]
        [SerializeField] private Light sunriseLight;
        [SerializeField] private GameObject sunriseLightGameObject;
        [SerializeField] private Light dayTimeLight;
        [SerializeField] private GameObject dayTimeLightGameObject;
        [SerializeField] private Light sunsetTimeLight;
        [SerializeField] private GameObject sunsetLightGameObject;
        [SerializeField] private Light nightTimeLight;
        [SerializeField] private GameObject nightTimeLightGameObject;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }

        // Start is called before the first frame update
        void Start()
        {
            isDayTime = true;
            currentTime = 0.20f;

            sunriseLightGameObject.SetActive(false);
            sunsetLightGameObject.SetActive(false);
            nightTimeLightGameObject.SetActive(false);
            dayTimeLightGameObject.SetActive(false);
            dayTimeLightGameObject.SetActive(true);

            skyBoxMat = new Material(dayMat);
            RenderSettings.skybox = skyBoxMat;
            RenderSettings.ambientIntensity = 2.7f;
            RenderSettings.sun = dayTimeLight;
            RenderSettings.fogColor = new Color32(0, 133, 166, 255);
            RenderSettings.fogDensity = 0.01f;
        }

        // Update is called once per frame
        void Update()
        {
            HandleDayNightCycle();
        }

        private void HandleDayNightCycle()
        {
            if (isSunriseTime)
            {
                float timeDuringDay = Time.deltaTime / (sunriseDurationInMin * 60f);
                currentTime += timeDuringDay;
            }
            else if (isDayTime)
            {
                float timeDuringDay = Time.deltaTime / (dayDurationInMin * 60f);
                currentTime += timeDuringDay;
            }
            else if (isSunsetTime)
            {
                float timeDuringDay = Time.deltaTime / (sunsetDurationInMin * 60f);
                currentTime += timeDuringDay;
            }
            else if (isNightTime)
            {
                float timeDuringDay = Time.deltaTime / (nightDurationInMin * 60f);
                currentTime += timeDuringDay;
            }

            if (timeOfDay >= 0f && timeOfDay < 0.20f)
            {
                isNightTime = true;
                isDayTime = false;
                isSunriseTime = false;
                isSunsetTime = false;

                sunriseLightGameObject.SetActive(false);
                sunsetLightGameObject.SetActive(false);
                dayTimeLightGameObject.SetActive(false);
                nightTimeLightGameObject.SetActive(false);
                nightTimeLightGameObject.SetActive(true);

                skyBoxMat = new Material(nightMat);
                RenderSettings.skybox = skyBoxMat;
                RenderSettings.ambientIntensity = 2f;
                RenderSettings.sun = nightTimeLight;
                RenderSettings.fogColor = new Color32(61, 87, 173, 255);
                RenderSettings.fogDensity = 0.02f;
            }

            if (timeOfDay >= 0.20f && timeOfDay < 0.40f)
            {
                isSunriseTime = true;
                isNightTime = false;
                isDayTime = false;
                isSunsetTime = false;

                sunsetLightGameObject.SetActive(false);
                dayTimeLightGameObject.SetActive(false);
                nightTimeLightGameObject.SetActive(false);
                sunriseLightGameObject.SetActive(false);
                sunriseLightGameObject.SetActive(true);

                skyBoxMat = new Material(sunriseMat);
                RenderSettings.skybox = skyBoxMat;
                RenderSettings.sun = sunriseLight;
                RenderSettings.ambientIntensity = 1.4f;
                RenderSettings.fogColor = new Color32(217, 158, 62, 255);
                RenderSettings.fogDensity = 0.02f;
            }

            if (timeOfDay >= 0.40f && timeOfDay < 0.60f)
            {
                isDayTime = true;
                isSunriseTime = false;
                isNightTime = false;
                isSunsetTime = false;

                sunriseLightGameObject.SetActive(false);
                sunsetLightGameObject.SetActive(false);
                nightTimeLightGameObject.SetActive(false);
                dayTimeLightGameObject.SetActive(false);
                dayTimeLightGameObject.SetActive(true);

                skyBoxMat = new Material(dayMat);
                RenderSettings.skybox = skyBoxMat;
                RenderSettings.ambientIntensity = 1.7f;
                RenderSettings.sun = dayTimeLight;
                RenderSettings.fogColor = new Color32(0, 133, 166, 255);
                RenderSettings.fogDensity = 0.01f;
            }

            if (timeOfDay >= 0.60f && timeOfDay < 0.80f)
            {
                isSunsetTime = true;
                isDayTime = false;
                isSunriseTime = false;
                isNightTime = false;

                skyBoxMat = new Material(sunsetMat);
                RenderSettings.skybox = skyBoxMat;
                RenderSettings.ambientIntensity = 0.8f;
                RenderSettings.sun = sunsetTimeLight;
                RenderSettings.fogColor = new Color32(108, 71, 47, 255);
                RenderSettings.fogDensity = 0.02f;
            }

            if (timeOfDay >= 0.80f && timeOfDay < 1.0f)
            {
                isNightTime = true;
                isSunsetTime = false;
                isDayTime = false;
                isSunriseTime = false;

                sunriseLightGameObject.SetActive(false);
                sunsetLightGameObject.SetActive(false);
                dayTimeLightGameObject.SetActive(false);
                nightTimeLightGameObject.SetActive(false);
                nightTimeLightGameObject.SetActive(true);

                skyBoxMat = new Material(nightMat);
                RenderSettings.skybox = skyBoxMat;
                RenderSettings.sun = nightTimeLight;
                RenderSettings.ambientIntensity = 3f;
                RenderSettings.fogColor = new Color32(61, 87, 173, 255);
                RenderSettings.fogDensity = 0.02f;
            }

            if (timeOfDay >= 1.0f)
            {
                currentTime = 0.0f;
            }

            timeOfDay = currentTime;
        }
    }
}
