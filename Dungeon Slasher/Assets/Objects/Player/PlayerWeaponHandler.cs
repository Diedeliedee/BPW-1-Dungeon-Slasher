using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    [SerializeField] private GameObject m_slashEffect;
    [SerializeField] private Transform m_weaponPivot;

    protected Hurtbox[] m_hurtboxes = null;
    protected List<Instance> m_caughtDamagables = new();

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
        var alreadyCaught = Instance.PresentIn(m_caughtDamagables, _damagable);

        Instance.RegisterIn(m_caughtDamagables, _damagable, _damage);
        if (alreadyCaught) return;

        if (((MonoBehaviour)_damagable).TryGetComponent(out IStunnable _stunnable)) _stunnable.Stun();

        var spawnedSlashEffect = Instantiate(m_slashEffect, transform.position, Quaternion.identity).transform;
        spawnedSlashEffect.position = m_weaponPivot.position;
        spawnedSlashEffect.rotation = m_weaponPivot.rotation;
    }

    public void StartRegister()
    {

    }

    public void EndRegister()
    {
        foreach (var _instance in m_caughtDamagables)
        {
            _instance.damagable.Damage(_instance.damage);
        }
        m_caughtDamagables.Clear();
    }

    public class Instance
    {
        public IDamagable damagable = default;
        public int damage = 0;

        public Instance(IDamagable _damagable, int _damage)
        {
            damagable = _damagable;
            damage = _damage;
        }

        public static bool PresentIn(List<Instance> _list, IDamagable _damagable)
        {
            foreach (var _instance in _list)
            {
                if (_instance.damagable.GetHashCode() == _damagable.GetHashCode()) return true;
            }
            return false;
        }

        public static void RegisterIn(List<Instance> _list, IDamagable _damagable, int _damage)
        {
            foreach (var _instance in _list)
            {
                if (_instance.damagable.GetHashCode() == _damagable.GetHashCode() && _instance.damage < _damage)
                {
                    _instance.damage = _damage;
                    return;
                }
            }
            _list.Add(new Instance(_damagable, _damage));
        }
    }
}
