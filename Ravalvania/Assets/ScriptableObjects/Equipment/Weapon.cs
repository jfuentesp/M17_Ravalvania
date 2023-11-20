using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponScriptableObject", menuName = "Scriptable Objects/Scriptable Weapon")]
public class Weapon : ScriptableObject
{
    [SerializeField]
    private string m_WeaponName;
    [SerializeField]
    private int m_Cost;
    [SerializeField]
    private float m_Damage;

    public string Name => m_WeaponName;
    public int Cost => m_Cost;
    public float Damage => m_Damage;
}
