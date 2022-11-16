using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonSlasher.Agents
{
    public abstract partial class Agent
    {
        /// <summary>
        /// Interface for a state within a state machine.
        /// </summary>
        public abstract class State
        {
            private FiniteStateMachine m_parentStateMachine = null;

            /// <summary>
            /// The state machine this state is a part of.
            /// </summary>
            protected FiniteStateMachine parent { get => m_parentStateMachine; }

            /// <summary>
            /// The state machine this state is a part of.
            /// </summary>
            protected Blackboard blackBoard { get => m_parentStateMachine.blackboard; }

            /// <summary>
            /// Called whenever the finite state machine the state is in, is created.
            /// </summary>
            public void Initialize(FiniteStateMachine parent)
            {
                m_parentStateMachine = parent;
            }

            /// <summary>
            /// Called at the very beginning of the state.
            /// </summary>
            public virtual void OnStart() { }

            /// <summary>
            /// Update function for the state.
            /// </summary>
            public virtual void OnTick() { }

            /// <summary>
            /// Called whenevet the state is entered.
            /// </summary>
            public virtual void OnEnter() { }

            /// <summary>
            /// Called whenever the state is exited.
            /// </summary>
            public virtual void OnExit() { }

            /// <summary>
            /// Functions for drawing gizmos of the state.
            /// </summary>
            public virtual void OnDrawGizmos() { }
        }
    }
}