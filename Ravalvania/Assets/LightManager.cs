using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightManager : MonoBehaviour
{
    [SerializeField]
    private Color m_DayLightColor;
    [SerializeField]
    private Color m_NightTimeColor;
    [SerializeField]
    private float m_DayLightIntensity;
    [SerializeField]
    private float m_NightTimeIntensity;

    private bool m_IsDayTime;

    private Light2D m_LightSettings;

    [SerializeField]
    List<Light2D> m_Lights;

    private void Awake()
    {
        m_LightSettings = GetComponent<Light2D>();
        m_IsDayTime = true;
        m_DayNightCoroutine = StartCoroutine(DayNightCoroutine());
    }

    private void OnDisable()
    {
        StopCoroutine(DayNightCoroutine());
    }

    private Coroutine m_DayNightCoroutine;

    private IEnumerator DayNightCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            if (m_IsDayTime)
            {
                m_IsDayTime = false;
                m_LightSettings.intensity = m_NightTimeIntensity;
                m_LightSettings.color = m_NightTimeColor;
                foreach(Light2D light in m_Lights)
                {
                    light.enabled = true;
                }
            }
            else
            {
                m_IsDayTime = true;
                m_LightSettings.intensity = m_DayLightIntensity;
                m_LightSettings.color = m_DayLightColor;
                foreach (Light2D light in m_Lights)
                {
                    light.enabled = false;
                }
            }
        }
    }
}
