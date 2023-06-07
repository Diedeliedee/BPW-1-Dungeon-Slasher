using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools.Structure;
using Joeri.Tools.Movement;

public partial class NPC : Agent
{
    [Header("NPC Properties:")]
    [SerializeField] private NPCConcept.Type m_type = 0;

    [Header("NPC States:")]
    [SerializeField] private ChasePlayer.Settings m_chasePlayer = null;
    [SerializeField] private Attack.Settings m_attack = null;

    private Player m_player = null;

    protected AgentController movement { get => base.movement as AgentController; }

    private void Awake()
    {
        Setup();
    }

    public void AssignPlayer(Player player)
    {
        m_player = player;
    }

    public override void Setup()
    {
        base.Setup();
        base.movement = base.movement as AgentController;
        m_stateMachine = new FSM
            (
                typeof(ChasePlayer),
                new ChasePlayer(this, m_chasePlayer),
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
        Destroy(gameObject);
        //GameManager.instance.agents.TakeBack(m_type, this);
    }
}
