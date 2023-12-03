using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealingOnTimeItems", menuName = "Inventory/Item/HealingOnTimeItems")]
public class HealingOnTimeItems : Item
{
    [SerializeField]
    private float m_HealAmount;
    [SerializeField]
    private float m_HealTime;
    [SerializeField]
    private float m_HealSpeed;
    public float Heal => m_HealAmount;
    public float Time => m_HealTime;
    public float Speed => m_HealSpeed;
    public override bool UsedBy(GameObject go, EPlayer player, EPlayer owner)
    {
        if (!go.TryGetComponent<HealableBehaviour>(out HealableBehaviour healable))
        {
            return false;
        }
        if (player == owner)
        {
            healable.OnHealOnTime(m_HealAmount, m_HealTime, m_HealSpeed);
            return true;
        }
        return false;
    }
}
