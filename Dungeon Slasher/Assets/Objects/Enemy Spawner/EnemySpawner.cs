using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Joeri.Tools.Debugging;
using Joeri.Tools.Utilities;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyConcept m_concept;
    [Space]
    [SerializeField] private bool m_spawnOnStart = false;

    private Enemy m_spawnedEnemy = null;

    private void Start()
    {
        if (m_spawnOnStart)
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        if (m_spawnedEnemy != null)
        {
            return;
        }

        m_spawnedEnemy = GameManager.instance.agents.SpawnEnemy(m_concept.type, transform.position, Vectors.VectorToFlat(transform.forward));
        m_spawnedEnemy.onDespawn += OnDespawn;
    }

    private void OnDespawn()
    {
        m_spawnedEnemy = null;
    }

    private void OnDrawGizmosSelected()
    {
        if (m_concept == null) return;

        var opacity = m_spawnedEnemy == null ? 0.5f : 0.1f;

        GizmoTools.DrawOutlinedDisc(transform.position, m_concept.radius, m_concept.color, Color.white, opacity);
    }
}
