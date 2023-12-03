using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInventoryItem : MonoBehaviour
{
    [Header("Functionality")]
    [SerializeField]
    private GameEventItem m_Event;

    [Header("Display")]
    [SerializeField]
    private TextMeshProUGUI m_IDText;
    [SerializeField]
    private TextMeshProUGUI m_AmountText;
    [SerializeField]
    private Image m_Image;

    public void Load(Item item)
    {
        //m_IDText.text = item.Id;
        m_Image.sprite = item.Sprite; 
        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(() => RaiseEvent(item));
    }

    public void Load(Inventory.ItemSlot itemSlot)
    {
        Load(itemSlot.item);
        m_AmountText.text = itemSlot.amount.ToString();
    }

    private void RaiseEvent(Item item)
    {
        m_Event.Raise(item);
    }

}
