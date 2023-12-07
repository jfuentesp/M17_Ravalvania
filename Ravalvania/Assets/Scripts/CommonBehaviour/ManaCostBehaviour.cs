using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An external class to set the amount of mana cost to an ability. It's a test.
/// </summary>

[RequireComponent(typeof(ManaBehaviour))]
public class ManaCostBehaviour : MonoBehaviour
{
    ManaBehaviour m_Mana;
    [SerializeField]
    private float m_ManaCost;
    public float ManaCost => m_ManaCost;
    /// <summary>
    /// In case that we need more than one ManaCost we can do a List of costs (attacks)
    /// </summary>

    private void Awake()
    {
        m_Mana = GetComponent<ManaBehaviour>();
    }

    // Function that increases or decreases mana
    private void OnConsumingMana()
    {
        m_Mana.OnChangeMana(m_ManaCost);
    }
}
