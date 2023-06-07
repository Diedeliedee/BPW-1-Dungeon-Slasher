using UnityEngine;
using Joeri.Tools.Movement;
using Joeri.Tools.Structure;
using Joeri.Tools.Utilities;

public partial class Agent
{
    public Health health { get => m_combat.health; }
    public MovementBase movement { get => m_movement; protected set => m_movement = value; }
    public Combat combat { get => m_combat; }
    public Animator animator { get => m_animator; }

    public Collider collider { get => m_movement.controller; }

    public Vector2 flatPosition { get => Vectors.VectorToFlat(transform.position); set => transform.position = Vectors.FlatToVector(value, transform.position.y); }

    /// <summary>
    /// Function to 'hit' the agent from outside.
    /// </summary>
    public virtual void OnHit(int damage, Agent source)
    {
        m_combat.health.AddHealth(-damage);
    }

    /// <summary>
    /// Public accessibility function to switch to another state the agent possesses from outside.
    /// </summary>
    public T SwitchToState<T>() where T : State
    {
        return m_stateMachine.SwitchToState<T>();
    }
}
