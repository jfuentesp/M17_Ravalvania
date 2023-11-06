using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovableBehaviour : MonoBehaviour
{
    //Movement will have a direction and a speed, and a clamp if needs to move by Forces. Also it will need a RigidBody to set the movement.
    private Rigidbody2D m_Rigidbody;
    private Vector2 m_Direction;

    [Header("Speed of the Entity")]
    [SerializeField]
    private float m_Speed;

    [Header("Velocity clamp to move by Forces and ForceToAdd")]
    [SerializeField]
    private float m_VelocityClamp;
    [SerializeField]
    private float m_ForceToAdd;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    //Function that moves by Velocity (no force interactions with effectors)
    private void MoveByVelocity(Vector2 direction)
    {
        m_Rigidbody.velocity = direction * m_Speed;
    }

    //Function that moves by Forces (interacts with effectors)
    private void MoveByForce(Vector2 direction)
    {
        float currentVelocity = Mathf.Clamp(m_Rigidbody.velocity.x, 0, m_VelocityClamp);
        if(currentVelocity < m_VelocityClamp) 
            m_Rigidbody.AddForce(direction * m_ForceToAdd);
    }
}
