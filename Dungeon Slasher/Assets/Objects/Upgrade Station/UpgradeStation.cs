using Joeri.Tools.Patterns;
using Joeri.Tools.Utilities;
using UnityEngine;
using UnityEngine.Events;

public class UpgradeStation : MonoBehaviour
{
    [SerializeField] private AbilityType m_type;
    [SerializeField] private Transform m_spawnPoint;
    [SerializeField] private int m_id;
    [Space]
    [SerializeField] private UnityEvent<Vector2, float> m_onSaveActivated;
    private bool m_used = false;

    private Player m_player     = null;
    private Animator m_animator = null;

    private void Awake()
    {
        m_player    = ServiceLocator.instance.Get<Player>("Player");
        m_animator  = GetComponent<Animator>();

        //  Subscribe player to save event.
        m_onSaveActivated.AddListener(m_player.OnSave);
    }

    private void Start()
    {
        //  If the upgrade station has already been used, set it to inactive.
        if (!PlayerPrefs.HasKey($"station{m_id}Used")) return;

        m_used = true;
        m_animator.Play("Used");
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

        if (m_spawnPoint == null) return;

        //  Mark the station as used.
        PlayerPrefs.SetInt($"station{m_id}Used", 0);

        //  Call the save Event.
        m_onSaveActivated.Invoke(m_spawnPoint.position.Planar(), m_spawnPoint.eulerAngles.y);
        PlayerPrefs.Save();
    }
}
