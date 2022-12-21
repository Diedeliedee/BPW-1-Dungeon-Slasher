using UnityEngine;
using Dodelie.Tools;

namespace DungeonSlasher.Agents
{
    public partial class Agent
    {
        public class Hitpause : AgentState
        {
            private int m_damage = 0;
            private Vector2 m_forceDirection = Vector2.zero;

            private ShakeInstancer m_shake  = null;

            public void Initiate(int damage, Vector2 forceDirection, out System.Action onRetract)
            {
                m_damage = damage;
                m_forceDirection = forceDirection;
                onRetract = Resume;
            }

            public override void OnEnter()
            {
                m_shake = new ShakeInstancer(root.transform.position, 0.1f, 60f, 0.1f);
            }

            public override void OnTick(float deltaTime)
            {
                root.transform.position = m_shake.GetPosition(Time.unscaledDeltaTime);
            }

            public override void OnExit()
            {
                root.transform.position = m_shake.startPosition;

                m_damage = 0;
                m_forceDirection = Vector2.zero;

                m_shake = null;
            }

            private void Resume()
            {
                Debug.LogWarning("Yay, this shit is succesfull.");
                SwitchToState<Hitstun>().Initiate(m_damage, m_forceDirection);
            }
        }
    }
}
