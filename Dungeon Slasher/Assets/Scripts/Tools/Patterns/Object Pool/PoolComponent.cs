using Joeri.Tools.Patterns.ObjectPool;
using System.Collections.Generic;
using UnityEngine;

public class PoolComponent<T> : MonoBehaviour where T : MonoBehaviour, IPoolItem
{
    [SerializeField] private GameObject m_poolObject;
    [Space]
    [SerializeField] private int m_groupSize = 5;
    [SerializeField] private bool m_autoGrow = true;

    private ObjectPool<T> m_pool = null;

    private void Awake()
    {
        m_pool = new ObjectPool<T>(m_poolObject, m_groupSize, m_autoGrow, transform);
    }

    /// <summary>
    /// Spawns an item at the given location and rotation, under the passed in parent.
    /// </summary>
    public T Spawn(Vector3 _position, Quaternion _rotation, Transform _parent = null)
    {
        return m_pool.Spawn(_position, _rotation, _parent);
    }

    /// <summary>
    /// Despawns the passed in item.
    /// </summary>
    public T Despawn(T _item)
    {
        return m_pool.Despawn(_item);
    }

    public List<T> GetItems(bool _active, bool _inactive)
    {
        return m_pool.GetItems(_active, _inactive);
    }
}
