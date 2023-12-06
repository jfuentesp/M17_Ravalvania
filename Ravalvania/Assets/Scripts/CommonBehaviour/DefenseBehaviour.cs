using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseBehaviour : MonoBehaviour
{
    [SerializeField]
    private float m_BaseDefense;
    public float BaseDefense => m_BaseDefense;
    [SerializeField]
    private float m_Defense;
    public float Defense => m_Defense;

    // Start is called before the first frame update
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
