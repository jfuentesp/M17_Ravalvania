using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Inventory/Item/Equipment/Weapon")]
public class Weapon : Item
{
    [SerializeField]
    private string m_WeaponName;
    [SerializeField]
    private float m_Damage;

    public string Name => m_WeaponName;
    public float Damage => m_Damage;

    public override bool UsedBy(GameObject go)
    {
        if (!go.TryGetComponent<EquipableBehaviour>(out EquipableBehaviour equipable))
        {
            return false;
        }
        equipable.SetWeapon(this);
        return true;
    }
}
