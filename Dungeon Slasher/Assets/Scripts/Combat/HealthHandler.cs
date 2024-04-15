using Joeri.Tools.Gameify;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class HealthHandler : MonoBehaviour, IDamagable
{
    [SerializeField] protected Health m_health;
    [Space]
    [SerializeField] protected UnityEvent<int, int> m_onHealthChange;
    [SerializeField] protected UnityEvent<int, int> m_onDamage;
    [SerializeField] protected UnityEvent<int, int> m_onHeal;
    [SerializeField] protected UnityEvent m_onDeath;

    public int health => m_health.health;
    public int maxHealth => m_health.maxHealth;

    protected virtual void Awake()
    {
        m_health.onHealthChange += (_h, _m) => m_onHealthChange.Invoke(_h, _m);
        m_health.onDamage       += (_a, _m) => m_onDamage.Invoke(_a, _m);
        m_health.onHeal         += (_a, _m) => m_onHeal.Invoke(_a, _m);
        m_health.onDeath        += () => m_onDeath.Invoke();
    }

    protected virtual void OnDestroy()
    {
        m_health.onHealthChange = null;
        m_health.onDeath        = null;
    }

    public virtual HitCallback Damage(AttackInstance _instance)
    {
        m_health.ChangeHealth(-_instance.damage);
        return HitCallback.Hit;
    }

    public virtual void Heal(int _amount)
    {
        m_health.ChangeHealth(_amount);
    }
}
