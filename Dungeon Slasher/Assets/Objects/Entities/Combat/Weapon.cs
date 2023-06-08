using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools;
using Joeri.Tools.Debugging;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Properties:")]
    [SerializeField] private int m_damage = 5;

    [Header("Hurtboxes:")]
    [SerializeField] private Transform[] m_hurtboxes = new Transform[1];
    [SerializeField] private float m_radius = 1f;

    [Header("Trail:")]
    [SerializeField] private TrailRenderer m_trail = null;

    //  Run-time:
    private Overlapper<Entity> m_overlapper = null;

    //  Reference:
    private Entity m_root = null;

    public void Setup(Entity agent, LayerMask mask)
    {
        m_root = agent;
        m_overlapper = new Overlapper<Entity>(m_radius, mask);
    }

    public void Tick()
    {
        if (!m_overlapper.active) return;

        for (int i = 0; i < m_hurtboxes.Length; i++)
        {
            m_overlapper.Overlap(m_hurtboxes[i].position);
        }
    }

    private void Hit(Entity agent)
    {
        if (m_root == null)
        {
            Debug.LogError("Weapon's root agent is not configured.", gameObject);
            return;
        }
        agent.OnHit(m_damage, m_root);
    }

    public void Activate()
    {
        m_overlapper.Activate(Hit, null);
        if (m_trail != null) m_trail.enabled = true;
    }

    public void Deactivate()
    {
        m_overlapper.Deactivate();
        if (m_trail != null) m_trail.enabled = false;
    }

    public void OnDrawGizmos()
    {
        if (Application.isPlaying && !m_overlapper.active) return;

        for (int i = 0; i < m_hurtboxes.Length; i++)
        {
            if (m_hurtboxes[i] == null) continue;
            GizmoTools.DrawSphere(m_hurtboxes[i].position, m_radius, Color.red, 0.75f, true);
        }
    }
}
