using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableBehaviour : MonoBehaviour
{
    //Movement will have a direction and a speed. Also it will need a RigidBody to set the movement
    //private Rigidbody2D m_RigidBody;
    //private Vector2 m_Direction;
    //private float m_Speed;
    //As it's a MonoBehaviour component it can't have Constructors, so we will need to set a clamp for the force.
    //private float m_VelocityClamp;

    private void MoveByVelocity(Rigidbody2D rigidbody, Vector2 direction, float speed)
    {
        rigidbody.velocity = direction * speed;
    }

    private void MoveByForce(Rigidbody2D rigidbody, Vector2 direction, float forceToAdd, float velocityToClamp)
    {
        float currentVelocity = Mathf.Clamp(rigidbody.velocity.x, 0, velocityToClamp);
        if(currentVelocity < velocityToClamp) 
            rigidbody.AddForce(direction * forceToAdd);
    }
}
