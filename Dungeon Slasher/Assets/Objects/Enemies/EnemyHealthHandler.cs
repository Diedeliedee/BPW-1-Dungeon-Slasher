using Joeri.Tools.Gameify;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealthHandler : HealthHandler
{
    [SerializeField] private UnityEvent m_onStunned;

    private AttackInstance m_lastHitAttackInstance = null;

    public override void Damage(AttackInstance _instance)
    {
        m_lastHitAttackInstance = _instance;
        _instance.onAttackEnd.AddListener(OnStunComplete);

        m_onStunned.Invoke();
    }

    private void OnStunComplete()
    {
        m_health.ChangeHealth(-m_lastHitAttackInstance.damage);
        m_lastHitAttackInstance = null;
    }
}
