using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class handling a class-based finite state machine system,
/// </summary>
public class FiniteStateMachine
{
    private Dictionary<System.Type, State> m_states     = null;
    private State m_currentState                        = null;
    
    public FiniteStateMachine(System.Type startState, params State[] states)
    {
        m_states = new Dictionary<System.Type, State>();
        foreach (var state in states)
        {
            state.Initialize(this);
            state.OnStart();
            m_states.Add(state.GetType(), state);
        }
        SwitchToState(startState);
    }

    /// <summary>
    /// Switches to a new state within the dictionary, based on the passed in key.
    /// </summary>
    public virtual void SwitchToState(System.Type stateToSwitchTo)
    {
        m_currentState?.OnExit();
        m_currentState = m_states[stateToSwitchTo];
        m_currentState?.OnEnter();
    }

    /// <summary>
    /// Updates the finite state machine's logic.
    /// </summary>
    public virtual void Tick(float deltaTime)
    {
        m_currentState.OnTick(deltaTime);
    }


    /// <summary>
    /// Function to call the gizmos of the current active state.
    /// </summary>
    public virtual void DrawGizmos()
    {
        m_currentState?.OnDrawGizmos();
    }
}
