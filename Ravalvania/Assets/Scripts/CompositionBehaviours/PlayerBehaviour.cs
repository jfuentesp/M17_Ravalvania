using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(MovableBehaviour))]
[RequireComponent(typeof(HealthBehaviour))]
[RequireComponent(typeof(HealableBehaviour))]
[RequireComponent(typeof(ManaBehaviour))]
[RequireComponent(typeof(ManaCostBehaviour))]
[RequireComponent(typeof(ShootableBehaviour))]
[RequireComponent(typeof(ComboBehaviour))]
[RequireComponent(typeof(DefenseBehaviour))]
[RequireComponent(typeof(JumpBehaviour))]
[RequireComponent(typeof(LevelingBehaviour))]
public class PlayerBehaviour : MonoBehaviour
{
    //Instance of the Player. Refers to this own gameobject. It needs to be an instance if the prefabs should refer to this object. (As enemies, for example)
    private static PlayerBehaviour m_Instance;
    public static PlayerBehaviour PlayerInstance => m_Instance; //A getter for the instance of the player. Similar to get { return m_Instance }. (Accessor)

    //Reference to the InputSystem
    [Header("Reference to the Input System")]
    [SerializeField]
    private InputActionAsset m_InputAsset;
    private InputActionAsset m_Input;
    public InputActionAsset Input => m_Input;
    private InputAction m_MovementAction;
    public InputAction MovementAction => m_MovementAction;
    private InputActionMap m_CurrentActionMap;


    //Components
    private Rigidbody2D m_Rigidbody;
    private MovableBehaviour m_Moving;
    private HealthBehaviour m_Health;
    private HealableBehaviour m_Healing;
    private ManaBehaviour m_Mana;
    private ManaCostBehaviour m_ManaCost;
    private ShootableBehaviour m_Shooting;
    private DamageableBehaviour m_Damaging;
    private ComboBehaviour m_Combo;
    private JumpBehaviour m_Jumping;
    private DefenseBehaviour m_Defense;
    private LevelingBehaviour m_Leveling;

    //Player animator
    private Animator m_Animator;

    //Animation names
    private const string m_IdleAnimationName = "idle";
    private const string m_WalkAnimationName = "walk";
    private const string m_JumpAnimationName = "jump";
    private const string m_Attack1AnimationName = "attack1";
    private const string m_Attack2AnimationName = "attack2";
    private const string m_Combo1AnimationName = "combo1";
    private const string m_Combo2AnimationName = "combo2";
    private const string m_SuperAnimationName = "super";
    private const string m_CrouchAnimationName = "crouch";
    private const string m_HitAnimationName = "hit";
    private const string m_CrouchAttack1AnimationName = "crouchattack1";
    private const string m_CrouchAttack2AnimationName = "crouchattack2";

    //Variables for the current state and an Enum for setting the Player States
    private enum PlayerMachineStates { NONE, IDLE, WALK, ATTACK1, ATTACK2, COMBO1, COMBO2, SUPER, JUMP, CROUCHATTACK1, CROUCHATTACK2, CROUCH, HIT }
    private PlayerMachineStates m_CurrentState;

    private bool m_IsInvulnerable;
    public bool IsInvulnerable => m_IsInvulnerable;
    private SpriteRenderer m_Sprite;

    [Header("LayerMask of the pickups")]
    [SerializeField]
    LayerMask m_PickupLayerMask;

    [Header("Is this object player1 or player2?")]
    [SerializeField]
    PlayerEnum m_PlayerSelect;

    [Header("Reference to the base statistics")]
    [SerializeField]
    private PlayerScriptableObject m_PlayerInfo;

    [Header("References to GameEvents")]
    [SerializeField]
    private GameEvent m_OnPlayerDamage;
    [SerializeField]
    private GameEvent m_OnEnergyUsed;
    [SerializeField]
    private GameEvent m_OnPlayerDeath;

    private void Awake()
    {
        //First, we initialize an instance of Player. If there is already an instance, it destroys the element and returns.
        if (m_Instance == null)
        {
            m_Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
        //Player components
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Moving = GetComponent<MovableBehaviour>();
        m_Health = GetComponent<HealthBehaviour>();
        m_Healing = GetComponent<HealableBehaviour>();
        m_Mana = GetComponent<ManaBehaviour>();
        m_ManaCost = GetComponent<ManaCostBehaviour>();
        m_Shooting = GetComponent<ShootableBehaviour>();
        m_Damaging = GetComponentInChildren<DamageableBehaviour>();
        m_Combo = GetComponent<ComboBehaviour>();
        m_Jumping = GetComponent<JumpBehaviour>();
        m_Defense = GetComponent<DefenseBehaviour>();
        m_Sprite = GetComponent<SpriteRenderer>();
        m_Leveling = GetComponent<LevelingBehaviour>();
        m_IsInvulnerable = false;

        //Setting the Input Controls
        Assert.IsNotNull(m_InputAsset);
        m_Input = Instantiate(m_InputAsset);
        m_CurrentActionMap = m_Input.FindActionMap("PlayerActions");
        //If the PlayerSelected Enum is Player1, will use the P1 inputs. If its 2, it will use the P2 inputs.
        m_CurrentActionMap.bindingMask = m_PlayerSelect == PlayerEnum.PLAYER1 ? InputBinding.MaskByGroup("player1") : InputBinding.MaskByGroup("player2");
        m_MovementAction = m_CurrentActionMap.FindAction("Movement");
    }

    private void OnEnable()
    {
        m_CurrentActionMap.FindAction("Attack1").performed += Attack1;
        m_CurrentActionMap.FindAction("Attack2").performed += Attack2;
        m_CurrentActionMap.FindAction("Jump").performed += Jump;
        m_CurrentActionMap.FindAction("Crouch").started += Crouch;
        m_CurrentActionMap.FindAction("Crouch").canceled += ReturnToIdleState;
        m_CurrentActionMap.Enable();
    }

    private void OnDisable()
    {
        m_CurrentActionMap.FindAction("Attack1").performed -= Attack1;
        m_CurrentActionMap.FindAction("Attack2").performed -= Attack2;
        m_CurrentActionMap.FindAction("Jump").performed -= Jump;
        m_CurrentActionMap.FindAction("Crouch").started -= Crouch;
        m_CurrentActionMap.FindAction("Crouch").canceled -= ReturnToIdleState;
        m_CurrentActionMap.Disable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyHitbox") && !m_IsInvulnerable)
        {
            m_Damaging.OnDealingDamage(collision.gameObject.GetComponentInChildren<DamageableBehaviour>().AttackDamage);
            ChangeState(PlayerMachineStates.HIT);
        }

        if (collision.CompareTag("EnemyProjectile") && !m_IsInvulnerable)
        {
            m_Damaging.OnDealingDamage(collision.gameObject.GetComponent<DamageableBehaviour>().AttackDamage);
            ChangeState(PlayerMachineStates.HIT);
            Destroy(collision.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //In this case, we can use InitState directly instead of ChangeState as it doesn't have to Exit any state previously. 
        InitState(PlayerMachineStates.IDLE);
        OnPlayerInit();
    }

    // Update is called once per frame
    void Update()
    {
        //Each frame, player behaviour will be listening 
        UpdateState();
    }

    private void OnPlayerInit()
    {
        m_Damaging.OnUpdateBaseDamage(m_PlayerInfo.PlayerDamage);
        m_Health.SetMaxHealthBase(m_PlayerInfo.PlayerMaxHP);
        m_Mana.SetMaxManaBase(m_PlayerInfo.PlayerMaxMana);
        m_Defense.OnSetBaseDefense(m_PlayerInfo.PlayerDefense);
        m_Moving.SetSpeedBase(m_PlayerInfo.PlayerSpeed);
    }

    public void EndHit()
    {
        ChangeState(PlayerMachineStates.IDLE);
    }

    //Function used to go back to idle state after performing an inputname.canceled action
    private void ReturnToIdleState(InputAction.CallbackContext context)
    {
        ChangeState(PlayerMachineStates.IDLE);
    }

    private IEnumerator OnPlayerHitCoroutine()
    {
        m_IsInvulnerable = true;
        m_Sprite.color = Color.red;
        yield return new WaitForSeconds(1f);
        m_Sprite.color = Color.white;
        m_IsInvulnerable = false;
    }

    /******** !!! BUILDING UP STATE MACHINE !!! Always change state with the function ChangeState ********/
    private void ChangeState(PlayerMachineStates newState)
    {
        //if the actual state is the same as the state we are trying to set, it exits the function
        if (newState == m_CurrentState)
            return;
        //First, it will do the actions to exit the current state, then will initiate the new state.
        ExitState();
        InitState(newState);
    }

    private void InitState(PlayerMachineStates currentState)
    {
        //We declare that the current state of the object is the new state we declare on the function
        m_CurrentState = currentState;

        //Then it will compare the current state to run the state actions
        switch (m_CurrentState)
        {
            case PlayerMachineStates.IDLE:
                m_Moving.OnStopMovement();
                m_Animator.Play(m_IdleAnimationName);
                break;

            case PlayerMachineStates.WALK:
                m_Animator.Play(m_WalkAnimationName);
                break;

            case PlayerMachineStates.JUMP:
                m_Animator.Play(m_JumpAnimationName);
                m_Jumping.JumpByForce();
                break;

            case PlayerMachineStates.ATTACK1:
                //Attack will set the velocity to zero, so it cant move while attacking
                m_Moving.OnStopMovement();
                m_Animator.Play(m_Attack1AnimationName);
                m_Damaging.SetComboMultiplier(1);
                break;

            case PlayerMachineStates.ATTACK2:
                //Attack will set the velocity to zero, so it cant move while attacking
                m_Moving.OnStopMovement();
                m_Animator.Play(m_Attack2AnimationName);
                m_Damaging.SetComboMultiplier(1.5f);
                break;

            case PlayerMachineStates.COMBO1:
                //Attack will set the velocity to zero, so it cant move while attacking
                m_Moving.OnStopMovement();
                m_Animator.Play(m_Combo1AnimationName);
                //Then we call for the shooting action and we pass the spawnpoint. We do substract the mana.
                m_Shooting.Shoot();
                Debug.Log("disparo");
                m_Mana.OnChangeMana(m_ManaCost.ManaCost);
                m_OnEnergyUsed.Raise();
                break;

            case PlayerMachineStates.COMBO2:
                //Attack will set the velocity to zero, so it cant move while attacking
                m_Moving.OnStopMovement();
                m_Animator.Play(m_Combo2AnimationName);
                m_Damaging.SetComboMultiplier(1.5f);
                break;

            case PlayerMachineStates.HIT:
                //Will play the animation and then set the state to Idle
                m_Moving.OnStopMovement();
                m_Animator.Play(m_HitAnimationName);
                StartCoroutine(OnPlayerHitCoroutine());
                break;

            case PlayerMachineStates.CROUCH:
                //Crouch sets the movement to zero and it doesn't move.
                m_Moving.OnStopMovement();
                m_Animator.Play(m_CrouchAnimationName);
                break;

            case PlayerMachineStates.CROUCHATTACK1:
                //Attack will set the velocity to zero, so it cant move while attacking
                m_Moving.OnStopMovement();
                m_Animator.Play(m_CrouchAttack1AnimationName);
                m_Damaging.SetComboMultiplier(1);
                break;

            case PlayerMachineStates.CROUCHATTACK2:
                //Attack will set the velocity to zero, so it cant move while attacking
                m_Moving.OnStopMovement();
                m_Animator.Play(m_CrouchAttack2AnimationName);
                m_Damaging.SetComboMultiplier(1);
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
            //If needs to leave a Coroutine or do something at the finish of the action...
            default:
                break;
        }
    }

    /* UpdateState will control every frame since it will be called from Update() and will control when it changes the state */
    private void UpdateState()
    {
        m_Moving.OnFlipCharacter(m_MovementAction.ReadValue<Vector2>());

        switch (m_CurrentState)
        {
            case PlayerMachineStates.IDLE:
                if (m_MovementAction.ReadValue<Vector2>().x != 0)
                    ChangeState(PlayerMachineStates.WALK);
                break;

            case PlayerMachineStates.WALK:
                m_Moving.OnMoveByForce(m_MovementAction.ReadValue<Vector2>());
                if (m_Rigidbody.velocity == Vector2.zero)
                    ChangeState(PlayerMachineStates.IDLE);
                break;

            case PlayerMachineStates.JUMP:
                if (m_Rigidbody.velocity == Vector2.zero)
                    ChangeState(PlayerMachineStates.IDLE);
                break;

            case PlayerMachineStates.CROUCH:
                //This gets the gameobject of the pickup, just as it would do in OnTriggerEnter/Stay, but with less load since it's a "Raycast"
                if (Physics2D.CircleCast(transform.position, 0.5f, Vector2.up, 0.5f, m_PickupLayerMask))
                {
                    GameObject pickup = Physics2D.CircleCast(transform.position, 0.5f, Vector2.up, 0.5f, m_PickupLayerMask).collider.gameObject;
                    pickup.GetComponent<PickupBehaviour>().GetPickup();
                    Destroy(pickup.gameObject);
                }
                break;

            default:
                break;
        }
    }
    /* !!! FINISHING THE BUILD OF THE STATE MACHINE !!! */

    /* !!!! ACTIONS WITH STATE MACHINE !!!! */
    private void Attack1(InputAction.CallbackContext context)
    {
        switch (m_CurrentState)
        {
            case PlayerMachineStates.IDLE:
                ChangeState(PlayerMachineStates.ATTACK1);
                break;

            case PlayerMachineStates.WALK:
                ChangeState(PlayerMachineStates.ATTACK1);
                break;

            case PlayerMachineStates.ATTACK1:
                if (m_Combo.ComboAvailable && m_ManaCost.ManaCost <= m_Mana.CurrentMana)
                    ChangeState(PlayerMachineStates.COMBO1);
                else
                    ChangeState(PlayerMachineStates.ATTACK1);
                break;

            case PlayerMachineStates.ATTACK2:
                if (m_Combo.ComboAvailable)
                    ChangeState(PlayerMachineStates.ATTACK1);
                else
                    ChangeState(PlayerMachineStates.ATTACK2);
                break;

            case PlayerMachineStates.CROUCH:
                if (m_Combo.ComboAvailable)
                    ChangeState(PlayerMachineStates.CROUCHATTACK2);
                else
                    ChangeState(PlayerMachineStates.CROUCHATTACK1);
                break;

            default:
                break;
        }
    }

    private void Attack2(InputAction.CallbackContext context)
    {
        switch (m_CurrentState)
        {
            case PlayerMachineStates.IDLE:
                ChangeState(PlayerMachineStates.ATTACK2);
                break;

            case PlayerMachineStates.WALK:
                ChangeState(PlayerMachineStates.ATTACK2);
                break;

            case PlayerMachineStates.ATTACK1:
                if (m_Combo.ComboAvailable)
                    ChangeState(PlayerMachineStates.ATTACK2);
                else
                    ChangeState(PlayerMachineStates.ATTACK1);
                break;

            case PlayerMachineStates.ATTACK2:
                if (m_Combo.ComboAvailable)
                    ChangeState(PlayerMachineStates.COMBO2);
                else
                    ChangeState(PlayerMachineStates.ATTACK2);
                break;

            case PlayerMachineStates.CROUCH:
                if (m_Combo.ComboAvailable)
                    ChangeState(PlayerMachineStates.CROUCHATTACK1);
                else
                    ChangeState(PlayerMachineStates.CROUCHATTACK2);
                break;

            default:
                break;
        }
    }

    private void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Im falling and i cant get up: "+m_Rigidbody.velocity.y);
        switch (m_CurrentState)
        {
            case PlayerMachineStates.IDLE:
                if (m_Rigidbody.velocity.y == 0)
                    ChangeState(PlayerMachineStates.JUMP);
                break;

            case PlayerMachineStates.WALK:
                if (m_Rigidbody.velocity.y == 0)
                    ChangeState(PlayerMachineStates.JUMP);
                break;

            default:
                break;
        }
    }

    private void Crouch(InputAction.CallbackContext context)
    {
        switch (m_CurrentState)
        {
            case PlayerMachineStates.IDLE:
                ChangeState(PlayerMachineStates.CROUCH);
                break;

            case PlayerMachineStates.WALK:
                ChangeState(PlayerMachineStates.CROUCH);
                break;

            case PlayerMachineStates.ATTACK1:
                ChangeState(PlayerMachineStates.CROUCH);
                break;

            case PlayerMachineStates.ATTACK2:
                ChangeState(PlayerMachineStates.CROUCH);
                break;

            default:
                break;
        }
    }
    /* !!!!!!! FINISHING ACTIONS !!!!!!! */
}

