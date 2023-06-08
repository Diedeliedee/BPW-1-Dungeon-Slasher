using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class AgentManager : MonoBehaviour
{
    [Header("Components:")]
    [SerializeField] private Spawning m_spawning;

    [Header("Reference:")]
    [SerializeField] private Player m_player;


    private List<NPC> m_enemies = new List<NPC>();

    public Player player { get => m_player; }

    public void Setup()
    {
        m_player.Setup();
        for (int i = 0; i < m_enemies.Count; i++)
        {
            m_enemies[i].Setup();
            m_enemies[i].AssignPlayer(m_player);
        }
    }

    public void Tick(float deltaTime)
    {
        m_player.Tick(deltaTime);
        for (int i = 0; i < m_enemies.Count; i++)
        {
            m_enemies[i].Tick(deltaTime);
        }
    }

    public NPC SpawnEnemy(Vector3 pos, Vector2 lookDir)
    {
        var spawnedEnemy = m_spawning.SpawnEnemy(pos, lookDir, ref m_enemies);

        spawnedEnemy.AssignPlayer(m_player);
        spawnedEnemy.Setup();

        m_enemies.Add(spawnedEnemy);
        return spawnedEnemy;
    }

    public void DespawnEnemy(NPC enemy)
    {
        m_enemies.Remove(enemy);
        Destroy(enemy.gameObject);
    }
}
