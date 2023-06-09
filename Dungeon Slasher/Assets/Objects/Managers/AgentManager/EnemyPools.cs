using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Joeri.Tools.Utilities;
using Joeri.Tools.ObjectPool;

public class EnemyPools : MonoBehaviour
{
    [SerializeField] private EnemyConcept[] m_concepts;

    private Dictionary<Enemy.Type, ObjectPool<Enemy>> m_pool;

    public void Setup()
    {
        m_pool = new Dictionary<Enemy.Type, ObjectPool<Enemy>>();

        for (int i = 0; i < m_concepts.Length; i++)
        {
            m_pool.Add(m_concepts[i].type, new ObjectPool<Enemy>(m_concepts[i].prefab, 5, true, transform));
        }
    }

    public Enemy SpawnEnemy(Enemy.Type type, Vector3 pos, Vector2 lookDir)
    {
        var rotation = Quaternion.Euler(0f, Vectors.VectorToAngle(lookDir), 0f);

        return m_pool[type].Spawn(pos, rotation);
    }

    public void DespawnEnemy(Enemy enemy)
    {
        m_pool[enemy.type].Despawn(enemy);
    }
}
