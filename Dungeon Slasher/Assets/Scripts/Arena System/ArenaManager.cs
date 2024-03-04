

using Joeri.Tools;
using UnityEngine;
using UnityEngine.Events;

public class ArenaManager : MonoBehaviour
{
    [SerializeField] private int m_totalEnemies = 10;
    [SerializeField] private int m_maxEnemies = 3;
    [SerializeField] private float m_spawnTime = 1.0f;
    [Space]
    [SerializeField] private UnityEvent m_onBattleStart;
    [SerializeField] private UnityEvent m_onBattleOver;

    //  Reference:
    private ArenaSpawnHandler m_spawner = null;
    private Timer m_spawnTimer = null;

    // Run-time:
    private State m_state = State.Ready;
    private int m_remainingEnemies = 0;

    private void Awake()
    {
        m_spawner = GetComponentInChildren<ArenaSpawnHandler>();
        m_spawnTimer = new Timer(m_spawnTime);
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
        m_state = State.Running;
        m_remainingEnemies = m_totalEnemies;
        m_spawner.SpawnRandomEnemy();
        m_onBattleStart.Invoke();
    }

    public void OnEnemyDefeated(int _livingEnemies)
    {
        --m_remainingEnemies;
        if (m_remainingEnemies == 0)
        {
            m_state = State.Over;
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