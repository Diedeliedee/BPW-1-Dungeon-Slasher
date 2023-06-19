using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools.Movement;
using Joeri.Tools.Structure;

public partial class Player : Entity
{
    [Header("Player Properties:")]
    [SerializeField] private float m_teleportDistance = 3f;

    [Header("States:")]
    [SerializeField] private FreeMove.Settings m_freeMove = null;
    [SerializeField] private LeftAttack.Settings m_leftAttack = null;
    [SerializeField] private RightAttack.Settings m_rightAttack = null;

    [Header("Player Sub-components:")]
    [SerializeField] private AbilityHandler m_abilities;

    private Controls m_controls = new Controls();

    public PlayerController movement { get => GetMovement<PlayerController>(); }

    public override void Setup()
    {
        base.Setup();

        m_movement = new PlayerController(gameObject, m_movementSettings);
        m_movement.canRotate = false;

        m_stateMachine = new FSM
            (
                typeof(FreeMove),
                new FreeMove(this, m_freeMove),
                new LeftAttack(this, m_leftAttack),
                new RightAttack(this, m_rightAttack)
            );

        m_abilities.SetAbility(new Teleport(this, m_teleportDistance));
    }

    public override void Tick(float deltaTime)
    {
        m_controls.CheckInput(transform.position);
        m_abilities.Tick(deltaTime);
        base.Tick(deltaTime);
    }

    public override void OnHit(int damage, Entity source)
    {
        base.OnHit(damage, source);
        PlaySound(m_hurtSound, 0.4f, 0.9f);

        GameManager.instance.events.onPlayerHit.Invoke(m_combat.health.health, m_combat.health.maxHealth);
    }

    public void Heal(int amount)
    {
        m_combat.health.AddHealth(amount);
        GameManager.instance.events.onPlayerHeal.Invoke();
    }
}