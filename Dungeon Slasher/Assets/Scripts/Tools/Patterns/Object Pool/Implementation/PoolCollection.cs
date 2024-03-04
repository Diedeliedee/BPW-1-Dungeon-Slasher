using System.Collections.Generic;
using UnityEngine;

namespace Joeri.Tools.Patterns.ObjectPool
{
    public class PoolCollection : MonoBehaviour
    {
        [SerializeField] private Association[] m_poolTypes;
        [Space]
        [SerializeField] private int m_groupSize = 5;

        private Dictionary<string, ObjectPool> m_pools = new();

        private void Awake()
        {
            for (int i = 0; i < m_poolTypes.Length; i++)
            {
                var pool = new ObjectPool(m_poolTypes[i].prefab, m_groupSize, transform, transform);
                var type = m_poolTypes[i].type;

                m_pools.Add(type, pool);
            }
        }

        public T Spawn<T>(string _type, Vector3 _position, Quaternion _rotation) where T : MonoBehaviour, IPoolItem
        {
            try
            {
                return m_pools[_type].Spawn<T>(_position, _rotation, null);
            }
            catch
            {
                Debug.Log($"The poolable object of type: {_type} does not seem to exist.", gameObject);
                return default;
            }
        }

        [System.Serializable]
        public class Association
        {
            public string type;
            public GameObject prefab;
        }
    }
}