using Joeri.Tools.Gameify;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class HealthHandler : MonoBehaviour, IDamagable
{
    [SerializeField] private Health m_health;
    [Space]
    [SerializeField] private UnityEvent<int, int> m_onHealthChange;
    [SerializeField] private UnityEvent m_onDeath;

    public int health => m_health.health;
    public int maxHealth => m_health.maxHealth;

    private void Awake()
    {
        m_health.onHealthChange += (h, m) => m_onHealthChange.Invoke(h, m);
        m_health.onDeath        += () => m_onDeath.Invoke();
    }

    private void OnDestroy()
    {
        m_health.onHealthChange = null;
        m_health.onDeath        = null;
    }

    public void Damage(int _damage)
    {
        m_health.ChangeHealth(-_damage);
    }
}
