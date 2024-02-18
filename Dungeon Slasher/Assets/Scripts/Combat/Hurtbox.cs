using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Hurtbox : MonoBehaviour
{
    [SerializeField] private int m_damage = 1;

    public void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out IDamagable _damagable)) return;
        _damagable.Damage(m_damage);
    }
}