using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBehaviour : MonoBehaviour
{
    [Header("Health of the Entity")]
    [SerializeField]
    private float m_MaxHealth;
    private float m_CurrentHealth;
    [SerializeField]
    private bool m_IsAlive;
    public bool IsAlive => m_IsAlive;

    // Start is called before the first frame update
    void Start()
    {
        m_CurrentHealth = m_MaxHealth;       
        m_IsAlive = true;
    }

    public void ChangeHealth(float amountToChange)
    {
        m_CurrentHealth += amountToChange;
        if(m_CurrentHealth > m_MaxHealth)
            m_CurrentHealth = m_MaxHealth;
        if (m_CurrentHealth < 0)
        {
            m_CurrentHealth = 0;
            m_IsAlive = false;
        }
    }
}
