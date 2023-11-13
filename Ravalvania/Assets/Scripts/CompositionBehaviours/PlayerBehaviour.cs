using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ravalvania
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(InputAction))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(MovableBehaviour))]
    [RequireComponent(typeof(HealthBehaviour))]
    [RequireComponent(typeof(HealableBehaviour))]
    [RequireComponent(typeof(ManaBehaviour))]
    [RequireComponent(typeof(ManaCostBehaviour))]
    [RequireComponent(typeof(ShootableBehaviour))]
    [RequireComponent(typeof(DamageableBehaviour))]
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

            //Player gameobject animator
            m_Animator = GetComponent<Animator>();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
