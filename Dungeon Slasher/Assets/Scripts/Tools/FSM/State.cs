using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dodelie.Tools
{
    /// <summary>
    /// Abstract base for a state within a state machine.
    /// </summary>
    public abstract class State
    {
        /// <summary>
        /// The state machine this state is a part of.
        /// </summary>
        protected FStateMachine parent { get; private set; }

        /// <summary>
        /// Called whenever the finite state machine the state is in, is created.
        /// </summary>
        public virtual void Initialize(FStateMachine parent)
        {
            this.parent = parent;
        }

        #region Local Interface

        /// <summary>
        /// Switches to another state using a generic variable.
        /// </summary>
        protected T SwitchToState<T>() where T : State
        {
            return parent.SwitchToState<T>();
        }

        /// <summary>
        /// Switches to another state using a type variable.
        /// </summary>
        protected State SwitchToState(System.Type state)
        {
            return parent.SwitchToState(state);
        }

        #endregion

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