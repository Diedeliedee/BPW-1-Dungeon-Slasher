using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonSlasher.Agents
{
    public class Hurtbox : MonoBehaviour
    {
        [SerializeField] private LayerMask m_hitMask = new LayerMask();
        [SerializeField] private float m_radius = 0.5f;
        [SerializeField] private float m_addedLength = 0f;
        [SerializeField] private float m_invokeTime = 0f;

        private Vector3 m_startPosition { get => transform.position; }
        private Vector3 m_endPosition { get => transform.position + transform.forward * m_addedLength; }

        private Agent[] GetAgents()
        {
            if (!Calc.Contains(out Agent[] agents, GetColliders())) return null;
            return agents;
        }

        private Collider[] GetColliders()
        {
            if (m_addedLength > 0f)
            {
                return Physics.OverlapCapsule(m_startPosition, m_endPosition, m_radius, m_hitMask);
            }
            return Physics.OverlapSphere(m_startPosition, m_radius, m_hitMask);
        }



        private void OnDrawGizmosSelected()
        {
            var color = Color.red;
            var opacity = 0.75f;

            if (m_addedLength > 0f)
            {
                GizmoTools.DrawCapsule(transform.position, transform.position + transform.forward * m_addedLength, m_radius, color, opacity);
                return;
            }
            GizmoTools.DrawSphere(transform.position, m_radius, color, opacity);
        }
    }
}
