using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonSlasher.Agents
{
    /// <summary>
    /// Theoretical weapon. Not the actual weapon itself, rather the digital structure holding it's information.
    /// </summary>
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private Hurtbox[] m_hurtboxes = new Hurtbox[1];

        private List<Agent> m_caughtAgents = new List<Agent>();
        private bool m_active = false;

        public void Tick()
        {
            if (!!m_active) return;
            foreach (var hurtbox in m_hurtboxes)
            {
                if (!hurtbox.DetectedAgents(out Agent[] agents)) continue;
                foreach (var agent in agents)
                {
                    if (IsAlreadyCaught(agent)) continue;
                    Hit(agent);
                }
            }
        }

        /// <summary>
        /// "Hits" an agent. Giving responsibility of reaction to the agent being hit.
        /// </summary>
        private void Hit(Agent agent)
        {
            m_caughtAgents.Add(agent);
        }

        /// <summary>
        /// Enables, or disables the activation state of the weapon.
        /// </summary>
        public void SetEnabled(bool enabled)
        {
            m_active = enabled;
            if (!enabled) m_caughtAgents.Clear();
        }

        /// <returns>True if the passed in agent was already caught within one of the weapon's hurtboxes during this instance of activation.</returns>
        private bool IsAlreadyCaught(Agent agent)
        {
            foreach (var caughtAgent in m_caughtAgents)
            {
                if (agent.GetHashCode() != caughtAgent.GetHashCode()) continue;
                return true;
            }
            return false;
        }

        private void OnDrawGizmosSelected()
        {
            if (Application.isPlaying && !m_active) return;
            foreach (var hurtbox in m_hurtboxes) hurtbox.DrawGizmos();
        }
    }
}
