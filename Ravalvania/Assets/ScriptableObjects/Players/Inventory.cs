using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/Mochilla")]
public class Inventory : ScriptableObject
{
    [Serializable]
    public class ItemSlot
    {
        [SerializeField]
        public Item item;
        [SerializeField]
        public int amount;
        public ItemSlot(Item obj)
        {
            item = obj;
            amount = 1;
        }
    }

    [SerializeField]
    public List<ItemSlot> m_Mochilla = new List<ItemSlot>();
    

    
    public void AddItem(Item usedItem)
    {
        ItemSlot item = GetItem(usedItem);
        if (item == null)
            m_Mochilla.Add(new ItemSlot(usedItem));
        else
            item.amount++;
    }
    public void RemoveItem(Item usedItem)
    {
        ItemSlot item = GetItem(usedItem);
        if (item == null)
            return;

        item.amount--;
        if (item.amount <= 0)
            m_Mochilla.Remove(item);
    }

    private ItemSlot GetItem(Item item)
    {
        return m_Mochilla.FirstOrDefault(slot => slot.item == item);
    }


}

