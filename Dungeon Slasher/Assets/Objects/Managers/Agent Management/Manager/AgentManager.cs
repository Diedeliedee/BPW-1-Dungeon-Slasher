using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dodelie.Tools;

namespace DungeonSlasher.Agents.Management
{
    public partial class AgentMananager : MonoBehaviour
    {
        [SerializeField] private Player m_player = null;

        public Player player { get => m_player; }

        /// <summary>
        /// Initializes all the agents in the scene.
        /// </summary>
        public void Initialize()
        {
            player.Initialize();
            InitializeNPCPools();
        }

        public void Tick(float deltaTime)
        {
            player.Tick(deltaTime);
            foreach (KeyValuePair<NPCType, ObjectPool<NPC>> pair in m_pools)
            {
                for (int i = 0; i < pair.Value.activeItems.Count; i++)
                {
                    pair.Value.activeItems[i].Tick(deltaTime);
                }
            }
        }
    }
}
