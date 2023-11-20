using streetsofraval;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryBehaivour : MonoBehaviour
{
    private PlayerBehaviour Owner;
    public Player m_Owner;
    //private List<ItemSlot> items;
    private bool InventoryAbierto;
    [SerializeField]
    private Inventory m_Backpack;
    [SerializeField]
    private GameEvent m_GUIEvent;
    // Start is called before the first frame update
    void Awake()
    {
        
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    public void AbrirInventario()
    {
        if(InventoryAbierto)
        {
            //Tanca Inventari
            InventoryAbierto = false;
        }
        else
        {
            InventoryAbierto=true;
        }
    }
    */
    public void ConsumeItem(Item item)
    {
        if (!item.UsedBy(gameObject))
            return;

        m_Backpack.RemoveItem(item);
        m_GUIEvent.Raise();
    }

    public void AddItem(Item item)
    {
        m_Backpack.AddItem(item);
        m_GUIEvent.Raise();
    }

}
