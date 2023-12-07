using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class defines that the object has a defensive stat. Requires the object to be damageable in order to apply an effect.
/// </summary>

public class DefenseBehaviour : MonoBehaviour
{
    [SerializeField]
    private float m_BaseDefense;
    public float BaseDefense => m_BaseDefense;
    [SerializeField]
    private float m_Defense;
    public float Defense => m_Defense;

    void Awake()
    {
        m_Defense = 1;
    }

    public void OnAddBaseDefense(float baseDefense)
    {
        m_BaseDefense = baseDefense;
    }

    public void OnAddDefense(float defense)
    {
        m_Defense += defense;
    }

    public void OnSetDefense(float defense)
    {
        m_Defense = defense;
    }
}
