using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovableBehaviour))]
public class PatrolBehaviour : MonoBehaviour
{
    private MovableBehaviour m_Moving;
    private Vector2 m_Direction;
    public Vector2 PatrolDirection => m_Direction;
    [Header("Patrol time if need to patrol over time to switch sides")]
    [SerializeField]
    private float m_PatrolTime;
    private Coroutine m_PatrolCoroutine;

    private enum Direction { RIGHT, LEFT }
    [SerializeField]
    private Direction m_DefaultDirection;

    void Awake()
    {
        m_Direction = m_DefaultDirection == Direction.RIGHT ? Vector2.right : -Vector2.right;
        m_Moving = GetComponent<MovableBehaviour>();
    }

    public void OnPatrolByTime()
    {
        m_Direction = m_Moving.IsFlipped ? -Vector2.right : Vector2.right;
        if(m_PatrolCoroutine == null)
            m_PatrolCoroutine = StartCoroutine(PatrolByTimeCoroutine());
    }

    public void OnPatrolStop()
    {
        if(m_PatrolCoroutine != null)
            StopCoroutine(m_PatrolCoroutine);
    }

    private IEnumerator PatrolByTimeCoroutine()
    {
        while (true)
        {
            Debug.Log(m_Direction);
            yield return new WaitForSeconds(m_PatrolTime);
            m_Direction *= -1;
        }   
    }
}
