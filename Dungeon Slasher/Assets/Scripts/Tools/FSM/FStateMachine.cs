using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dodelie.Tools
{
    /// <summary>
    /// Class handling a class-based finite state machine system,
    /// </summary>
    public class FStateMachine
    {
        private readonly Dictionary<System.Type, State> m_states = null;
        private State m_currentState = null;

        public FStateMachine(System.Type startState, params State[] states)
        {
            m_states = new Dictionary<System.Type, State>();

            //  All the state instances in the parameter get initialized, and added to the dictionary.
            foreach (var state in states)
            {
                state.Initialize(this);
                state.OnStart();
                m_states.Add(state.GetType(), state);
            }

            //  Whatever state is set as the start state, gets activated.
            SwitchToState(startState);
        }

        /// <summary>
        /// Switches to a new state based on the passed in type parameter.
        /// </summary>
        public State SwitchToState(System.Type state)
        {
            m_currentState?.OnExit();
            m_currentState = m_states[state];
            m_currentState?.OnEnter();
            return m_currentState;
        }

        /// <summary>
        /// Switches to a new state based on the generic parameter.
        /// </summary>
        public T SwitchToState<T>() where T : State
        {
            return SwitchToState(typeof(T)) as T;
        }

        /// <summary>
        /// Updates the finite state machine's logic.
        /// </summary>
        public virtual void Tick()
        {
            m_currentState.OnTick();
        }

        /// <summary>
        /// Function to call the gizmos of the current active state.
        /// </summary>
        public virtual void DrawGizmos(Vector3 position)
        {
            //  Drawing text in the world describing the current state the agent is in.
            GizmoTools.DrawLabel(position, m_currentState.GetType().ToString(), Color.black);

            //  Drawing the gizmos of the current state, if it isn't null.
            m_currentState?.OnDrawGizmos();
        }
    }
}