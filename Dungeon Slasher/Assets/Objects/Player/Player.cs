using Joeri.Tools.Patterns;
using Joeri.Tools.Structure.StateMachine.Advanced;
using Joeri.Tools.Utilities;
using UnityEngine;
using UnityEngine.UI;

public partial class Player : MonoBehaviour
{
    [SerializeField] private PlayerSettings m_settings;

    //  Components:
    private CompositeFSM<Player> m_stateMachine = null;

    private PlayerMovement m_movement = null;
    private PlayerRotation m_rotation = null;
    private CombatHandler m_combat = null;

    //  Reference:
    private Animator m_animator = null;
    private InputReader m_input = null;

    //  Constants:
    private const float m_offsetAngle = 45f;

    #region Properties:
    public Vector2 position
    {
        get => transform.position.Planar();
        set => transform.position = new Vector3(value.x, transform.position.y, value.y);
    }
    public float angle
    {
        get => transform.eulerAngles.y;
        set => transform.rotation = Quaternion.Euler(0f, value, 0f);
    }
    public Vector2 forward
    {
        get => transform.forward.Planar();
        set => transform.rotation = Quaternion.LookRotation(value.Cubular(), Vector3.up);
    }
    #endregion

    #region Accessors
    public PlayerConfiguration configuration { get; set; }
    public PlayerSettings settings => m_settings;
    public PlayerMovement movement => m_movement;
    #endregion

    private void Awake()
    {
        m_movement  = GetComponent<PlayerMovement>();
        m_rotation  = GetComponent<PlayerRotation>();
        m_combat    = GetComponent<CombatHandler>();
        m_animator  = GetComponent<Animator>();
        m_input     = ServiceLocator.instance.Get<InputReader>("Input Reader");

        // Setting configuration class.
        configuration                   = new();
        configuration.speed             = m_settings.speed;
        configuration.leftAttackState   = typeof(StartupLeft);
        configuration.rightAttackState  = typeof(StartupRight);
    }

    private void Start()
    {
        m_stateMachine = new(this,

            new State<Player>(new FreeMove(), new(
                new FlexibleCondition(() => m_combat.ConfirmBuffer(), () => configuration.leftAttackState))),

            new State<Player>(new StartupLeft(), new(
                new SimpleCondition(() => AnimationAt(1f), typeof(AttackLeft)))),
            new State<Player>(new AttackLeft(), new(
                new SimpleCondition(() => AnimationAt(1f), typeof(RecoverLeft)))),
            new State<Player>(new RecoverLeft(), new(
                new FlexibleCondition(() => m_combat.ConfirmBuffer(), () => configuration.rightAttackState),
                new SimpleCondition(() => AnimationAt(0.333f), typeof(FreeMove)))),

            new State<Player>(new StartupRight(), new(
                new SimpleCondition(() => AnimationAt(1f), typeof(AttackRight)))),
            new State<Player>(new AttackRight(), new(
                new SimpleCondition(() => AnimationAt(1f), typeof(RecoverRight)))),
            new State<Player>(new RecoverRight(), new(
                new FlexibleCondition(() => m_combat.ConfirmBuffer(), () => configuration.leftAttackState),
                new SimpleCondition(() => AnimationAt(0.333f), typeof(FreeMove)))));
    }

    private void Update()
    {
        m_stateMachine.Tick();
    }

    private bool AnimationPlaying(string _name)
    {
        return m_animator.GetCurrentAnimatorStateInfo(0).IsName(_name);
    }

    private bool AnimationAt(float _mark)
    {
        return m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > _mark;
    }
}
