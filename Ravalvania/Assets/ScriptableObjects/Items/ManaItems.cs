using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ManaItems", menuName = "Inventory/Item/ManaItems")]
public class ManaItems : Item
{
    [SerializeField]
    private float m_ManaAmount;
    public float Mana => m_ManaAmount;
    public override bool UsedBy(GameObject go)
    {
        if (!go.TryGetComponent<ManaBehaviour>(out ManaBehaviour fillable))
        {
            return false;
        }
        fillable.OnChangeMana(m_ManaAmount);
        return true;
    }
}
