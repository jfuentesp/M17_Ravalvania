using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(MovableBehaviour))]
[RequireComponent(typeof(HealthBehaviour))]
[RequireComponent(typeof(DamageableBehaviour))]
[RequireComponent(typeof(PatrolBehaviour))]
[RequireComponent(typeof(DropableBehaviour))]
[RequireComponent(typeof(DefenseBehaviour))]
[RequireComponent(typeof(LevelingBehaviour))]
public class EnemyThiefBehaviour : MonoBehaviour, IObjectivable
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
    private DefenseBehaviour m_Defense;
    private LevelingBehaviour m_Leveling;  

    //Animator
    private Animator m_Animator;

    //Scriptable object references (Enemy tier on spawn)
    [SerializeField]
    List<EnemyScriptableObject> m_EnemyTier;

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
    private enum EnemyMachineStates { IDLE, PATROL, CHASE, ATTACK, FLEE, HIT, DEAD }
    private EnemyMachineStates m_CurrentState;

    private Vector2 m_PatrolDirection = Vector2.right;

    //Mission info for Hit and Kill countdown
    MissionBehaviour m_Mission;

    [Header("GameEvent for the player XP increase")]
    [SerializeField]
    private GameEventInt m_OnEnemyDeathExp;

    [Header("GameEvent for the mission")]
    [SerializeField]
    private GameEvent m_OnObjectiveCountdown;
    private int m_EnemyID;

    Vector2 m_LastAttackSide;
    float m_LastKnockbackPower;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Moving = GetComponent<MovableBehaviour>();
        m_Health = GetComponent<HealthBehaviour>();
        m_Damaging = GetComponentInChildren<DamageableBehaviour>();
        m_Patrol= GetComponent<PatrolBehaviour>();
        m_Chase = GetComponentInChildren<ChaseBehaviour>();
        m_Attacking = GetComponentInChildren<AttackableBehaviour>();
        m_Dropping = GetComponent<DropableBehaviour>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_Defense = GetComponent<DefenseBehaviour>();
        m_Leveling = GetComponent<LevelingBehaviour>();
        m_IsInvulnerable = false;
    }

    private void Start()
    {
        m_Mission = LevelManager.LevelManagerInstance.GetComponent<MissionBehaviour>();
        InitEnemy();
        InitState(EnemyMachineStates.PATROL);
    }

    void OnEnable()
    {
        InitState(EnemyMachineStates.PATROL);
        m_IsInvulnerable = false;
    }

    void Update()
    {
        UpdateState();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHitbox") && !m_IsInvulnerable)
        {
            m_Damaging.OnDealingDamage(collision.gameObject.GetComponentInChildren<DamageableBehaviour>().AttackDamage);
            m_LastAttackSide = collision.transform.position.x < transform.position.x ? Vector2.right : -Vector2.right;
            m_LastKnockbackPower = collision.gameObject.GetComponentInChildren<DamageableBehaviour>().KnockbackPower;
            ChangeState(EnemyMachineStates.HIT);
            if (!m_Health.IsAlive)
                ChangeState(EnemyMachineStates.DEAD);
        }

        if (collision.CompareTag("PlayerProjectile") && !m_IsInvulnerable)
        {
            m_Damaging.OnDealingDamage(collision.gameObject.GetComponent<DamageableBehaviour>().AttackDamage);
            m_LastAttackSide = collision.transform.position.x < transform.position.x ? Vector2.right : -Vector2.right;
            m_LastKnockbackPower = collision.gameObject.GetComponentInChildren<DamageableBehaviour>().KnockbackPower;
            ChangeState(EnemyMachineStates.HIT);
            Destroy(collision.gameObject);
            if (!m_Health.IsAlive)
                ChangeState(EnemyMachineStates.DEAD);
        }
    }

    public void EndOfHit()
    {
        ChangeState(EnemyMachineStates.CHASE);
    }

    public void InitEnemy()
    {
        int rand = Random.Range(0, m_EnemyTier.Count);
        EnemyScriptableObject enemyInfo = m_EnemyTier[rand];
        m_Health.SetMaxHealthBase(enemyInfo.EnemyMaxHP);
        m_Damaging.OnSetDamage(enemyInfo.EnemyDamage);
        m_Defense.OnAddBaseDefense(enemyInfo.EnemyDefense);
        m_Moving.SetSpeed(enemyInfo.EnemySpeed);
        m_SpriteRenderer.color = enemyInfo.SpriteColor;
        m_Dropping.SetCoins(enemyInfo.MoneyValue);
        m_Leveling.OnSetExperienceOnDeath(enemyInfo.ExperienceValue);
        m_EnemyID = enemyInfo.EnemyType;
    }

    public void OnObjectiveCheck(EMission type)
    {
        if (m_Mission.MissionType == type)
            m_OnObjectiveCountdown.Raise();
    }

    public void OnDeath()
    {
        gameObject.SetActive(false);
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
                m_Moving.OnKnockback(m_LastAttackSide, m_LastKnockbackPower);
                if (m_Mission.MissionType == EMission.HIT)
                    m_OnObjectiveCountdown.Raise();
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

            case EnemyMachineStates.DEAD:
                m_IsInvulnerable = true;
                m_Moving.OnStopMovement();
                m_Animator.Play(m_DieAnimationName);
                if (m_Mission != null && m_Mission.MissionType == EMission.KILL && m_Mission.ObjectiveType == m_EnemyID)
                    m_OnObjectiveCountdown.Raise();
                m_OnEnemyDeathExp.Raise(m_Leveling.ExpGivenOnDeath);
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
