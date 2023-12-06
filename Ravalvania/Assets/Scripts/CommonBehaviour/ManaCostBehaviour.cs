using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ManaBehaviour))]
public class ManaCostBehaviour : MonoBehaviour
{
    ManaBehaviour m_Mana;
    [SerializeField]
    private float m_ManaCost;
    public float ManaCost => m_ManaCost;
    //In case that we need more than one ManaCost we can do a List of costs (attacks)

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
