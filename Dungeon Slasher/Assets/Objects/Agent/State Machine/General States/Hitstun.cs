using UnityEngine;
using Dodelie.Tools;

namespace DungeonSlasher.Agents
{
    public partial class Agent
    {
        public class Hitstun : AgentState
        {
            private System.Type m_returnType = null;

            public void Initiate(System.Type returnType, Vector2 force)
            {
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
