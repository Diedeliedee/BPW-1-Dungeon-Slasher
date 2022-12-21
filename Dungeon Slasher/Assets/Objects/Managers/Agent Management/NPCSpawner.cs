using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dodelie.Tools;

namespace DungeonSlasher.Agents.Management
{
    public class NPCSpawner : MonoBehaviour
    {
        [SerializeField] private NPCConcept npcToSpawn = null;

        private NPC m_spawnedNPC = null;

        public void Spawn()
        {
            m_spawnedNPC = GameManager.instance.agents.Spawn(npcToSpawn.type, transform.position, transform.rotation);
            m_spawnedNPC.onDespawn += OnDespawn;
        }

        private void OnDespawn()
        {
            m_spawnedNPC.onDespawn -= OnDespawn;
            m_spawnedNPC = null;
            StartCoroutine(CommonRoutines.WaitForSeconds(1f, Spawn));
        }
    }
}
