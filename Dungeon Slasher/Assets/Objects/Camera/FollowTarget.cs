using Joeri.Tools.Utilities;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform m_target;
    [Space]
    [SerializeField] private float m_time = 0f;
    [Space]
    [SerializeField] private Vector2 m_offset = default;
    [SerializeField] private bool m_applyOffset = false;

    private Vector3 m_velocity = default;

    public float time       { get => m_time; set => m_time = value; }
    public Vector3 velocity { get => m_velocity; set => m_velocity = value; }
    public Vector2 offset   { get => m_offset; set => m_offset = value; }
    public bool applyOffset { get => m_applyOffset; set => m_applyOffset = value; }

    private void Update()
    {
        var target = m_target.position + (m_applyOffset ? offset.Cubular() : Vector3.zero);

        if (m_time <= 0)    transform.position = target;
        else                transform.position = Vector3.SmoothDamp(transform.position, target, ref m_velocity, m_time);
    }
}
