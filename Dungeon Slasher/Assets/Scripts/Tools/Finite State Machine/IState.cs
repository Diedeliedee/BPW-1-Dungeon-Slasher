using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for a state within a state machine.
/// </summary>
public interface IState
{
    /// <summary>
    /// Called whenever the finite state machine the state is in, is created.
    /// </summary>
    public void Initialize()            { }

    /// <summary>
    /// Update function for the state.
    /// </summary>
    public void Tick(float deltaTime)   { }

    /// <summary>
    /// Called whenevet the state is entered.
    /// </summary>
    public void OnEnter()               { }

    /// <summary>
    /// Called whenever the state is exited.
    /// </summary>
    public void OnExit()                { }
}
