using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ArenaSpawnHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] m_enemiesToSpawn;
    [SerializeField] private Transform m_spawnPointParent;
    [Space]
    [SerializeField] private UnityEvent<int> m_onEnemyKilled;

    private Transform[] m_spawnPoints       = null;
    private List<Shade> m_spawnedEnemies    = null; //  Hardcoded reference to shade for now.

    public int activeEnemies => m_spawnedEnemies.Count;

    private void Awake()
    {
        m_spawnPoints = m_spawnPointParent.GetComponentsInChildren<Transform>();
        m_spawnedEnemies = new();
    }

    public void SpawnRandomEnemy()
    {
        var randSpawn = m_spawnPoints[Random.Range(0, m_spawnPoints.Length)];
        var randEnemy = m_enemiesToSpawn[Random.Range(0, m_enemiesToSpawn.Length)];
        var spawnedEnemy = Instantiate(randEnemy, randSpawn.position, randSpawn.rotation, transform).GetComponent<Shade>();

        spawnedEnemy.onRequestDespawn.AddListener(DespawnEnemy);
        m_spawnedEnemies.Add(spawnedEnemy);
    }

    private void DespawnEnemy(Shade _enemy)
    {
        _enemy.onRequestDespawn.RemoveListener(DespawnEnemy);
        m_spawnedEnemies.Remove(_enemy);
        Destroy(_enemy);
        m_onEnemyKilled.Invoke(activeEnemies);
    }
}
