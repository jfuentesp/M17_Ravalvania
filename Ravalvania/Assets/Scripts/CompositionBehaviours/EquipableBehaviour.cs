using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipableBehaviour : MonoBehaviour
{
    private DamageableBehaviour m_Damage;
    private DefenseBehaviour m_Defense;
    private PlayerBehaviour m_PlayerOrb;

    [SerializeField]
    private Weapon m_EquippedWeapon;
    [SerializeField]
    private Armor m_EquippedArmor;
    [SerializeField]
    private Orb m_EquippedOrb;

    public Weapon EquippedWeapon => m_EquippedWeapon;
    public Armor EquippedArmor => m_EquippedArmor;
    public Orb EquippedOrb => m_EquippedOrb;

    public void SetWeapon(Weapon weapon)
    {
        if (m_EquippedWeapon != null)
            DetachWeapon();
        m_EquippedWeapon = weapon;
        m_Damage.OnUpdateDamage(weapon.Damage);
    }
    
    public void DetachWeapon()
    {
        if(m_EquippedWeapon != null)
        {
            m_Damage.OnUpdateDamage(-m_EquippedWeapon.Damage);
            m_EquippedWeapon = null;
        }
    }

    public void SetArmor(Armor armor)
    {
        if (m_EquippedArmor != null)
            DetachArmor();
        m_EquippedArmor = armor;
        m_Defense.OnAddDefense(armor.Defense);
    }

    public void DetachArmor()
    {
        if (m_EquippedArmor != null)
        {
            m_Defense.OnAddDefense(-m_EquippedArmor.Defense);
            m_EquippedArmor = null;
        }
    }

    public void SetOrb(Orb orb)
    {
        if (m_EquippedOrb != null)
            DetachOrb();
        m_EquippedOrb = orb;
        m_PlayerOrb.SetOrbType(orb.OrbType);
    }

    public void DetachOrb()
    {
        if (m_EquippedOrb != null)
        {
            m_PlayerOrb.SetOrbType(EOrb.NONE);
            m_EquippedOrb = null;
        }
    }

}
