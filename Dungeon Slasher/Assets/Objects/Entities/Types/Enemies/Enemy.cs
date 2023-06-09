using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools.Structure;
using Joeri.Tools.Movement;

public abstract partial class Enemy : Entity
{
    [Header("General:")]
    [SerializeField] private Type m_type;

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

    protected override void OnEnable()
    {
        base.OnEnable();
        m_movement = new AgentController(gameObject, m_movementSettings);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        m_movement = null;

        onDespawn?.Invoke();
        onDespawn = null;
    }

    public override void OnHit(int damage, Entity source)
    {
        base.OnHit(damage, source);

        SwitchToState<Hitstun>();
        GameManager.instance.events.onEnemyHit.Invoke();
        
        //SwitchToState<Hitpause>().Initiate(damage, Calc.ToDirection(source.flatPosition, flatPosition), out onRetract);
    }

    protected override void OnDeath()
    {
        //  Call to death state?
        //  For now, despawn.

        GameManager.instance.agents.DespawnEnemy(this);
    }

    public enum Type
    {
        Shade
    }
}
