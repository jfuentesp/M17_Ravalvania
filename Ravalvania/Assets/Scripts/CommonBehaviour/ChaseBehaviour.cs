using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ChaseBehaviour : MonoBehaviour
{
    private MovableBehaviour m_Moving;
    private bool m_TargetDetected;
    public bool TargetDetected => m_TargetDetected;
    private GameObject m_Target;
    public GameObject Target => m_Target;

    private void Awake()
    {
        m_TargetDetected = false;
        m_Moving = GetComponentInParent<MovableBehaviour>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            m_TargetDetected = true;
            m_Target = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            m_TargetDetected = false;
            m_Target = null;
        }
    }

    public void SetTargetDetected(bool isDetected)
    {
        m_TargetDetected = isDetected;
    }

    public void OnTargetChase()
    {
        if(m_Target == null)
        {
            m_Moving.OnStopMovement();
            return;
        }
        Vector2 direction = new Vector2(m_Target.transform.position.x - transform.position.x, m_Target.transform.position.y - transform.position.y).normalized;
        m_Moving.OnMoveByForce(direction);
    }
}
