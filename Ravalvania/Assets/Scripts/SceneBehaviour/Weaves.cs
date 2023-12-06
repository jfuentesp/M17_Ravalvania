using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weaves : MonoBehaviour
{
    [SerializeField]
    float freaquencyWaves;
    [SerializeField]
    float force;
    float currentY;
    bool m_switch;
    bool isOnWater;
    Rigidbody2D m_Rigidbody;

    void Start()
    {
        m_Rigidbody = gameObject.GetComponent<Rigidbody2D>();
        currentY = m_Rigidbody.position.y;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
            isOnWater = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
            isOnWater = false;
    }

    private void FixedUpdate()
    {
        if (!isOnWater)
            return;

        if (!m_switch)
        {
            if (m_Rigidbody.position.y < currentY - freaquencyWaves)
                m_switch = true;
            return;
        }

        if (m_Rigidbody.position.y > currentY + freaquencyWaves)
            m_switch = false;
        
        m_Rigidbody.AddForce(new Vector2(0, force), ForceMode2D.Impulse);
    }
}