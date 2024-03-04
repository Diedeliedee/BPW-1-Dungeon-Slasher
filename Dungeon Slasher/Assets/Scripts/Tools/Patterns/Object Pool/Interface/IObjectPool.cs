using UnityEngine;

namespace Joeri.Tools.Patterns.ObjectPool
{
    public interface IObjectPool
    {
        public T Spawn<T>(Vector3 _position, Quaternion _rotation, Transform _parent = null) where T : MonoBehaviour, IPoolItem;

        public void Despawn(IPoolItem _item);
    }
}