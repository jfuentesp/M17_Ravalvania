using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyBehaviour : MonoBehaviour
{
    [Header("Economy initial stats")]
    [SerializeField]
    private int m_InitialCoins;

    private void OnEnable()
    {
        ChangeCoins(m_InitialCoins);
    }

    private int m_PlayerCoins;
    public int PlayerCoins => m_PlayerCoins;

    public void ChangeCoins(int amount)
    {
        m_PlayerCoins += amount;
    }
}
