using System.Collections.Generic;
using UnityEngine;
using Dodelie.Tools;
using System.Linq;

namespace DungeonSlasher.Agents.Management
{
    public partial class AgentManager
    {
        [SerializeField] private NPCConcept[] m_npcTypes = new NPCConcept[1];

        private Dictionary<NPCConcept.Type, ObjectPool<NPC>> m_pools = new Dictionary<NPCConcept.Type, ObjectPool<NPC>>();

        /// <returns>All active NPC's in the scene.</returns>
        public List<NPC> npcs
        {
            get
            {
                var npcList = new List<NPC>();

                foreach (KeyValuePair<NPCConcept.Type, ObjectPool<NPC>> pair in m_pools) npcList.AddRange(pair.Value.GetItems(true, false));
                return npcList;
            }
        }

        /// <summary>
        /// Returns the given npc back to their object pool.
        /// </summary>
        public NPC TakeBack(NPCConcept.Type type, NPC npc)
        {
            return m_pools[type].Despawn(npc);
        }

        /// <summary>
        /// Creates the object pools for all NPC's.
        /// </summary>
        private void InitializeNPCPools()
        {
            for (int i = 0; i < m_npcTypes.Length; i++)
            {
                m_pools.Add(m_npcTypes[i].type, new ObjectPool<NPC>(m_npcTypes[i].prefab, 5, true, transform));
            }
        }
    }

}
