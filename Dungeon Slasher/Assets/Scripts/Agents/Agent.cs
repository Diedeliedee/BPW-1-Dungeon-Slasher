using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonSlasher.Agents
{
    public abstract partial class Agent : MonoBehaviour
    {
        //  Run-time Variables:
        protected FiniteStateMachine m_stateMachine = null;
        protected Blackboard m_blackBoard           = null;

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
            m_blackBoard = new Blackboard(gameObject, GetComponent<CharacterController>());
        }

        /// <summary>
        /// Updates the logic of the agent.
        /// </summary>
        public virtual  void Tick(float deltaTime)
        {
            m_blackBoard.UpdateBlackboard(deltaTime);
            m_stateMachine.Tick();
        }
    }
}
