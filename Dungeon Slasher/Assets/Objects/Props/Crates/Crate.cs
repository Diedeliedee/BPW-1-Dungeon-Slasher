using UnityEngine;

public class Crate : MonoBehaviour, IDamagable
{
    [SerializeField] private Element m_element;

    private Animator m_animator = null;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    public HitCallback Damage(AttackInstance _instance)
    {
        if (_instance.element == m_element)
        {
            m_animator.Play("Damage");
            _instance.onAttackEnd.AddListener(() => m_animator.Play("Destroy"));
            return HitCallback.Hit;
        }
        else
        {
            m_animator.Play("Damage");
            return HitCallback.Fumbled;
        }
    }

    public void DeleteCrate()
    {
        Destroy(gameObject);
    }
}
