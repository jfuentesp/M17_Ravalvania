using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthBehaviour))]
public class DamageableBehaviour : MonoBehaviour
{
    HealthBehaviour m_Health;

    private void Awake()
    {
        m_Health = GetComponent<HealthBehaviour>();
    }

    //Function that substracts damage to the Health component
    public void GetDamage(float damageAmount)
    {
        m_Health.ChangeHealth(-damageAmount);
    }
}
