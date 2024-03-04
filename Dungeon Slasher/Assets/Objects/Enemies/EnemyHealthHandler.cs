using UnityEngine;
using UnityEngine.Events;

public class EnemyHealthHandler : HealthHandler
{
    [SerializeField] private UnityEvent m_onStunned;
    [SerializeField] private UnityEvent m_onFlabbergasted;
    [Space]
    [SerializeField] private Element m_element = Element.None;

    private AttackInstance m_lastHitAttackInstance = null;

    public override HitCallback Damage(AttackInstance _instance)
    {
        if (_instance.element == m_element)
        {
            m_lastHitAttackInstance = _instance;
            m_lastHitAttackInstance.onAttackEnd.AddListener(OnStunComplete);

            m_onStunned.Invoke();
            return HitCallback.Hit;
        }
        else
        {
            m_health.ChangeHealth(-_instance.damage / 2);

            m_onFlabbergasted.Invoke();
            return HitCallback.Fumbled;
        }
    }

    private void OnStunComplete()
    {
        m_health.ChangeHealth(-m_lastHitAttackInstance.damage);
        m_lastHitAttackInstance = null;
    }
}
