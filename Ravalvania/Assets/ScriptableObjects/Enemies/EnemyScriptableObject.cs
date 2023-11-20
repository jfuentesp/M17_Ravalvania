using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

//Order of difficulty: Common < Elite < Veteran


[CreateAssetMenu(fileName = "EnemyInfoScriptableObject", menuName = "Scriptable Objects/Scriptable EnemyInfo")]
public class EnemyScriptableObject : ScriptableObject
{
    [SerializeField]
    private float m_EnemyMaxHP;
    [SerializeField]
    private float m_EnemyDamage;
    [SerializeField]
    private float m_EnemySpeed;
    [SerializeField]
    private float m_EnemyDefense;
    [SerializeField]
    private Color m_SpriteColor;
    [SerializeField]
    private int m_ExperienceValue;
    [SerializeField]
    private int m_MoneyValue;


    public float EnemyMaxHP => m_EnemyMaxHP;
    public float EnemyDamage => m_EnemyDamage;
    public float EnemySpeed => m_EnemySpeed;
    public float EnemyDefense => m_EnemyDefense;
    public Color SpriteColor => m_SpriteColor;
    public int ExperienceValue => m_ExperienceValue;
    public int MoneyValue => m_MoneyValue;
}


