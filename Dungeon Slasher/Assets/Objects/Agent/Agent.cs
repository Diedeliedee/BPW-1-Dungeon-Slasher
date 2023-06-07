using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Joeri.Tools.Structure;
using Joeri.Tools.Movement;

public abstract partial class Agent : MonoBehaviour
{
    [Header("Agent Properties:")]
    [SerializeField] private MovementBase m_movement = null;
    [SerializeField] private Combat m_combat = null;

    [Header("Agent References:")]
    [SerializeField] private Animator m_animator = null;

    //  Run-time:
    protected FSM m_stateMachine = null;

    public virtual void Setup()
    {
        m_combat.Setup(this);
        m_combat.health.onDeath += OnDeath;
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

    protected abstract void OnDeath();

    private void OnDrawGizmosSelected()
    {
        m_movement.DrawGizmos();
        m_stateMachine.DrawGizmos(transform.position);
    }
}
