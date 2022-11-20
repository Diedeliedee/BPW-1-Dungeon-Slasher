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
            /// <summary>
            /// The state machine this state is a part of.
            /// </summary>
            protected FiniteStateMachine parent { get; private set; }

            /// <summary>
            /// The behavior handler helping with movement behaviors.
            /// </summary>
            protected BehaviorHandler behavior { get; private set; }

            /// <summary>
            /// The state machine this state is a part of.
            /// </summary>
            protected Blackboard blackBoard { get => parent.blackboard; }

            /// <summary>
            /// Called whenever the finite state machine the state is in, is created.
            /// </summary>
            public void Initialize(FiniteStateMachine parent)
            {
                this.parent     = parent;
                this.behavior   = new BehaviorHandler();
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