using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayItem : MonoBehaviour
{
    [Header("Purchase Event")]
    [SerializeField]
    private GameEventItem m_PurchaseEvent;

    [Header("Display")]
    [SerializeField]
    private TextMeshProUGUI m_Price;
    [SerializeField]
    private Image m_Image;
    [SerializeField]
    private Button m_Button;

    private Item m_Item;

    // Start is called before the first frame update
    void Start()
    {
        m_Button.onClick.AddListener(RaiseEvent);
    }

    public void LoadItem(Item item)
    {
        m_Item = item;
        m_Image.sprite = item.Sprite;
        m_Price.text = item.Price + "G";
    }

    private void RaiseEvent()
    {
        m_PurchaseEvent.Raise(m_Item);
    }
}
