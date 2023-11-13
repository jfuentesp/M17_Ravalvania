using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class JumpBehaviour : MonoBehaviour
{
    private Rigidbody2D m_Rigidbody;

    [Header("Force added to the jump")]
    private float m_Force;

    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    public void JumpByForce()
    {
        m_Rigidbody.AddForce(Vector2.up * m_Force);
    }
}
