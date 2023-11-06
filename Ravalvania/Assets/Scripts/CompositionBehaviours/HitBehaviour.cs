using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamageableBehaviour))]
public class HitBehaviour : MonoBehaviour
{
    DamageableBehaviour m_Damageable;

    private void Awake()
    {
        m_Damageable = GetComponent<DamageableBehaviour>();
    }

    //Function that sets damage on contact with the element


}
