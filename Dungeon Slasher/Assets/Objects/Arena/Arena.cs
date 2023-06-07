using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools;

public class Arena : MonoBehaviour
{
    [Header("Properties:")]
    [SerializeField] private float m_minSpawnTime = 1f;
    [SerializeField] private float m_maxSpawnTime = 1f;

    [Header("Reference")]
    [SerializeField] private ArenaActivator[] m_activators;
    [SerializeField] private Blockade[] m_blockades;
    [SerializeField] private Transform[] m_spawnPoints;

    private Timer m_spawnTimer = null;
    private State m_state = State.Dormant;

    private void Start()
    {
        foreach (var activator in m_activators) activator.Setup(StartFight);
        foreach (var blockade in m_blockades) blockade.Setup();
    }

    private void StartFight()
    {
        foreach (var blockade in m_blockades) blockade.Rise();

        m_spawnTimer = new Timer(Random.Range(m_minSpawnTime, m_maxSpawnTime));
        m_state = State.Acitve;
    }

    private void Update()
    {
        var deltaTime = Time.deltaTime;

        if (m_state != State.Acitve) return;
        if (!m_spawnTimer.HasReached(deltaTime)) return;

        //  Spawn enemy
        m_spawnTimer.Reset(Random.Range(m_minSpawnTime, m_maxSpawnTime));
    }

    private void FinishFight()
    {
        foreach (var blockade in m_blockades) blockade.Fall();
        m_state = State.Beaten;
    }

    public enum State { Dormant, Acitve, Beaten }
}
