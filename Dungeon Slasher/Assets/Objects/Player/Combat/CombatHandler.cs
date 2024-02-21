using UnityEngine;

public class CombatHandler : MonoBehaviour
{
    [SerializeField] private Weapon m_weapon;

    private InputReader m_input = null;
    private bool m_inptBuffer = false;

    private void Awake()
    {
        m_input = FindObjectOfType<InputReader>();
    }

    private void Update()
    {
        m_inptBuffer = m_input.attackInput;
    }

    /// <summary>
    /// Functions called by the animator, at a point that the attack sequence is supposed to start.
    /// </summary>
    public virtual void StartAttack()
    {
        m_weapon.StartAttack();
    }

    /// <summary>
    /// Functions called by the animator, at a point that the attack sequence is supposed to stop.
    /// </summary>
    public virtual void EndAttack()
    {
        m_weapon.EndAttack();
    }

    public bool ConfirmBuffer()
    {
        var buffer = m_inptBuffer;

        m_inptBuffer = false;
        return buffer;
    }

}