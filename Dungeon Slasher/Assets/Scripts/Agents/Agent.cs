using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonSlasher.Agents
{
    public abstract partial class Agent : MonoBehaviour
    {
        [SerializeField] private AgentSettings m_settings = null;

        //  Run-time Variables:
        protected FiniteStateMachine m_stateMachine = null;
        protected Blackboard m_blackBoard           = null;
        protected Movement m_movement               = null;
        protected Health m_health                   = null;

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
            m_health        = new Health(10, 10);
            m_movement      = new Movement(GetComponent<CharacterController>(), m_settings);
            m_blackBoard    = new Blackboard(gameObject, m_settings, m_movement);
        }

        /// <summary>
        /// Updates the logic of the agent.
        /// </summary>
        public virtual  void Tick(float deltaTime)
        {
            m_blackBoard.UpdateBlackboard(deltaTime);
            m_stateMachine.Tick();
        }

        private void OnDrawGizmosSelected()
        {
            if (!Application.isPlaying) return;

            m_movement.DrawGizmos(transform.position);
            m_stateMachine.DrawGizmos();
            GizmoTools.DrawLabel(transform.position, m_stateMachine.currentState.ToString(), Color.black);
        }
    }
}
