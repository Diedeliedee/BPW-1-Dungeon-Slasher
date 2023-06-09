using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools.Structure;
using Joeri.Tools.Movement;

public partial class Shade : Enemy
{
    [Header("States:")]
    [SerializeField] private Spawning.Settings m_spawning;
    [SerializeField] private ChasePlayer.Settings m_chase;
    [SerializeField] private Attack.Settings m_attack;
    [SerializeField] private Hitstun.Settings m_hitStun;
    [SerializeField] private Death.Settings m_death;

    public override void OnCreate()
    {
        base.OnCreate();
        m_stateMachine = new FSM
            (
                typeof(Spawning),
                new Spawning(this, m_spawning),
                new ChasePlayer(this, m_chase),
                new Attack(this, m_attack),
                new Hitstun(this, m_hitStun),
                new Death(this, m_death)
            );
    }

    public override void OnSpawn()
    {
        base.OnSpawn();
        SwitchToState<Spawning>();
    }
}
