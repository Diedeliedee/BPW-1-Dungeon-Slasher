using System.Collections.Generic;
using UnityEngine;
using Dodelie.Tools;
using System.Linq;

namespace DungeonSlasher.Agents.Management
{
    public partial class AgentMananager
    {
        [SerializeField] private NPCType[] m_npcTypes = new NPCType[1];

        private Dictionary<NPCType, ObjectPool<NPC>> m_pools = new Dictionary<NPCType, ObjectPool<NPC>>();

        /// <returns>All active NPC's in the scene.</returns>
        public List<NPC> npcs
        {
            get
            {
                var npcList = new List<NPC>();

                foreach (KeyValuePair<NPCType, ObjectPool<NPC>> pair in m_pools) npcList.AddRange(pair.Value.GetItems(true, false));
                return npcList;
            }
        }

        /// <summary>
        /// Creates the object pools for all NPC's.
        /// </summary>
        private void InitializeNPCPools()
        {
            for (int i = 0; i < m_npcTypes.Length; i++)
            {
                m_pools.Add(m_npcTypes[i], new ObjectPool<NPC>(m_npcTypes[i].prefab, 5, true, transform));
            }
        }
    }

}
