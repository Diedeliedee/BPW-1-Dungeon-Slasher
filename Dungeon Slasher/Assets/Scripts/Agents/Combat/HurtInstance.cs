using UnityEngine;

namespace DungeonSlasher.Agents
{
    public partial class Combat
    {
        private class HurtInstance
        {
            private Agent m_caughtAgent = null;
            private float m_time = 0f;
            private float m_timer = 0f;


            public HurtInstance(Agent agent, float time)
            {
                m_caughtAgent = agent;
                m_time = time;
            }

            public void Tick(float deltaTime)
            {
                m_timer += deltaTime;
                if (m_timer < m_time) return;

                Deal();
            }

            public void Deal()
            {
                //  Kaboom damage boom flash!
            }
        }
    }
}
