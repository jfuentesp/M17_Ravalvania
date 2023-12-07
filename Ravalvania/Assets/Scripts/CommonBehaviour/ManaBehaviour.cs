using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class sets the capability to hold a resource to use special skills.
/// </summary>
public class ManaBehaviour : MonoBehaviour
{
    [SerializeField]
    GameEvent m_OnGUIEvent;

    [Header("Mana of the Entity")]
    [SerializeField]
    private float m_MaxMana;
    private float m_MaxManaBase;
    private float m_CurrentMana;
    //Getter of the Entity's current mana
    public float CurrentMana => m_CurrentMana;
    public float MaxMana => m_MaxMana;

    void Start()
    {
        m_CurrentMana = m_MaxMana;
    }

    public void SetMaxManaBase(float maxMana)
    {
        m_MaxManaBase = maxMana;
    }

    public void AddMaxMana(float maxMana)
    {
        m_MaxMana += maxMana;
        m_CurrentMana += maxMana;
    }

    public void SetMana(float mana)
    {
        m_CurrentMana = mana;
    }

    /// <summary>
    /// Public function that changes the amount of mana of the Entity
    /// </summary>
    /// <param name="manaAmount"></param>
    public void OnChangeMana(float manaAmount)
    {
        m_CurrentMana += manaAmount;
        if(m_CurrentMana < 0)
            m_CurrentMana = 0;
        if (m_CurrentMana > m_MaxMana)
            m_CurrentMana = m_MaxMana;
        m_OnGUIEvent.Raise();
    }
}
