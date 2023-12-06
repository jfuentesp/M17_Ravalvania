using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class JumpBehaviour : MonoBehaviour
{
    private Rigidbody2D m_Rigidbody;

    [Header("Force added to the jump")]
    [SerializeField]
    private float m_Force;
    [SerializeField]
    private LayerMask m_GroundLayerMask;

    bool m_OnGround;
    bool m_DoubleJump;

    void Awake()
    {
        m_OnGround = false;
        m_DoubleJump = false;
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    public void JumpByForce()
    {
        if (m_OnGround || m_DoubleJump)
        {
            m_DoubleJump = false;
            m_Rigidbody.AddForce(Vector2.up * m_Force);
        }
    }

    private void Update()
    {
        if(Physics2D.Raycast(transform.position, Vector2.down, gameObject.transform.localScale.y * 1.1f, m_GroundLayerMask))
        {
            m_OnGround = true;
            m_DoubleJump = true;
        } else
        {
            m_OnGround = false;
        }
    }
}
