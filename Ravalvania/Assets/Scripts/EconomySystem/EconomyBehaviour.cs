using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class stores all the economy system. No currency has been inflated in the process.
/// </summary>
public class EconomyBehaviour : MonoBehaviour
{
    [Header("Economy initial stats")]
    [SerializeField]
    private int m_InitialCoins;

    private void Awake()
    {
        //m_InitialCoins = 500;
        //ChangeCoins(m_InitialCoins);
    }

    private int m_PlayerCoins;
    public int PlayerCoins => m_PlayerCoins;

    public void SetCoins(int coins)
    {
        m_PlayerCoins = coins;
    }

    public void ChangeCoins(int amount)
    {
        m_PlayerCoins += amount;
    }
}
