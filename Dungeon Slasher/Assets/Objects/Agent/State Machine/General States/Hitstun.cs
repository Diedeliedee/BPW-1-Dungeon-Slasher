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
                blackBoard.movement.SetVelocity(force);
            }

            public override void OnTick()
            {
                blackBoard.movement.TickPhysics(blackBoard.deltaTime, 5f);
                if (blackBoard.movement.velocity.magnitude <= 0f) SwitchToState(m_returnType);
            }
        }
    }
}
