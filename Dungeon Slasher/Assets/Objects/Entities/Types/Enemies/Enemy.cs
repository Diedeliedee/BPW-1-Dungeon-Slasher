using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools.Structure;
using Joeri.Tools.Movement;

public abstract partial class Enemy : Entity
{
    private Player m_player = null;

    protected AgentController movement { get => GetMovement<AgentController>(); }

    public void AssignPlayer(Player player)
    {
        m_player = player;
    }

    public override void Setup()
    {
        base.Setup();
        m_movement = new AgentController(gameObject, m_movementSettings);
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
        GameManager.instance.agents.DespawnEnemy(this);
    }
}
