using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class handling a class-based finite state machine system,
/// </summary>
public class FiniteStateMachine
{
    private Dictionary<System.Type, IState> m_states    = null;
    private IState m_currentState                       = null;
    
    public FiniteStateMachine(System.Type startState, IState[] states)
    {
        m_states = new Dictionary<System.Type, IState>();
        foreach (var state in states)
        {
            state.Initialize();
            m_states.Add(state.GetType(), state);
        }
        SwitchToState(startState);
    }

    /// <summary>
    /// Updates the finite state machine's logic.
    /// </summary>
    public void Tick(float deltaTime)
    {
        m_currentState.Tick(deltaTime);
    }

    /// <summary>
    /// Switches to a new state within the dictionary, based on the passed in key.
    /// </summary>
    public void SwitchToState(System.Type stateToSwitchTo)
    {
        m_currentState?.OnExit();
        m_currentState = m_states[stateToSwitchTo];
        m_currentState?.OnEnter();
    }
}
