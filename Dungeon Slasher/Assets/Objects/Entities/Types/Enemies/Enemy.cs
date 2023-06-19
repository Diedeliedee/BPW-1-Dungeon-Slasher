using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools.Structure;
using Joeri.Tools.Movement;

public abstract partial class Enemy : Entity, IPoolItem
{
    [Header("General:")]
    [SerializeField] private Type m_type;

    private bool m_dyingSed = false;

    #region Properties:

    //  Properties:
    public Type type { get => m_type; }

    //  Events:
    public System.Action onDespawn { get; set; }

    //  Reference:
    protected AgentController movement { get => GetMovement<AgentController>(); }

    //  Utilities:
    private Player player { get => GameManager.instance.entities.player; }

    #endregion

    public virtual void OnCreate()
    {
        Setup();
        m_movement = new AgentController(gameObject, m_movementSettings);
    }

    public virtual void OnSpawn()
    {
        m_aura.Play();
        m_combat.health.SetHealth(m_combat.health.maxHealth);
    }

    public override void OnHit(int damage, Entity source)
    {
        if (m_dyingSed) return;

        GameManager.instance.events.onEnemyHit.Invoke(m_combat.health.health, m_combat.health.maxHealth);
        SwitchToState(typeof(Hitstun));
        PlaySound(m_hurtSound, 0.4f, 1.2f);

        base.OnHit(damage, source);
    }

    protected override void OnDeath()
    {
        SwitchToState(typeof(Death));
        PlaySound(m_deathSound, 0.4f, 1.2f);
        base.OnDeath();
    }

    public virtual void OnDespawn()
    {
        m_dyingSed = false;

        m_combat.DeactivateWeapon();
        m_movement.velocity = Vector3.zero;

        m_audio.Stop();

        onDespawn?.Invoke();
        onDespawn = null;
    }

    public enum Type
    {
        Shade
    }
}
