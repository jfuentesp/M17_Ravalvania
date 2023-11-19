using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


[CreateAssetMenu(fileName = "PlayerInfoScriptableObject", menuName = "Scriptable Objects/Scriptable PlayerInfo")]
public class PlayerScriptableObject : ScriptableObject
{
    [SerializeField]
    private float m_PlayerMaxHP;
    [SerializeField]
    private float m_PlayerMaxMana;
    [SerializeField]
    private float m_PlayerDamage;
    [SerializeField]
    private float m_PlayerSpeed;
    [SerializeField]
    private float m_PlayerDefense;
    [SerializeField]
    private int m_Experience;
    [SerializeField]
    private int m_Level;


    public float PlayerMaxHP => m_PlayerMaxHP;
    public float PlayerMaxMana => m_PlayerMaxMana;
    public float PlayerDamage => m_PlayerDamage;
    public float PlayerSpeed => m_PlayerSpeed;
    public float PlayerDefense => m_PlayerDefense;
    public int Experience => m_Experience;
    public int Level => m_Level;
}


