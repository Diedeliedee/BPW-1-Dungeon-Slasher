using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class AgentManager : MonoBehaviour
{
    [SerializeField] private Player m_player;
    [SerializeField] private NPC[] m_enemies = new NPC[1];

    public Player player { get => m_player; }

    /// <summary>
    /// Initializes all the agents in the scene.
    /// </summary>
    public void Setup()
    {
        m_player.Setup();
        for (int i = 0; i < m_enemies.Length; i++)
        {
            m_enemies[i].Setup();
            m_enemies[i].AssignPlayer(m_player);
        }
    }

    public void Tick(float deltaTime)
    {
        m_player.Tick(deltaTime);
        for (int i = 0; i < m_enemies.Length; i++)
        {
            m_enemies[i].Tick(deltaTime);
        }
    }
}
