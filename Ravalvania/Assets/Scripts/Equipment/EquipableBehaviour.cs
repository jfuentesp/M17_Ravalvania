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

    private void Awake()
    {
        m_Damage = GetComponentInChildren<DamageableBehaviour>();
        m_Defense = GetComponent<DefenseBehaviour>();
        m_PlayerOrb = GetComponent<PlayerBehaviour>();
    }

    public void SetWeapon(Weapon weapon)
    {
        if (m_EquippedWeapon != null)
            DetachWeapon();
        m_EquippedWeapon = weapon;
        Debug.Log(string.Format("Equipped weapon: {0} | Weapon is: {1} | Damage is: {2}", weapon.Name, m_EquippedWeapon.Name, m_EquippedWeapon.Damage));
        m_Damage.OnAddDamage(m_EquippedWeapon.Damage);
    }
    
    public void DetachWeapon()
    {
        if(m_EquippedWeapon != null)
        {
            m_Damage.OnAddDamage(-m_EquippedWeapon.Damage);
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
