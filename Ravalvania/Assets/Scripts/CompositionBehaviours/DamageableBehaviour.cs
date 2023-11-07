using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthBehaviour))]
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
        m_Health = GetComponent<HealthBehaviour>();
    }

    private void Start()
    {
        m_AttackDamage = m_BaseAttackDamage;
    }

    //Function that substracts damage to the Health component
    public void OnDealingDamage()
    {
        m_Health.ChangeHealth(m_AttackDamage);
    }
    //Function that updates damage (either if a debuff or a debuff is applied or on a level up) 
    public void OnUpdateDamage(float damageAmount)
    {
        m_AttackDamage = damageAmount;
    }
}
