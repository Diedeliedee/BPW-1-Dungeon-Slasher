using UnityEngine;

public class HealthPoint : MonoBehaviour
{
    private bool filled         = true;

    private Animator m_animator = null;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    public void Fill()
    {
        if (filled) return;
        m_animator.Play("Filled");
        filled = true;
    }

    public void Deplete()
    {
        if (!filled) return;
        m_animator.Play("Depleted");
        filled = false;
    }
}
