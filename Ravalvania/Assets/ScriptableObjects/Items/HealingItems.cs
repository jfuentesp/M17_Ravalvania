using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealingItems", menuName = "Inventory/Item/HealingItems")]
public class HealingItems : Item
{
    public float Heal;
    public override bool UsedBy(GameObject go)
    {
        if (!go.TryGetComponent<IHealable>(out IHealable healable))
        {
            return false;
        }

        healable.heal(Heal);
        return true;
    }
}
