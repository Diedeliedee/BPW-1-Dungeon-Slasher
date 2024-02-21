using Joeri.Tools.Gameify;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealthHandler : HealthHandler
{
    [SerializeField] private UnityEvent m_onStunned;
    [Space]
    [SerializeField] private Animator m_animator = null;

    private AttackInstance m_lastHitAttackInstance = null;

    public override void Damage(AttackInstance _instance)
    {
        m_lastHitAttackInstance = _instance;
        _instance.onAttackEnd.AddListener(OnStunComplete);

        m_animator.Play("Stunned", -1, 0f);
        m_onStunned.Invoke();
    }

    private void OnStunComplete()
    {
        m_animator.Play("Hurt", -1, 0f);
        m_health.ChangeHealth(-m_lastHitAttackInstance.damage);
    }
}
