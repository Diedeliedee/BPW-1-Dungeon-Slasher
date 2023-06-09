using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools.Movement;
using Joeri.Tools.Structure;

public partial class Player : Entity
{
    [Header("States:")]
    [SerializeField] private FreeMove.Settings m_freeMove = null;
    [SerializeField] private LeftAttack.Settings m_leftAttack = null;
    [SerializeField] private RightAttack.Settings m_rightAttack = null;

    private Controls m_controls = new Controls();

    public PlayerController movement { get => GetMovement<PlayerController>(); }

    protected override void OnEnable()
    {
        base.OnEnable();
        m_movement = new PlayerController(gameObject, m_movementSettings);
        m_stateMachine = new FSM
            (
                typeof(FreeMove),
                new FreeMove(this, m_freeMove),
                new LeftAttack(this, m_leftAttack),
                new RightAttack(this, m_rightAttack)
            );
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        m_movement = null;
        m_stateMachine = null;
    }

    public override void Tick(float deltaTime)
    {
        m_controls.CheckInput(transform.position);
        base.Tick(deltaTime);
    }

    public override void OnHit(int damage, Entity source)
    {
        base.OnHit(damage, source);
        /*
        GameManager.instance.events.onPlayerHit.Invoke();
        SwitchToState<Hitstun>().Initiate(damage, Calc.ToDirection(source.flatPosition, flatPosition));
        */
    }
}