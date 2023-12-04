using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour
{
    private Animator m_Animator;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_Animator.Play("explosion");
    }

    public void OnDestroy()
    {
        Destroy(gameObject);
    }
}
