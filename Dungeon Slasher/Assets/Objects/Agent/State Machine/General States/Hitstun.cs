using UnityEngine;
using Dodelie.Tools;

namespace DungeonSlasher.Agents
{
    public partial class Agent
    {
        public class Hitstun : AgentState
        {
            private System.Type m_returnType = null;

            public Hitstun(System.Type returnType)
            {
                m_returnType = returnType;
            }

            public void Initiate(int damage, Vector2 force)
            {
                root.health.AddHealth(-damage);
                root.movement.SetVelocity(force);
            }

            public override void OnTick(float deltaTime)
            {
                root.movement.TickPhysics(deltaTime, 5f);
                if (root.movement.velocity.magnitude <= 0f) SwitchToState(m_returnType);
            }
        }
    }
}
