using Joeri.Tools.Patterns;
using UnityEngine;
using UnityEngine.Events;
using Joeri.Tools.Utilities.SpawnManager;

public class PlayerWeapon : Weapon
{
    [SerializeField] private UnityEvent m_onAttackFumbled;
    [SerializeField] private UnityEvent m_onAttackBlocked;
    [Space]
    [SerializeField] private Element m_element = Element.None;
    [SerializeField] private GameObject m_slashEffect;
    [Space]
    [SerializeField] private Transform m_weaponPivot;

    private ISpawnManager m_effects = null;

    protected override void Awake()
    {
        base.Awake();
        m_effects = ServiceLocator.instance.Get<ISpawnManager>("Effect Collection");
    }

    public void SwitchTo(Element _element)
    {
        m_element = _element;
    }

    protected override void OnHitDamagable(IDamagable _damagable, Vector3 _velocity)
    {
        //  Guard clause.
        if (m_caughtDamagables.Contains(_damagable)) return;

        //  Creating attack data container.
        var instance = new AttackInstance();

        instance.attackVelocity = _velocity;
        instance.damage         = m_damage;
        instance.onAttackEnd    = m_AttackEndCallbak;
        instance.element        = m_element;

        //  Passing data container to interface.
        var callback = _damagable.Damage(instance);

        //  Processing callback..
        switch (callback)
        {
            case HitCallback.Hit:       m_onAttackHit.Invoke();     break;
            case HitCallback.Fumbled:   m_onAttackFumbled.Invoke(); break;
            case HitCallback.Blocked:   m_onAttackBlocked.Invoke(); break;
        }
        SpawnHitEffect(callback);

        //  Adding damagable to the culling list.
        m_caughtDamagables.Add(_damagable);
    }

    private void SpawnHitEffect(HitCallback _callback)
    {
        string effectType = default;

        switch (_callback)
        {
            case HitCallback.Hit:
                if (m_element == Element.Dark)  { effectType = EffectTypes.slashDark;   break; }
                if (m_element == Element.Light) { effectType = EffectTypes.slashLight;  break; }
                break;

            case HitCallback.Fumbled:
                if (m_element == Element.Dark)  { effectType = EffectTypes.fumbleDark;  break; }
                if (m_element == Element.Light) { effectType = EffectTypes.fumbleLight; break; }
                break;
        }

        m_effects.Spawn(effectType, m_weaponPivot.position, m_weaponPivot.rotation);
    }
}
