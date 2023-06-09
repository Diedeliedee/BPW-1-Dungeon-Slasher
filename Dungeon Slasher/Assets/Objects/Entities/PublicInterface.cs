using UnityEngine;
using Joeri.Tools.Movement;
using Joeri.Tools.Structure;
using Joeri.Tools.Utilities;

public partial class Entity
{
    public Vector2 flatPosition { get => Vectors.VectorToFlat(transform.position); set => transform.position = Vectors.FlatToVector(value, transform.position.y); }

    /// <summary>
    /// Function to 'hit' the agent from outside.
    /// </summary>
    public virtual void OnHit(int damage, Entity source)
    {
        m_combat.health.AddHealth(-damage);
    }

    public T SwitchToState<T>() where T : State
    {
        return m_stateMachine.SwitchToState<T>();
    }

    public State SwitchToState(System.Type state)
    {
        return m_stateMachine.SwitchToState(state);
    }

    public T GetMovement<T>() where T : MovementBase
    {
        return m_movement as T;
    }
}
