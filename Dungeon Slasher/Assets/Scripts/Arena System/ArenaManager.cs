using Joeri.Tools;
using UnityEngine;
using UnityEngine.Events;

public class ArenaManager : MonoBehaviour
{
    [SerializeField] private int m_totalEnemies = 10;
    [SerializeField] private int m_maxEnemies   = 3;
    [SerializeField] private float m_spawnTime  = 1.0f;
    [Space]
    [SerializeField] private UnityEvent m_onBattleStart;
    [SerializeField] private UnityEvent m_onBattleOver;
    [Space]
    [SerializeField] private int m_id = 0;

    //  Reference:
    private ArenaSpawnHandler m_spawner = null;
    private Timer m_spawnTimer          = null;

    // Run-time:
    private State m_state           = State.Ready;
    private int m_remainingEnemies  = 0;

    private void Awake()
    {
        m_spawner       = GetComponentInChildren<ArenaSpawnHandler>();
        m_spawnTimer    = new Timer(m_spawnTime);
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey($"arena{m_id}Beat.")) return;

        m_state = State.Over;
    }

    private void Update()
    {
        if (m_state != State.Running) return;

        if (m_spawner.activeEnemies < m_maxEnemies && m_spawner.activeEnemies < m_remainingEnemies && m_spawnTimer.ResetOnReach(Time.deltaTime))
        {
            m_spawner.SpawnRandomEnemy();
        }
    }

    public void StartFight()
    {
        //  Return if the arena is not in the first stage.
        if (m_state != State.Ready) return;

        m_state             = State.Running;
        m_remainingEnemies  = m_totalEnemies;

        m_spawner.SpawnRandomEnemy();
        m_onBattleStart.Invoke();
    }

    public void OnEnemyDefeated(int _livingEnemies)
    {
        --m_remainingEnemies;
        if (m_remainingEnemies == 0)
        {
            //  Mark the arena as beaten.
            m_state = State.Over;
            PlayerPrefs.SetInt($"arena{m_id}Beat.", 1);

            //  Invoke that the battle is over.
            m_onBattleOver.Invoke();
        }
    }

    private enum State
    {
        Ready,
        Running,
        Over
    }
}