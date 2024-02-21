using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Hurtbox : MonoBehaviour
{
    [SerializeField] private int m_damage = 1;

    public System.Action<IDamagable, int> onHit;

    public void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out IDamagable _damagable)) return;
        onHit?.Invoke(_damagable, m_damage);
    }
}