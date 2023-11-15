using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(MovableBehaviour))]
[RequireComponent(typeof(HealthBehaviour))]
[RequireComponent(typeof(PatrolBehaviour))]
[RequireComponent(typeof(DropableBehaviour))]
public class EnemyRobberBehaviour : MonoBehaviour
{
    //Components
    private Rigidbody2D m_Rigidbody;
    private MovableBehaviour m_Moving;
    private HealthBehaviour m_Health;
    private DamageableBehaviour m_Damaging;
    private PatrolBehaviour m_Patrol;
    private ChaseBehaviour m_Chase;
    private AttackableBehaviour m_Attacking;
    private DropableBehaviour m_Dropping;

    //Animator
    private Animator m_Animator;

    //Player reference
    private GameObject m_TargetPlayer;

    //Animation names
    private const string m_IdleAnimationName = "idle";
    private const string m_WalkAnimationName = "walk";
    private const string m_Attack1AnimationName = "attack1";
    private const string m_HitAnimationName = "hit";
    private const string m_DieAnimationName = "die";

    //Reference to this sprite renderer
    private SpriteRenderer m_SpriteRenderer;
    private bool m_IsInvulnerable;
    public bool IsInvulnerable => m_IsInvulnerable;

    //States from Enemy statemachine
    private enum EnemyMachineStates { IDLE, PATROL, CHASE, ATTACK, FLEE, HIT }
    private EnemyMachineStates m_CurrentState;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Moving = GetComponent<MovableBehaviour>();
        m_Health = GetComponent<HealthBehaviour>();
        m_Damaging = GetComponentInChildren<DamageableBehaviour>();
        m_Patrol = GetComponent<PatrolBehaviour>();
        m_Chase = GetComponentInChildren<ChaseBehaviour>();
        m_Attacking = GetComponent<AttackableBehaviour>();
        m_Dropping = GetComponent<DropableBehaviour>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_IsInvulnerable = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        InitState(EnemyMachineStates.IDLE);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHitbox") && !m_IsInvulnerable)
        {
            m_Damaging.OnDealingDamage(collision.gameObject.GetComponentInChildren<DamageableBehaviour>().AttackDamage);
            ChangeState(EnemyMachineStates.HIT);
        }

        if (collision.CompareTag("PlayerProjectile") && !m_IsInvulnerable)
        {
            m_Damaging.OnDealingDamage(collision.gameObject.GetComponent<DamageableBehaviour>().AttackDamage);
            ChangeState(EnemyMachineStates.HIT);
            Destroy(collision.gameObject);
        }
    }

    public void EndOfHit()
    {
        ChangeState(EnemyMachineStates.CHASE);
    }

    public void InitEnemy(EnemyScriptableObject enemyInfo, int spawnpoint)
    {
        m_Health.SetMaxHealth(enemyInfo.EnemyMaxHP);
        m_Damaging.OnSetDamage(enemyInfo.EnemyDamage);
        m_Moving.SetSpeed(enemyInfo.EnemySpeed);
        m_SpriteRenderer.color = enemyInfo.SpriteColor;
        m_Dropping.SetCoins(enemyInfo.ScoreValue);
    }

    /* !!! BUILDING UP STATE MACHINE !!! Always change state with the function ChangeState */
    private void ChangeState(EnemyMachineStates newState)
    {
        //if the actual state is the same as the state we are trying to set, it exits the function
        if (newState == m_CurrentState)
            return;
        //First, it will do the actions to exit the current state, then will initiate the new state.
        ExitState();
        InitState(newState);
    }

    /* InitState will run every instruction that has to be started ONLY when enters a state */
    private void InitState(EnemyMachineStates currentState)
    {
        //We declare that the current state of the object is the new state we declare on the function
        m_CurrentState = currentState;

        //Then it will compare the current state to run the state actions
        switch (m_CurrentState)
        {
            case EnemyMachineStates.IDLE:
                m_Moving.OnStopMovement();
                m_Animator.Play(m_IdleAnimationName);
                break;

            case EnemyMachineStates.PATROL:
                m_Patrol.OnPatrolByTime();
                m_Animator.Play(m_WalkAnimationName);
                break;

            case EnemyMachineStates.CHASE:
                m_Animator.Play(m_WalkAnimationName);
                break;

            case EnemyMachineStates.HIT:
                m_Moving.OnStopMovement();
                m_Animator.Play(m_HitAnimationName);
                break;

            case EnemyMachineStates.FLEE:
                m_Animator.Play(m_WalkAnimationName);
                m_Moving.SetSpeed(m_Moving.Speed / 2);
                break;

            case EnemyMachineStates.ATTACK:
                //Attack will set the velocity to zero, so it cant move while attacking
                m_Moving.OnStopMovement();
                m_Animator.Play(m_Attack1AnimationName);
                break;

            default:
                break;
        }
    }

    /* ExitState will run every instruction that has to be started ONLY when exits a state */
    private void ExitState()
    {
        switch (m_CurrentState)
        {
            case EnemyMachineStates.PATROL:
                m_Patrol.OnPatrolStop();
                break;

            default:
                break;
        }
    }

    /* UpdateState will control every frame since it will be called from Update() and will control when it changes the state */
    private void UpdateState()
    {
        Vector2 direction = m_Moving.IsFlipped ? -Vector2.right : Vector2.right;
        m_Moving.OnFlipCharacter(direction);

        switch (m_CurrentState)
        {
            case EnemyMachineStates.IDLE:
                if (m_Chase.TargetDetected)
                    ChangeState(EnemyMachineStates.CHASE);
                break;

            case EnemyMachineStates.PATROL:
                m_Moving.OnMoveByForce(m_Patrol.PatrolDirection);
                if (m_Chase.TargetDetected)
                    ChangeState(EnemyMachineStates.CHASE);
                break;

            case EnemyMachineStates.CHASE:
                m_Chase.OnTargetChase();
                if (m_Attacking.TargetDetected)
                    ChangeState(EnemyMachineStates.ATTACK);
                if (!m_Chase.TargetDetected)
                    ChangeState(EnemyMachineStates.PATROL);
                break;

            default:
                break;
        }
    }
}
