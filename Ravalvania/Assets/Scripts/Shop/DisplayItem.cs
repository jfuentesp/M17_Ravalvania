using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayItem : MonoBehaviour
{
    [Header("Purchase Event")]
    [SerializeField]
    private GameEventItemPlayer m_PurchaseEvent;

    [Header("Display")]
    [SerializeField]
    private TextMeshProUGUI m_Price;
    [SerializeField]
    private Image m_Image;
    [SerializeField]
    private Button m_Button;

    private Item m_Item;
    private EPlayer m_PlayerStore;

    EconomyBehaviour m_Money;

    // Start is called before the first frame update
    void Start()
    {
        m_Button.onClick.AddListener(RaiseEvent);
        m_Money = LevelManager.LevelManagerInstance.GetComponent<EconomyBehaviour>();
    }

    public void LoadItem(Item item, EPlayer player)
    {
        m_PlayerStore = player;
        m_Item = item;
        m_Image.sprite = item.Sprite;
        m_Price.text = item.Price + "G";
    }

    private void RaiseEvent()
    {
        if(m_Money.PlayerCoins >= m_Item.Price)
        {
            m_Money.ChangeCoins(-m_Item.Price);
            m_PurchaseEvent.Raise(m_Item, m_PlayerStore);
        }
    }
}
