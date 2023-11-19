using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovableBehaviour : MonoBehaviour
{
    //Movement will have a direction and a speed, and a clamp if needs to move by Forces. Also it will need a RigidBody to set the movement.
    private Rigidbody2D m_Rigidbody;

    [Header("Speed of the Entity")]
    [SerializeField]
    private float m_InitialSpeed;
    private float m_Speed;
    public float Speed => m_Speed;

    [Header("Velocity clamp to move by Forces and ForceToAdd")]
    [SerializeField]
    private float m_VelocityClamp;
    [SerializeField]
    private float m_ForceToAdd;
    [SerializeField]
    private bool m_IsFlipped;
    public bool IsFlipped => m_IsFlipped;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        m_Speed = m_InitialSpeed;
    }

    //Function that moves by Velocity (no force interactions with effectors)
    public void OnMoveByVelocity(Vector2 direction)
    {
        m_Rigidbody.velocity = direction * m_Speed;
    }

    //Function that moves by Forces (interacts with effectors)
    public void OnMoveByForce(Vector2 direction)
    {
        float currentVelocity = Mathf.Clamp(m_Rigidbody.velocity.x, -m_VelocityClamp, m_VelocityClamp);
        if(currentVelocity < m_VelocityClamp && currentVelocity > -m_VelocityClamp) 
            m_Rigidbody.AddForce(direction * m_ForceToAdd);
        OnFlipCharacter(direction);
    }
    //Stops the entity movement (sets it to Vector3.zero)
    public void OnStopMovement()
    {
        m_Rigidbody.velocity = Vector3.zero;
    }
    //Compares which direction the entity is looking. Then, rotates the object in the looking direction. 
    public void OnFlipCharacter(Vector2 direction)
    {
        if (direction == Vector2.zero)
            return;
        m_IsFlipped = direction.x < 0 ? true : false;
        m_Rigidbody.transform.eulerAngles = m_IsFlipped ? Vector3.up * 180 : Vector3.zero;
    }

    public void SetSpeedBase(float speed)
    {
        m_InitialSpeed = speed;
        m_VelocityClamp = speed;
    }

    public void AddSpeed(float speed)
    {
        m_Speed += speed;
        m_VelocityClamp += speed;
    }

    public void SetSpeed(float speed)
    {
        m_Speed = speed;
        m_VelocityClamp = speed;
    }
}
