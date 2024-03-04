using Joeri.Tools.Patterns.ObjectPool;
using Joeri.Tools.Patterns.ServiceLocator;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ArenaSpawnHandler : MonoBehaviour
{
    [SerializeField] private string[] m_enemiesToSpawn;
    [SerializeField] private Transform m_spawnPointParent;
    [Space]
    [SerializeField] private UnityEvent<int> m_onEnemyKilled;

    private Transform[] m_spawnPoints           = null;
    private PoolCollection m_enemyCollection    = null;
    private List<Shade> m_spawnedEnemies        = null; //  Hardcoded reference to shade for now.

    public int activeEnemies => m_spawnedEnemies.Count;

    private void Awake()
    {
        m_spawnPoints = m_spawnPointParent.GetComponentsInChildren<Transform>();
        m_enemyCollection = ServiceLocator.instance.Get<PoolCollection>("Enemy Collection");
        m_spawnedEnemies = new();
    }

    public void SpawnRandomEnemy()
    {
        var randSpawn       = m_spawnPoints[Random.Range(0, m_spawnPoints.Length)];
        var randEnemy       = m_enemiesToSpawn[Random.Range(0, m_enemiesToSpawn.Length)];
        var spawnedEnemy    = m_enemyCollection.Spawn<Shade>(randEnemy, randSpawn.position, randSpawn.rotation);

        spawnedEnemy.onDespawn.AddListener(OnEnemyDespawn);
        m_spawnedEnemies.Add(spawnedEnemy);
    }

    private void OnEnemyDespawn(Shade _enemy)
    {
        _enemy.onDespawn.RemoveListener(OnEnemyDespawn);
        m_spawnedEnemies.Remove(_enemy);
        m_onEnemyKilled.Invoke(activeEnemies);
    }
}
