using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonSlasher.Agents.Management
{
    public partial class AgentManager
    {
        [SerializeField] private NPCSpawner[] m_constantSpawners = new NPCSpawner[1];

        public NPC Spawn(NPCConcept.Type type, Vector3 position, Quaternion rotation)
        {
            var newNPC = m_pools[type].Spawn(position, rotation);

            newNPC.Initialize();
            return newNPC;
        }
    }
}