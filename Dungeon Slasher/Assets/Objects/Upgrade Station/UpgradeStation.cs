using Joeri.Tools.Patterns;
using UnityEngine;

public class UpgradeStation : MonoBehaviour
{
    [SerializeField] private AbilityType m_type;

    private bool m_used = false;

    private Player m_player     = null;
    private Animator m_animator = null;

    private void Awake()
    {
        m_player    = ServiceLocator.instance.Get<Player>("Player");
        m_animator  = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_used) return;
        if (!other.TryGetComponent(out Player _player)) return;

        m_used = true;
        m_animator.Play("Use");
    }

    public void ApplyAbility()
    {
        m_player.health.Heal(m_player.health.maxHealth);
        m_player.abilities.ApplyAbility(m_type);
    }
}
