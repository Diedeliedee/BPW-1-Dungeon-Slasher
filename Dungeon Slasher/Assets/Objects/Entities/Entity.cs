using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Joeri.Tools.Structure;
using Joeri.Tools.Movement;

public abstract partial class Entity : MonoBehaviour
{
    [Header("Agent Components:")]
    [SerializeField] protected Combat m_combat;

    [Header("Agent References:")]
    [SerializeField] protected MovementBase.Settings m_movementSettings;

    //  Run-time:
    protected MovementBase m_movement = null;
    protected FSM m_stateMachine = null;

    //  Reference:
    protected Animator m_animator;

    protected virtual void Start()
    {
        m_animator = GetComponent<Animator>();

        m_combat.Setup(this);
        m_combat.health.onDeath += OnDeath;
    }

    protected virtual void OnEnable()
    {
        m_combat.health.SetHealth(m_combat.health.maxHealth);
    }

    protected virtual void OnDisable()
    {
        m_combat.health.SetHealth(0);
        m_combat.DeactivateWeapon();
        m_stateMachine = null;
    }

    public virtual void Tick(float deltaTime)
    {
        m_stateMachine.Tick(deltaTime);
    }

    public void CrossFadeAnimation(AnimationClip clip, float seconds = 0f)
    {
        if (clip == null) return;
        m_animator.CrossFadeInFixedTime(clip.name, seconds);
    }

    protected virtual void OnDeath()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) return;
        m_movement.DrawGizmos();
        m_stateMachine.DrawGizmos(transform.position);
    }
}
