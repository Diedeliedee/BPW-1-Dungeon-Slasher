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

    public System.Action onEnemySpawned = null;
    public System.Action onEnemyDespawned = null;

    private Enemy m_spawnedEnemy = null;

    public bool occupied { get => m_spawnedEnemy != null; }

    public void Setup()
    {
        if (m_spawnOnStart)
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        if (occupied) return;

        m_spawnedEnemy = GameManager.instance.entities.SpawnEnemy(m_concept.type, transform.position, Vectors.VectorToFlat(transform.forward));
        m_spawnedEnemy.onDespawn += OnDespawn;

        onEnemySpawned?.Invoke();
        onEnemySpawned = null;
    }

    private void OnDespawn()
    {
        m_spawnedEnemy = null;

        onEnemyDespawned?.Invoke();
        onEnemyDespawned = null;
    }

    private void OnDrawGizmosSelected()
    {
        if (m_concept == null) return;

        var opacity = m_spawnedEnemy == null ? 0.5f : 0.1f;

        GizmoTools.DrawOutlinedDisc(transform.position, m_concept.radius, m_concept.color, Color.white, opacity);
    }
}
