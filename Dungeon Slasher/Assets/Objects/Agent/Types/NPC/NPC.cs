using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools.Structure;
using Joeri.Tools.Movement;

public partial class NPC : Agent
{
    [Header("States:")]
    [SerializeField] private ChasePlayer.Settings m_chase;
    [SerializeField] private Attack.Settings m_attack;

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
        m_stateMachine = new FSM
            (
                typeof(ChasePlayer),
                new ChasePlayer(this, m_chase),
                new Attack(this, m_attack)
            );
    }

    public override void OnHit(int damage, Agent source)
    {
        base.OnHit(damage, source);
        /*
        GameManager.instance.events.onEnemyHit.Invoke();
        SwitchToState<Hitpause>().Initiate(damage, Calc.ToDirection(source.flatPosition, flatPosition), out onRetract);
        */
    }

    protected override void OnDeath()
    {
        GameManager.instance.agents.DespawnEnemy(this);
    }
}
