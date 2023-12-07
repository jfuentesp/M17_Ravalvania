using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is implemented if the object can be damaged.
/// </summary>
public class DamageableBehaviour : MonoBehaviour
{
    HealthBehaviour m_Health;
    DefenseBehaviour m_Defense;

    [SerializeField]
    GameEvent m_OnGuiEvent;
    [SerializeField]
    private float m_BaseAttackDamage;
    public float BaseAttackDamage => m_BaseAttackDamage;
    private float m_AttackDamage;
    public float AttackDamage => m_AttackDamage;
    private float m_ComboMultiplier;
    [SerializeField]
    private float m_KnockbackPower;
    public float KnockbackPower => m_KnockbackPower;


    private void Awake()
    {
        m_Health = GetComponentInParent<HealthBehaviour>();
        m_Defense = GetComponentInParent<DefenseBehaviour>();
        m_AttackDamage = 1;
        m_ComboMultiplier = 2;
    }

    /// <summary>
    /// Function that substracts damage to the Health component. Works with a damage formula to prevent skyrocket damaging.
    /// </summary>
    /// <param name="damage"></param>
    public void OnDealingDamage(float damage)
    {
        //Damage function -> if damage is higher or equal than defense, raises it twice and substracts def so the value is never zero.
        //Else, it will be multiplied by its value / defense so the damage can't skyrocket
        if (damage >= m_Defense.Defense)
            damage = damage * 2 - m_Defense.Defense;
        else
            damage = damage * damage / m_Defense.Defense;
        m_Health.ChangeHealth(-damage * m_ComboMultiplier);
        m_OnGuiEvent.Raise();
    }

    /// <summary>
    /// Function that updates damage (e.g if a buff or a debuff is applied, increasing stats on level up ,...) 
    /// </summary>
    /// <param name="damage"></param>
    public void OnAddDamage(float damage)
    {
        m_AttackDamage += damage;
    }

    /// <summary>
    /// Function that sets a specific damage. 
    /// </summary>
    /// <param name="damage"></param>
    public void OnSetDamage(float damage)
    {
        m_AttackDamage = damage;
    }

    /// <summary>
    /// Sets a multiplier for combo attacks.
    /// </summary>
    /// <param name="multiplier"></param>
    public void SetComboMultiplier(float multiplier)
    {
        m_ComboMultiplier = multiplier;
    }
}
