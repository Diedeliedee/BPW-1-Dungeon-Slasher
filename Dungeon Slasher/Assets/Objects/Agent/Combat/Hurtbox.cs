using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Dodelie.Tools;

namespace DungeonSlasher.Agents
{
    public class Hurtbox : MonoBehaviour
    {
        [SerializeField] private float m_radius = 0.5f;

        private Vector3 m_startPosition { get => transform.position; }

        /// <returns>True if there are agents present within the hurtbox, that are not on the whitelist.</returns>
        public bool DetectedAgents(LayerMask mask, out Agent[] agents, params Collider[] whitelist)
        {
            if (!Calc.Contains(out agents, GetColliders(mask, whitelist))) return false;
            return true;
        }

        /// <returns>All colliders currently present in the hurtbox, that are not on the whitelist.</returns>
        private Collider[] GetColliders(LayerMask mask, params Collider[] whitelist)
        {
            var colliders = Physics.OverlapSphere(m_startPosition, m_radius, mask).ToList();

            bool IsWhiteListed(Collider collider)
            {
                for (int i = 0; i < whitelist.Length; i++)
                {
                    if (collider.GetHashCode() != whitelist[i].GetHashCode()) continue;
                    return true;
                }
                return false;
            }

            //  If the whitelist is null, empty, or the caught colliders list is empty, simply return the list.
            if (whitelist == null || whitelist.Length <= 0 || colliders.Count <= 0) return colliders.ToArray();

            //  Otherwise, check for any whitelisted collider within the caught ones.
            for (int i = 0; i < colliders.Count; i++)
            {
                if (!IsWhiteListed(colliders[i])) continue;
                colliders.Remove(colliders[i]);
                i--;
            }
            return colliders.ToArray();
        }

        /// <summary>
        /// Draws the hurtbox in a spherical representation.
        /// </summary>
        public void DrawGizmos()
        {
            var color = Color.red;
            var opacity = 0.75f;

            GizmoTools.DrawSphere(m_startPosition, m_radius, color, opacity, true);
        }
    }
}
