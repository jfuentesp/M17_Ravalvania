using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        m_Health = GetComponentInParent<HealthBehaviour>();
        m_Defense = GetComponentInParent<DefenseBehaviour>();
        m_AttackDamage = 1;
        m_ComboMultiplier = 1;
    }

    //Function that substracts damage to the Health component
    public void OnDealingDamage(float damage)
    {
        //Damage function -> if damage is higher or equal than defense, we raise it double and substract def so the value is not zero.
        //Else, it will be multiplied by its value / defense so the damage can't skyrocket
        if (damage >= m_Defense.Defense)
            damage = damage * 2 - m_Defense.Defense;
        else
            damage = damage * damage / m_Defense.Defense;
        m_Health.ChangeHealth(-damage * m_ComboMultiplier);
        m_OnGuiEvent.Raise();
    }
    //Function that updates base damage (either if a buff or a debuff is applied or on a level up) 
    public void OnAddBaseDamage(float damageAmount)
    {
        m_BaseAttackDamage += damageAmount;
    }
    //Function that updates damage (either if a buff or a debuff is applied or on a level up) 
    public void OnAddDamage(float damage)
    {
        m_AttackDamage += damage;
    }
    //Function that sets damage 
    public void OnSetDamage(float damage)
    {
        m_AttackDamage = damage;
    }

    public void SetComboMultiplier(float multiplier)
    {
        m_ComboMultiplier = multiplier;
    }
}
