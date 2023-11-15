using System.Collections;
using System.Collections.Generic;
using streetsofraval;
using UnityEngine;

public class DropableBehaviour : MonoBehaviour
{
    private int m_ScoreValue;
    public int ScoreValue => m_ScoreValue;
    [Header("List of things the entity can drop")]
    [SerializeField]
    private int m_Coins;
    public int Coins => m_Coins;
    [SerializeField]
    private List<PickupScriptableObject> m_Pickups;
    // Start is called before the first frame update
    private void Awake()
    {
        
    }

    public void DropOnDestroy()
    {

    }

    public void SetCoins(int coinsToSet)
    {
        m_Coins = coinsToSet;
    }

}
