using Joeri.Tools.Structure.StateMachine.Advanced;
using Joeri.Tools.Utilities;
using UnityEngine;

public partial class Player : MonoBehaviour
{
    [SerializeField] private float m_speed = 10f;
    [SerializeField] private float m_grip = 20f;

    [SerializeField] private float m_attackThing1 = 10f;
    [SerializeField] private float m_attackThing2 = 10f;
    [SerializeField] private string m_attackLeftName = "Swing Left";
    [SerializeField] private string m_attackRightName = "Swing Right";

    private CompositeFSM<Player> m_stateMachine = null;

    private PlayerMovement m_movement = null;
    private PlayerRotation m_rotation = null;
    private CombatHandler m_combat = null;

    private Animator m_animator = null;
    private InputReader m_input = null;

    private const float m_offsetAngle = 45f;

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

    private void Awake()
    {
        m_movement  = GetComponent<PlayerMovement>();
        m_rotation  = GetComponent<PlayerRotation>();
        m_combat    = GetComponent<CombatHandler>();
        m_animator  = GetComponent<Animator>();

        m_input = FindObjectOfType<InputReader>();
    }

    private void Start()
    {
        m_stateMachine = new(this,

            new State<Player>(new FreeMove(), new(
                new Condition(() => m_combat.ConfirmBuffer(), typeof(Attack)))),

            new State<Player>(new Attack(), new(
                new Condition(() => m_input.attackInput, typeof(AttackRight)),
                new Condition(() => StateBeyondMark(0.25f), typeof(FreeMove)))),

            new State<Player>(new AttackRight(), new(
                new Condition(() => m_input.attackInput, typeof(Attack)),
                new Condition(() => StateBeyondMark(0.25f), typeof(FreeMove)))));

    }

    private void Update()
    {
        m_stateMachine.Tick();
    }

    private bool StateBeyondMark(float _mark)
    {
        var state = m_animator.GetCurrentAnimatorStateInfo(0);

        return state.normalizedTime * state.length >= _mark;
    }
}
