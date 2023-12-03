using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Orb", menuName = "Inventory/Item/Equipment/Orb")]
public class Orb : Item
{
    [SerializeField]
    private string m_OrbName;
    [SerializeField]
    private EOrb m_OrbType;

    public string Name => m_OrbName;
    public EOrb OrbType => m_OrbType;

    public override bool UsedBy(GameObject go, EPlayer player, EPlayer owner)
    {
        if (!go.TryGetComponent<EquipableBehaviour>(out EquipableBehaviour equipable))
        {
            return false;
        }
        if (player == owner)
        {
            equipable.SetOrb(this);
            return true;
        }
        return false;
    }
}
