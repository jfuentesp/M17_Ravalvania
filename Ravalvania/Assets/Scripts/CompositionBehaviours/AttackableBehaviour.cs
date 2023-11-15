using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AttackableBehaviour : MonoBehaviour
{
    private bool m_TargetDetected;
    public bool TargetDetected => m_TargetDetected;

    private void Awake()
    {
        m_TargetDetected = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            m_TargetDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            m_TargetDetected = false;
        }
    }

    public void SetTargetDetected(bool isDetected)
    {
        m_TargetDetected = isDetected;
    }
}
