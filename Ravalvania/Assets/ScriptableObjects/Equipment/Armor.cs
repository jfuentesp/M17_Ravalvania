using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArmorScriptableObject", menuName = "Scriptable Objects/Scriptable Armor")]
public class Armor : ScriptableObject
{
    [SerializeField]
    private string m_ArmorName;
    [SerializeField]
    private int m_Cost;
    [SerializeField]
    private float m_Defense;

    public string Name => m_ArmorName;
    public int Cost => m_Cost;
    public float Defense => m_Defense;
}
