using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableBehaviour : MonoBehaviour
{
    HealthBehaviour m_Health;
    [SerializeField]
    private float m_BaseAttackDamage;
    public float BaseAttackDamage => m_BaseAttackDamage;
    private float m_AttackDamage;
    public float AttackDamage => m_AttackDamage;

    private void Awake()
    {
        m_Health = GetComponentInParent<HealthBehaviour>();
    }

    private void Start()
    {
        m_AttackDamage = m_BaseAttackDamage;
    }

    //Function that substracts damage to the Health component
    public void OnDealingDamage(float damage)
    {
        m_Health.ChangeHealth(-damage);
    }
    //Function that updates base damage (either if a buff or a debuff is applied or on a level up) 
    public void OnUpdateBaseDamage(float damageAmount)
    {
        m_BaseAttackDamage += damageAmount;
    }
    //Function that updates damage (either if a buff or a debuff is applied or on a level up) 
    public void OnUpdateDamage(float damage)
    {
        m_AttackDamage += damage;
    }
    //Function that sets damage 
    public void OnSetDamage(float damage)
    {
        m_AttackDamage = damage;
    }
}
