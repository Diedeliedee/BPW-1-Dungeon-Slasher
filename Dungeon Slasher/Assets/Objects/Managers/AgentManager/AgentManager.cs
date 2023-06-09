using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class AgentManager : MonoBehaviour
{
    [Header("Reference:")]
    [SerializeField] private Player m_player;
    [SerializeField] private EnemyPools m_enemyPools;

    private List<Enemy> m_activeEnemies = new List<Enemy>();

    public Player player { get => m_player; }

    public void Tick(float deltaTime)
    {
        m_player.Tick(deltaTime);
        for (int i = 0; i < m_activeEnemies.Count; i++)
        {
            m_activeEnemies[i].Tick(deltaTime);
        }
    }

    public Enemy SpawnEnemy(Enemy.Type type, Vector3 pos, Vector2 lookDir)
    {
        var spawnedEnemy = m_enemyPools.SpawnEnemy(type, pos, lookDir);
        m_activeEnemies.Add(spawnedEnemy);
        return spawnedEnemy;
    }

    public void DespawnEnemy(Enemy enemy)
    {
        m_activeEnemies.Remove(enemy);
        m_enemyPools.DespawnEnemy(enemy);
    }
}
