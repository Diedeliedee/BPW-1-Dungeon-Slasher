using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonSlasher.Agents
{
    public abstract class Agent : MonoBehaviour
    {
        //  Run-time Variables:
        protected FSM m_stateMachine = null;

        //  Reference:
        protected CharacterController m_controller = null;

        private void Awake()
        {
            Initialize();
        }

        /// <summary>
        /// 'Awake' function for the agent.
        /// </summary>
        public virtual void Initialize()
        {
            m_controller = GetComponent<CharacterController>();
        }

        /// <summary>
        /// Updates the logic of the agent.
        /// </summary>
        public void Update()
        {
            var context = new State.Context(Time.deltaTime, gameObject, m_controller);

            m_stateMachine.Tick(context);
        }

        public State.Context context
        {
            get
            {

            }
        }
           
    }
}
