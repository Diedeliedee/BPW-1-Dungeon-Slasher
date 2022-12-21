using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Dodelie.Tools;

namespace DungeonSlasher.Agents
{
    public abstract partial class Agent : MonoBehaviour
    {
        public event System.Action onDespawn = null;

        [Header("Agent Properties:")]
        [SerializeField] private Health m_health        = null;
        [SerializeField] private Movement m_movement    = null;
        [SerializeField] private Combat m_combat        = null;

        [Header("Agent References:")]
        [SerializeField] private Animator m_animator    = null;

        //  Background:
        private FSM<Agent> m_stateMachine               = null;

        /// <summary>
        /// Updates the logic of the agent.
        /// </summary>
        public virtual void Tick(float deltaTime)
        {
            m_stateMachine.Tick(deltaTime);
            m_combat.Tick(m_movement.collider);
        }

        /// <summary>
        /// 'Awake' function for the agent.
        /// </summary>
        public virtual void Initialize()
        {
            m_health.onDeath += OnDeath;
        }

        /// <summary>
        /// Create, and configure a new state machine with the given states.
        /// </summary>
        protected void SetStates(System.Type startState, params AgentState[] states)
        {
            m_stateMachine = new FSM<Agent>(this, startState, states);
        }

        /// <summary>
        /// Called the moment the agent's health is depleted.
        /// </summary>
        public abstract void OnDeath();

        private void OnDrawGizmosSelected()
        {
            m_combat.DrawGizmos();

            if (!Application.isPlaying) return;

            m_movement.DrawGizmos(transform.position);
            m_stateMachine.DrawGizmos(transform.position);
        }
    }
}
