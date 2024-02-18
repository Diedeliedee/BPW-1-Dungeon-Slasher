using Joeri.Tools.AI.BehaviorTree;
using Joeri.Tools.Debugging;
using Joeri.Tools.Patterns;
using Joeri.Tools.Utilities;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Shade : MonoBehaviour
{
    [SerializeField] private float m_stalkSpeed = 1f;
    [SerializeField] private float m_chargeSpeed = 3f;
    [SerializeField] private float m_attackVelocity = 3f;
    [SerializeField] private float m_attackTime = 3f;
    [Space]
    [SerializeField] private float m_stalkRange = 15f;
    [SerializeField] private float m_chaseRange = 5f;
    [SerializeField] private float m_attackRange = 2f;
    [Space]
    [SerializeField] private float m_stalkIncrementation = 0.5f;
    [SerializeField] private float m_stalkRotationDegrees = 30f;

    private BehaviorTree m_tree = null;
    private NavMeshAgent m_agent = null;
    private Animator m_animator = null;

    private Player m_player = null;

    private SelfMemory m_selfMemory = new();
    private TargetMemory m_targetMemory = new();
    private TimeMemory m_timeMemory = new();

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
        m_agent = GetComponent<NavMeshAgent>();
        m_animator = GetComponent<Animator>();

        m_player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        var blackboard = new FittedBlackboard();

        blackboard.Add(m_agent);
        blackboard.Add(m_animator);
        blackboard.Add(m_selfMemory);
        blackboard.Add(m_targetMemory);
        blackboard.Add(m_timeMemory);

        m_agent.updateRotation = false;

        m_tree = new BehaviorTree(
            new Selector(
                new Sequence(
                    new IsAnimationPlaying("Attack"),
                    new IsAnimationPlaying("Dying"),
                    new Wait("Unable to do anything..")),
                new Sequence(
                    new PrioritizeSucces(
                        new Selector(
                            new Sequence(
                                new InRangeOf(m_player.transform, m_attackRange, "Player within attack range?"),
                                new Action(() => m_animator.Play("Attack"), "Initiating attack!")),
                            new Sequence(
                                new InRangeOf(m_player.transform, m_chaseRange, "Player within chase range?"),
                                new SetTarget(m_player.transform),
                                new NavigateToTarget("Running in for the attack!")),
                            new Sequence(
                                new InRangeOf(m_player.transform, m_stalkRange, "Player within stalk range?"),
                                new Routine(
                                    new Action(() => m_targetMemory.SetTarget(GetNewStalkPosition().Cubular())),
                                    new IterateToTarget("Stalking Player.."))))),
                    new Action(() => forward = m_player.position - position)),
                new Wait("Idle.")));

        m_tree.PassBlackboard(blackboard);
    }

    private void Update()
    {
        m_selfMemory.Update(transform, m_agent.velocity);
        m_timeMemory.deltaTime = Time.deltaTime;
        m_tree.Tick();
    }

    private Vector2 GetNewStalkPosition()
    {
        var turn = m_stalkRotationDegrees / 2f;
        var distance = Vector2.Distance(position, m_player.position) * m_stalkIncrementation;
        var direction = (position - m_player.position).normalized.Rotate(Random.Range(-turn, turn));

        return m_player.position + direction * distance;
    }

    private void OnDrawGizmosSelected()
    {
        GizmoTools.DrawCircle(transform.position, m_stalkRange, Color.white, 0.25f);
        GizmoTools.DrawOutlinedDisc(transform.position, m_chaseRange, Color.gray, Color.white, 0.25f);
        GizmoTools.DrawOutlinedDisc(transform.position, m_attackRange, Color.red, Color.white, 0.25f);

        if (!Application.isPlaying) return;

        m_tree.Draw(transform.position + Vector3.up * m_agent.height);
    }

}
