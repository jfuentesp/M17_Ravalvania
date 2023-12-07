using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class allows the object to move by using physics.
/// </summary>

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
        m_IsFlipped = false;
    }

    private void Start()
    {
        m_Speed = m_InitialSpeed;
    }

    /// <summary>
    /// Function that moves by Velocity (no force interactions with effectors)
    /// </summary>
    /// <param name="direction"></param>
    public void OnMoveByVelocity(Vector2 direction)
    {
        m_Rigidbody.velocity = direction * m_Speed;
    }

    /// <summary>
    /// Function that moves by Forces (interacts with effectors)
    /// </summary>
    /// <param name="direction"></param>
    public void OnMoveByForce(Vector2 direction)
    {
        float currentVelocity = Mathf.Clamp(m_Rigidbody.velocity.x, -m_VelocityClamp, m_VelocityClamp);
        if(currentVelocity < m_VelocityClamp && currentVelocity > -m_VelocityClamp) 
            m_Rigidbody.AddForce(direction * m_ForceToAdd);
        OnFlipCharacter(direction);
    }

    /// <summary>
    /// Stops the entity movement (sets it to Vector3.zero)
    /// </summary>
    public void OnStopMovement()
    {
        m_Rigidbody.velocity = Vector3.zero;
    }

    /// <summary>
    /// Compares in which direction the entity is looking at. Then, rotates the object in the right direction. 
    /// </summary>
    /// <param name="direction"></param>
    public void OnFlipCharacter(Vector2 direction)
    {
        if (direction == Vector2.zero)
            return;
        m_IsFlipped = direction.x < 0 ? true : false;
        m_Rigidbody.transform.eulerAngles = m_IsFlipped ? Vector3.up * 180 : Vector3.zero;
    }

    /// <summary>
    /// Sets a force to the object that acts as a knockback.
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="knockbackpower"></param>
    public void OnKnockback(Vector2 direction, float knockbackpower)
    {
        m_Rigidbody.AddForce(direction * knockbackpower, ForceMode2D.Impulse);
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
