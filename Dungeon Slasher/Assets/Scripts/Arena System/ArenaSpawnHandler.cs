using Joeri.Tools.Patterns;
using Joeri.Tools.Utilities.SpawnManager;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ArenaSpawnHandler : MonoBehaviour
{
    [SerializeField] private string[] m_enemiesToSpawn;
    [SerializeField] private Transform m_spawnPointParent;
    [Space]
    [SerializeField] private UnityEvent<int> m_onEnemyKilled;

    private List<Transform> m_spawnPoints   = null;
    private ISpawnManager m_enemyCollection = null;
    private List<Shade> m_spawnedEnemies    = null; //  Hardcoded reference to shade for now.

    public int activeEnemies => m_spawnedEnemies.Count;

    private void Awake()
    {
        //  Getting spawn points.
        m_spawnPoints = m_spawnPointParent.GetComponentsInChildren<Transform>().ToList();
        m_spawnPoints.Remove(m_spawnPointParent);

        m_enemyCollection   = ServiceLocator.instance.Get<ISpawnManager>("Enemy Collection");
        m_spawnedEnemies    = new();
    }

    public void SpawnRandomEnemy()
    {
        var randSpawn       = m_spawnPoints[Random.Range(0, m_spawnPoints.Count)];
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
