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
    public void GetHeal(float healAmount)
    {
        m_Health.ChangeHealth(healAmount);
    }
}
