using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AttackableBehaviour : MonoBehaviour
{
    //An attackable object will have an attack damage and a Hitbox
    private HitboxInfo m_Hitbox;
    
    [Header("Attack damage")]
    [SerializeField]
    private float m_Damage;

    private void Awake()
    {
        m_Hitbox = GetComponent<HitboxInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Attack() { 
    
    }
}
