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
    //Getter of the Entity current health
    public float CurrentHealth => m_CurrentHealth;
    [SerializeField]
    private bool m_IsAlive;
    //Getter of the alive state of the Entity
    public bool IsAlive => m_IsAlive;

    void Start()
    {
        m_CurrentHealth = m_MaxHealth;       
        m_IsAlive = true;
    }

    //Public function that Changes health. If health drops to zero, the object is no longer alive, so it can notify other elements.
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
