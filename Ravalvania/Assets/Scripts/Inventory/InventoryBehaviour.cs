using streetsofraval;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryBehaviour : MonoBehaviour
{
    [SerializeField]
    private EPlayer m_Owner;

    [SerializeField]
    private Inventory m_P1Backpack;
    [SerializeField]
    private Inventory m_P2Backpack;
    [SerializeField]
    private GameEvent m_InventoryRefreshEvent;
    [SerializeField]
    private GameObject m_P1Inventory;
    [SerializeField]
    private GameObject m_P2Inventory;
    // Start is called before the first frame update
    void Awake()
    {
        m_Owner = GetComponentInParent<PlayerBehaviour>().PlayerSelect;
    }   

    public void OnInventoryOpen(EPlayer player)
    {
        if(player == EPlayer.PLAYER1)
        {
            if (!m_P1Inventory.activeInHierarchy)
            {
                m_P1Inventory?.SetActive(true);
                return;
            }
            m_P1Inventory?.SetActive(false);
        } 
        else
        {
            if (!m_P2Inventory.activeInHierarchy)
            {
                m_P2Inventory?.SetActive(true);
                return;
            }
            m_P2Inventory?.SetActive(false);
        }
    }

    public void ConsumeItem(Item item, EPlayer player)
    {
        if (!item.UsedBy(gameObject, player, m_Owner))
            return;

        if(player == EPlayer.PLAYER1)
            m_P1Backpack.RemoveItem(item);
        if(player == EPlayer.PLAYER2)
            m_P2Backpack.RemoveItem(item);
        m_InventoryRefreshEvent.Raise();
    }

    public void AddItem(Item item, EPlayer player)
    {
        if(player == EPlayer.PLAYER1 && m_Owner == player)
            m_P1Backpack.AddItem(item);
        if(player == EPlayer.PLAYER2 && m_Owner == player)
            m_P2Backpack.AddItem(item);
        m_InventoryRefreshEvent.Raise();
    }
}
