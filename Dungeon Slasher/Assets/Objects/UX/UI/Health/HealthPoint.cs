using UnityEngine;

public class HealthPoint : MonoBehaviour
{
    private bool m_filled = true;

    private Animator m_animator = null;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    public void Fill()
    {
        if (m_filled) return;
        m_animator.Play("Filled");
        m_filled = true;
    }

    public void Deplete()
    {
        if (!m_filled) return;
        m_animator.Play("Depleted");
        m_filled = false;
    }
}
