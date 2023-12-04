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
    
    [SerializeField]
    private LayerMask m_GroundLayerMask;

    private enum Direction { RIGHT, LEFT }
    [SerializeField]
    private Direction m_DefaultDirection;

    void Awake()
    {
        m_Direction = m_DefaultDirection == Direction.RIGHT ? Vector2.right : -Vector2.right;
        m_Moving = GetComponent<MovableBehaviour>();
    }

    private void OnEnable()
    {
        m_Direction = m_DefaultDirection == Direction.RIGHT ? Vector2.right : -Vector2.right;
        m_Moving = GetComponent<MovableBehaviour>();
    }

    private void Update()
    {
        //Debug.DrawRay(transform.position + Vector3.right / 2, Vector2.down*1.5f, Color.green, 2f);
        if (!Physics2D.Raycast(transform.position + Vector3.right/2, Vector2.down*1.5f, gameObject.transform.localScale.y * 1.2f, m_GroundLayerMask))
        {     
            //m_Direction *= -1;
        }      
    }

    private Coroutine m_PatrolCoroutine;

    public void OnPatrolByTime()
    {
        //m_Direction = m_Moving.IsFlipped ? -Vector2.right : Vector2.right;
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
            m_Direction *= -1;
            yield return new WaitForSeconds(m_PatrolTime);           
        }   
    }
}
