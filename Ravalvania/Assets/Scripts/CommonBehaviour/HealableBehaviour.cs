using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthBehaviour))]
public class HealableBehaviour : MonoBehaviour
{
    HealthBehaviour m_Health;

    private void Awake()
    {
        m_Health = GetComponent<HealthBehaviour>();
    }

    //Function that adds health points to the Health compoment
    public void OnHeal(float healAmount)
    {
        m_Health.ChangeHealth(healAmount);
    }

    private Coroutine m_HoTCoroutine;

    public void OnHealOnTime(float healAmount, float time, float speed)
    {
        m_HoTCoroutine = StartCoroutine(HealOnTimeCoroutine(healAmount, time, speed));
    }

    public void StopHealingOnTime()
    {
        if(m_HoTCoroutine != null)
        {
            StopCoroutine(m_HoTCoroutine);
        }
    }

    private IEnumerator HealOnTimeCoroutine(float healAmount, float time, float speed)
    {
        float timeLapse = 0;
        while(timeLapse <= time)
        {
            m_Health.ChangeHealth(healAmount);
            yield return new WaitForSeconds(speed);
            timeLapse += Time.deltaTime;
        }
    }
}
