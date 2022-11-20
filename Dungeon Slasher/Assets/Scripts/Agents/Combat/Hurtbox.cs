using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

            if (whitelist == null || whitelist.Length <= 0 || colliders.Count <= 0) return colliders.ToArray();
            for (int c = 0; c < colliders.Count; c++)
            {
                for (int w = 0; w < whitelist.Length; w++)
                {
                    //  Anomalous code with weird index error because stuff gets deleted probably.
                    if (colliders[c].GetHashCode() != whitelist[w].GetHashCode()) continue;
                    colliders.Remove(colliders[c]);
                    c--;
                    if (c >= colliders.Count)
                    {
                        Debug.LogError("Bitch");
                        return null;
                    }
                }
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
