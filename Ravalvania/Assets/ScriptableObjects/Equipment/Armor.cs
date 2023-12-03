using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Armor", menuName = "Inventory/Item/Equipment/Armor")]
public class Armor : Item
{
    [SerializeField]
    private string m_ArmorName;
    [SerializeField]
    private float m_Defense;

    public string Name => m_ArmorName;
    public float Defense => m_Defense;

    public override bool UsedBy(GameObject go)
    {
        if(!go.TryGetComponent<EquipableBehaviour>(out EquipableBehaviour equipable))
        {
            return false;
        }
        equipable.SetArmor(this);
        return true;
    }
}
