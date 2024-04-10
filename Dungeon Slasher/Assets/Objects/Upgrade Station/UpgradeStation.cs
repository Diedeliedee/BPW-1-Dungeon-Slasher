using UnityEngine;

public class UpgradeStation : MonoBehaviour
{
    [SerializeField] private AbilityType m_type;

    private AbilityManager m_abilityContact = null;
    private bool m_used                     = false;

    private Animator m_animator = null;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_used) return;
        if (!other.TryGetComponent(out AbilityManager _playerAbility)) return;

        m_abilityContact    = _playerAbility;
        m_used              = true;
        m_animator.Play("Use");
    }

    public void ApplyAbility()
    {
        m_abilityContact.ApplyAbility(m_type);
        m_abilityContact = null;
    }
}
