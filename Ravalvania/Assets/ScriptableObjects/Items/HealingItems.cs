using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealingItems", menuName = "Inventory/Item/HealingItems")]
public class HealingItems : Item
{
    [SerializeField]
    private float m_HealAmount;
    public float Heal => m_HealAmount;
    public override bool UsedBy(GameObject go, EPlayer player, EPlayer owner)
    {
        if (!go.TryGetComponent<HealableBehaviour>(out HealableBehaviour healable))
        {
            return false;
        }
        if(player == owner)
        {
            healable.OnHeal(m_HealAmount);
            return true;
        }       
        return false;
    }
}
