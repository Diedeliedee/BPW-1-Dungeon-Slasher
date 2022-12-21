using UnityEngine;
using Dodelie.Tools;

namespace DungeonSlasher.Agents
{
    public partial class Agent
    {
        public class Hitstun : AgentState
        {
            private float m_knockback  = 50f;
            private float m_drag       = 50f;

            private System.Type m_returnType = null;

            public Hitstun(System.Type returnType)
            {
                m_returnType = returnType;
            }

            public void Initiate(int damage, Vector2 direction)
            {
                root.health.AddHealth(-damage);
                root.movement.SetVelocity(direction * m_knockback);
            }

            public override void OnTick(float deltaTime)
            {
                root.movement.TickPhysics(deltaTime, m_drag);
                if (root.movement.velocity.magnitude <= 0f) SwitchToState(m_returnType);
            }
        }
    }
}
