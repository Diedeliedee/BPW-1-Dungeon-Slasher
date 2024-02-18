using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform m_target;
    [SerializeField] private float m_time = 0f;

    private Vector3 m_offset = Vector3.zero;
    private Vector3 m_velocity = Vector3.zero;

    private void Start()
    {
        m_offset = transform.position - m_target.position;
    }

    private void Update()
    {
        if (m_time <= 0)
        {
            transform.position = m_target.position + m_offset;
            return;
        }
        transform.position = Vector3.SmoothDamp(transform.position, m_target.position + m_offset, ref m_velocity, m_time);
    }
}
