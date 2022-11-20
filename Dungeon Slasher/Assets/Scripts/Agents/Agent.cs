using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonSlasher.Agents
{
    public abstract partial class Agent : MonoBehaviour
    {
        [Header("Agent Properties:")]
        [SerializeField] protected Health m_health      = null;
        [SerializeField] protected Movement m_movement  = null;
        [SerializeField] protected Combat m_combat      = null;

        //  Background:
        protected FiniteStateMachine m_stateMachine     = null;

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

        private void OnDrawGizmosSelected()
        {
            m_combat.DrawGizmos();

            if (!Application.isPlaying) return;

            m_movement.DrawGizmos(transform.position);
            m_stateMachine.DrawGizmos();
            GizmoTools.DrawLabel(transform.position, m_stateMachine.currentState.ToString(), Color.black);
        }
    }
}
