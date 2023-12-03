using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInventoryItem : MonoBehaviour
{
    [Header("Functionality")]
    [SerializeField]
    private GameEventItemPlayer m_Event;

    [Header("Display")]
    private TextMeshProUGUI m_IDText;
    [SerializeField]
    private TextMeshProUGUI m_AmountText;
    [SerializeField]
    private Image m_Image;

    public void Load(Item item, EPlayer player)
    {
        m_Image.sprite = item.Sprite; 
        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(() => RaiseEvent(item, player));
    }

    public void Load(Inventory.ItemSlot itemSlot, EPlayer player)
    {
        Load(itemSlot.item, player);
        m_AmountText.text = itemSlot.amount.ToString();
    }

    private void RaiseEvent(Item item, EPlayer player)
    {
        m_Event.Raise(item, player);
    }

}
