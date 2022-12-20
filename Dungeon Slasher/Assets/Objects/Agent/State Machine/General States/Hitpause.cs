using UnityEngine;
using Dodelie.Tools;

namespace DungeonSlasher.Agents
{
    public partial class Agent
    {
        public class Hitpause : AgentState
        {
            private const float m_forceMultiplier = 0.25f;

            private ShakeInstancer m_shake      = null;
            private System.Type m_returnType    = null;
            private Vector2 m_velocity          = Vector2.zero;
            private float m_time                = 0f;

            private float m_timer               = 0f;

            public void Initiate(System.Type returnType, Vector2 velocity, float time)
            {
                m_shake         = new ShakeInstancer(root.transform.position, ForceToAmpltidue(velocity.magnitude), 60f, 0.5f);
                m_returnType    = returnType;
                m_velocity      = velocity;
                m_time          = time;
            }

            public override void OnTick(float deltaTime)
            {
                root.transform.position = m_shake.GetPosition(deltaTime);

                m_timer += deltaTime;
                if (m_timer < m_time) return;

                SwitchToState<Hitstun>();
            }

            public override void OnExit()
            {
                root.transform.position = m_shake.startPosition;
                m_shake     = null;
                m_velocity  = Vector2.zero;
                m_time      = 0f;
                m_timer     = 0f;
            }

            private float ForceToAmpltidue(float force)
            {
                return Mathf.Sqrt(force) * m_forceMultiplier;
            }
        }
    }
}
