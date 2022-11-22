using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dodelie.Tools;

namespace DungeonSlasher.Agents
{
    public abstract partial class Agent : MonoBehaviour
    {
        [Header("Agent Properties:")]
        [SerializeField] private Health m_health        = null;
        [SerializeField] private Movement m_movement    = null;
        [SerializeField] private Combat m_combat        = null;

        //  Background:
        private AgentFSM m_stateMachine                 = null;

        //  Run-time Variables:
        protected Blackboard m_blackBoard               = null;

        public Collider collider { get => m_movement.collider; }

        private void Awake()
        {
            Initialize();
        }

        private void Update()
        {
            Tick(Time.deltaTime);
        }

        /// <summary>
        /// 'Awake' function for the agent.
        /// </summary>
        public virtual void Initialize()
        {
            m_blackBoard = new Blackboard(gameObject, m_movement, m_combat);
        }

        /// <summary>
        /// Updates the logic of the agent.
        /// </summary>
        public virtual void Tick(float deltaTime)
        {
            m_blackBoard.UpdateBlackboard(deltaTime);
            m_stateMachine.Tick();
            m_combat.Tick(m_movement.collider);
        }

        #region Public Accesibility

        public void ChangeHealth(int amount)
        {
            m_health.AddHealth(amount);
        }

        #endregion

        protected void SetStates(System.Type startState, params AgentState[] states)
        {
            m_stateMachine = new AgentFSM(m_blackBoard, startState, states);
        }

        private void OnDrawGizmosSelected()
        {
            m_combat.DrawGizmos();

            if (!Application.isPlaying) return;

            m_movement.DrawGizmos(transform.position);
            m_stateMachine.DrawGizmos(transform.position);
        }
    }
}
