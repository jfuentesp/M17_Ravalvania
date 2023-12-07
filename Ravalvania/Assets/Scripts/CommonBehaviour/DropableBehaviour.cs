using System.Collections;
using System.Collections.Generic;
using streetsofraval;
using UnityEngine;

/// <summary>
/// This class sets the object to drop elements once it's destroyed or disabled.
/// </summary>
public class DropableBehaviour : MonoBehaviour
{
    private int m_ScoreValue;
    public int ScoreValue => m_ScoreValue;
    [Header("List of things the entity can drop")]
    [SerializeField]
    private int m_Coins;
    public int Coins => m_Coins;
    [SerializeField]
    private GameObject m_PickupPrefab;

    /// <summary>
    /// Drops a random item based on a specified amount of chance.
    /// </summary>
    public void DropOnDestroy()
    {
        float chance = Random.value;
        if(chance > 0.5f)
        {
            GameObject pickup = Instantiate(m_PickupPrefab);
            pickup.transform.position = transform.position;
        }
    }

    public void SetCoins(int coinsToSet)
    {
        m_Coins = coinsToSet;
    }

}
