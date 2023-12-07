using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class defines a Health element for the object. It manages the state of the element to be destroyed.
/// </summary>
public class HealthBehaviour : MonoBehaviour
{
    private LevelingBehaviour m_Leveling;

    [Header("Health of the Entity")]
    [SerializeField]
    private float m_MaxHealth;
    private float m_MaxHealthBase;
    private float m_CurrentHealth;
    //Getter of the Entity current health
    public float CurrentHealth => m_CurrentHealth;
    public float MaxHealth => m_MaxHealth;
    [SerializeField]
    private bool m_IsAlive;
    //Getter of the alive state of the Entity
    public bool IsAlive => m_IsAlive;
    [SerializeField]
    private GameEvent m_OnDeathEvent;
    [SerializeField]
    private GameEventInt m_OnDeathExpEvent;
    [SerializeField]
    private bool m_IsDestroyedOnDeath;
    [SerializeField]
    private bool m_GivesExpOnDeath;

    private void Awake()
    {
        m_CurrentHealth = m_MaxHealth;
        m_IsAlive = true;
    }

    void Start()
    {
        if(m_GivesExpOnDeath)
            m_Leveling = GetComponent<LevelingBehaviour>();  
    }

    public void SetMaxHealthBase(float maxHealth)
    {
        m_MaxHealthBase = maxHealth;
    }

    public void AddMaxHealth(float maxHealth)
    {
        m_MaxHealth += maxHealth;
        m_CurrentHealth += maxHealth;
    }

    /// <summary>
    /// Public function that Changes health. If health drops to zero, the object is no longer alive, so it can notify other elements.
    /// </summary>
    /// <param name="amountToChange"></param>
    public void ChangeHealth(float amountToChange)
    {
        m_CurrentHealth += amountToChange;
        if(m_CurrentHealth > m_MaxHealth)
            m_CurrentHealth = m_MaxHealth;
        if (m_CurrentHealth <= 0)
        {
            m_CurrentHealth = 0;
            m_IsAlive = false;
        }
    }

    /// <summary>
    /// A function that manages the object death. If it's an enemy, it triggers an event that adds an amount of experience. If it's the player, it raises an event that respawns the player.
    /// </summary>
    public void OnDeath()
    {
        if (m_OnDeathEvent != null || m_OnDeathExpEvent != null)
        {
            if (m_GivesExpOnDeath)
            {
                m_OnDeathExpEvent.Raise(m_Leveling.ExpGivenOnDeath);
            } else
            {
                m_OnDeathEvent.Raise();
            }
        }            
        if (m_IsDestroyedOnDeath)
        {
            Destroy(gameObject);
        } 
        else
        {
            gameObject.SetActive(false);
        }
    }
}
