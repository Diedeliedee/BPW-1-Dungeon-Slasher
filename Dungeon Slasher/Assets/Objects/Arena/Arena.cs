using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools;

public class Arena : MonoBehaviour
{
    [Header("Properties:")]
    [SerializeField] private float m_minSpawnTime = 1f;
    [SerializeField] private float m_maxSpawnTime = 1f;
    [SerializeField] private int m_maxEnemies = 3;

    [Header("Reference")]
    [SerializeField] private ArenaActivator[] m_activators;
    [SerializeField] private Blockade[] m_blockades;
    [SerializeField] private EnemySpawner[] m_spawnPoints;

    private Timer m_spawnTimer = null;
    private State m_state = State.Dormant;
    private int m_spawnedEnemies = 0;

    public void Setup()
    {
        foreach (var activator in m_activators) activator.Setup(StartFight);
        foreach (var blockade in m_blockades) blockade.Setup();
    }

    private void StartFight()
    {
        foreach (var blockade in m_blockades) blockade.Rise();

        m_spawnTimer = new Timer(Random.Range(m_minSpawnTime, m_maxSpawnTime));
        m_state = State.Active;
    }

    public void Tick(float deltaTime)
    {
        //  Return if the arena isn't active.
        if (m_state != State.Active) return;

        //  Return if it's not the time to take action yet.
        if (!m_spawnTimer.HasReached(deltaTime)) return;

        //  Only spawn an enemy if a spawnpoint is available.
        //  Caution: There is a better way to manage this, but for now this is fine.
        EnemySpawner spawner = null;
        for (int i = 0; i < m_spawnPoints.Length; i++)
        {
            if (m_spawnPoints[i].occupied) continue;
            spawner = m_spawnPoints[i];
        }
        if (spawner == null) return;

        //  Spawn the enemy.
        spawner.Spawn();
        m_spawnTimer.Reset(Random.Range(m_minSpawnTime, m_maxSpawnTime));
        m_spawnedEnemies++;

        //  If this is the last enemy to be spawned, pend for the end of the fight.
        if (m_spawnedEnemies >= m_maxEnemies)
        {
            m_state = State.Pending;
            spawner.onEnemyDespawned += FinishFight;
        }
    }

    private void FinishFight()
    {
        foreach (var blockade in m_blockades) blockade.Fall();
        m_state = State.Beaten;
    }

    public enum State { Dormant, Active, Pending, Beaten }
}
