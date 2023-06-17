using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Joeri.Tools.Structure;
using Joeri.Tools.Movement;

public abstract partial class Entity : MonoBehaviour
{
    [Header("Agent Properties:")]
    [SerializeField] protected AudioClip m_hurtSound;
    [SerializeField] protected AudioClip m_deathSound;

    [Header("Agent Components:")]
    [SerializeField] protected Combat m_combat;

    [Header("Agent References:")]
    [SerializeField] protected MovementBase.Settings m_movementSettings;
    [SerializeField] protected ParticleSystem m_aura;

    //  Run-time:
    protected MovementBase m_movement = null;
    protected FSM m_stateMachine = null;

    //  Reference:
    protected Animator m_animator = null;
    protected AudioSource m_audio = null;

    #region Properties

    //  Events:
    public event System.Action onDeath 
    { 
        add => m_combat.health.onDeath += value;
        remove => m_combat.health.onDeath -= value;
    }
    public event System.Action<int, int> onHealthChange
    {
        add => m_combat.health.onHealthChange += value;
        remove => m_combat.health.onHealthChange -= value;
    }

    #endregion

    public virtual void Setup()
    {
        m_animator = GetComponent<Animator>();
        m_audio = GetComponent<AudioSource>();

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

    protected virtual void OnDeath()
    {
        m_aura.Stop();
    }

    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) return;
        m_movement.DrawGizmos();
        m_stateMachine.DrawGizmos(transform.position);
    }
}
