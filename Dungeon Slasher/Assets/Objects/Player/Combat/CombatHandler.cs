using UnityEngine;

public class CombatHandler : MonoBehaviour
{
    [SerializeField] private Weapon m_weapon;

    private InputReader m_input = null;

    private bool m_active = false;
    private bool m_inputBuffer = false;

    public bool active => m_active;

    private void Awake()
    {
        m_input = FindObjectOfType<InputReader>();
    }

    private void Update()
    {
        if (m_input.attackInput) m_inputBuffer = true;
    }

    /// <summary>
    /// Functions called by the animator, at a point that the attack sequence is supposed to start.
    /// </summary>
    public virtual void StartAttack()
    {
        m_active = true;
        m_weapon.StartAttack();
    }

    /// <summary>
    /// Functions called by the animator, at a point that the attack sequence is supposed to stop.
    /// </summary>
    public virtual void EndAttack()
    {
        m_active = false;
        m_weapon.EndAttack();
    }

    public bool ConfirmBuffer()
    {
        var buffer = m_inputBuffer;

        m_inputBuffer = false;
        return buffer;
    }
}