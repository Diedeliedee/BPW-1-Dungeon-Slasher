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
    private Player player { get => GameManager.instance.agents.player; }

    #endregion

    public virtual void OnCreate()
    {
        Setup();
        m_movement = new AgentController(gameObject, m_movementSettings);
    }

    public virtual void OnSpawn()
    {
        m_combat.health.SetHealth(m_combat.health.maxHealth);
    }

    public override void OnHit(int damage, Entity source)
    {
        if (m_dyingSed) return;

        GameManager.instance.events.onEnemyHit.Invoke();
        SwitchToState(typeof(Hitstun));

        base.OnHit(damage, source);
    }

    protected override void OnDeath()
    {
        SwitchToState(typeof(Death));
        base.OnDeath();
    }

    public virtual void OnDespawn()
    {
        m_combat.DeactivateWeapon();
        m_movement.velocity = Vector3.zero;
        m_dyingSed = false;

        onDespawn?.Invoke();
        onDespawn = null;
    }

    public enum Type
    {
        Shade
    }
}
