using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OrbScriptableObject", menuName = "Scriptable Objects/Scriptable Orb")]
public class Orb : ScriptableObject
{
    [SerializeField]
    private string m_OrbName;
    [SerializeField]
    private int m_Cost;
    [SerializeField]
    private OrbEnum m_OrbType;

    public string Name => m_OrbName;
    public int Cost => m_Cost;
    public OrbEnum OrbType => m_OrbType;
}
