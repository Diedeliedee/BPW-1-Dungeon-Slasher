using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected Hurtbox[] m_hurtboxes = null;
    protected List<IDamagable> m_caughtDamagables = new();

    private void Awake()
    {
        m_hurtboxes = GetComponentsInChildren<Hurtbox>(true);
        foreach (var _hurtbox in m_hurtboxes)
        {
            _hurtbox.onHit += OnHitDamagable;
        }
    }

    private void OnHitDamagable(IDamagable _damagable, int _damage)
    {
        if (m_caughtDamagables.Contains(_damagable)) return;

        _damagable.Damage(_damage);
        m_caughtDamagables.Add(_damagable);
    }

    
}
