using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonSlasher.Agents
{
    /// <summary>
    /// Theoretical weapon. Not the actual weapon itself, rather the digital structure holding it's information.
    /// </summary>
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private int m_damage           = 5;
        [Space]
        [SerializeField] private Agent m_root           = null;
        [SerializeField] private Hurtbox[] m_hurtboxes  = new Hurtbox[1];
        [Space]
        [SerializeField] private TrailRenderer m_trail  = null;

        private List<Collider> m_caughtAgents           = new List<Collider>();
        private bool m_active                           = false;

        private event System.Action m_onRetract         = null;

        public bool isActive { get => m_active; }

        public void Tick(LayerMask mask, Collider ownCollider)
        {
            if (!m_active) return;

            //  The list of colliders representing agents already hit within this weapon's active instance gets made into a local variable.
            var whiteList = m_caughtAgents;

            //  The collider of the agent initiating the attack then gets added to the list.
            whiteList.Add(ownCollider);

            //  The weapon will scan each hurtbox for colliders, and attempt to extract agent classes from them.
            //  !IMPORTANT! Make sure whatever collider type the agent is using, is on the same game object as the agent component.
            //  As of now, agents already hit will not be hit in the next frame, because their singular collider has been added to the whitelist.
            for (int i = 0; i < m_hurtboxes.Length; i++)
            {
                if (!m_hurtboxes[i].DetectedAgents(mask, out Agent[] agents, whiteList.ToArray())) continue;
                foreach (var agent in agents) Hit(agent);
            }
        }

        /// <summary>
        /// "Hits" an agent. Giving responsibility of reaction to the agent being hit.
        /// </summary>
        private void Hit(Agent agent)
        {
            if (m_root == null) 
            { 
                Debug.LogError("Weapon's root agent is not configured.", gameObject); 
                return; 
            }

            agent.Hit(m_damage, m_root, out System.Action retractEvent);
            m_onRetract += retractEvent;
            m_caughtAgents.Add(agent.collider);
        }

        /// <summary>
        /// Enables, or disables the activation state of the weapon.
        /// </summary>
        public void SetEnabled(bool enabled)
        {
            m_active = enabled;
            if (m_trail != null) m_trail.gameObject.SetActive(enabled);
            if (!enabled)
            {
                m_onRetract?.Invoke();
                m_caughtAgents.Clear();
                m_onRetract = null;
            }
        }

        public void DrawGizmos()
        {
            if (Application.isPlaying && !m_active) return;
            foreach (var hurtbox in m_hurtboxes) hurtbox?.DrawGizmos();
        }
    }
}
