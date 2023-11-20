using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesplegarInventari : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject m_ParentInventory;
    [SerializeField]
    private GameObject m_ItemPrefab;
    [SerializeField]
    private Inventory m_Backpack;

    void OnEnable()
    {
        RefreshBackpack();
    }
    private void ClearDisplay()
    {
        foreach (Transform child in m_ParentInventory.transform)
            Destroy(child.gameObject);
    }

    private void FillDisplay()
    {
        foreach (Inventory.ItemSlot itemSlot in m_Backpack.m_Mochilla)
        {
            GameObject displayedItem = Instantiate(m_ItemPrefab, m_ParentInventory.transform);
            displayedItem.GetComponent<DesplegarItem>().Load(itemSlot);
        }
    }

    public void RefreshBackpack()
    {
        ClearDisplay();
        FillDisplay();
    }

}
