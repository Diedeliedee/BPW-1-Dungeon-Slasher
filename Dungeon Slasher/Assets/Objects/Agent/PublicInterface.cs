using Dodelie.Tools;
using UnityEngine;

namespace DungeonSlasher.Agents
{
    public partial class Agent
    {
        public Health health { get => m_health; }
        public Movement movement { get => m_movement; }
        public Combat combat { get => m_combat; }
        public Animator animator { get => m_animator; }

        public Collider collider { get => m_movement.collider; }

        public Vector2 flatPosition { get => new Vector2(transform.position.x, transform.position.z); set => transform.position = new Vector3(value.x, transform.position.y, value.y); }

        /// <summary>
        /// Function to 'hit' the agent from outside.
        /// </summary>
        public abstract void Hit(int damage, Agent source, out System.Action onRetract);

        /// <summary>
        /// Public accessibility function to switch to another state the agent possesses from outside.
        /// </summary>
        public State SwitchToState<State>() where State : State<Agent>
        {
            return m_stateMachine.SwitchToState<State>();
        }
    }
}
