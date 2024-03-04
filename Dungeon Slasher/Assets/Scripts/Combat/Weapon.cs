using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected UnityEvent m_onAttackStart;
    [SerializeField] protected UnityEvent m_onAttackHit;
    [SerializeField] protected UnityEvent m_onAttackEnd;

    //  Composition:
    protected Hurtbox[] m_hurtboxes = null;

    //  Aggregation:
    protected List<IDamagable> m_caughtDamagables = new();
    protected UnityEvent m_AttackEndCallbak = new();

    protected virtual void Awake()
    {
        //  Register the hit events by all child hurtboxes.
        m_hurtboxes = GetComponentsInChildren<Hurtbox>(true);
        foreach (var _hurtbox in m_hurtboxes)
        {
            _hurtbox.onHit += OnHitDamagable;
        }
    }

    protected virtual void OnHitDamagable(IDamagable _damagable, int _damage, Vector3 _velocity)
    {
        //  Guard clause.
        if (m_caughtDamagables.Contains(_damagable)) return;

        //  Creating attack data container.
        var instance = new AttackInstance();
        instance.attackVelocity = _velocity;
        instance.damage = _damage;
        instance.onAttackEnd = m_AttackEndCallbak;

        //  Passing data container to interface.
        _damagable.Damage(instance);

        //  Calling corresponding event.
        m_onAttackHit.Invoke();

        //  Adding damagable to the culling list.
        m_caughtDamagables.Add(_damagable);
    }

    public virtual void StartAttack()
    {
        m_onAttackStart.Invoke();
    }

    public virtual void EndAttack()
    {
        m_onAttackEnd.Invoke();
        m_AttackEndCallbak.Invoke();

        m_caughtDamagables.Clear();
        m_AttackEndCallbak.RemoveAllListeners();
    }
}
