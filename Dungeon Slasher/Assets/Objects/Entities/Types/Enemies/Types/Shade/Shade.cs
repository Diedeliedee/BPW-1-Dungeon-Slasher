using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools.Structure;
using Joeri.Tools.Movement;

public partial class Shade : Enemy
{
    [Header("States:")]
    [SerializeField] private ChasePlayer.Settings m_chase;
    [SerializeField] private Attack.Settings m_attack;
    [SerializeField] private Hitstun.Settings m_hitStun;

    public override void OnSpawn()
    {
        base.OnSpawn();
        m_stateMachine = new FSM
            (
                typeof(ChasePlayer),
                new ChasePlayer(this, m_chase),
                new Attack(this, m_attack),
                new Hitstun(this, m_hitStun)
            );
    }
}
