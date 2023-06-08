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

    public override void Setup()
    {
        base.Setup();
        m_stateMachine = new FSM
            (
                typeof(ChasePlayer),
                new ChasePlayer(this, m_chase),
                new Attack(this, m_attack)
            );
    }
}
