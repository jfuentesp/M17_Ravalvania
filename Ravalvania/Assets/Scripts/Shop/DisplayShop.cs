using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayShop : MonoBehaviour
{
    [SerializeField]
    private GameObject m_DisplayShop;

    [SerializeField]
    private GameObject m_DisplayItemPrefab;

    [SerializeField]
    private Item[] m_ItemsToDisplay;

    private void Start()
    {
        foreach (Item item in m_ItemsToDisplay)
        {
            GameObject displayedItem = Instantiate(m_DisplayItemPrefab, m_DisplayShop.transform);
            displayedItem.GetComponent<DisplayItem>().LoadItem(item);
        }
    }
}
