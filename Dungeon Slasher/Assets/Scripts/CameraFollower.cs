using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform m_target;
    [SerializeField] private float m_followTime = 0.1f;

    private Vector3 m_offset = Vector3.zero;
    private Vector3 m_velocity = Vector3.zero;

    private void Start()
    {
        m_offset = transform.position - m_target.position;
    }

    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, m_target.position + m_offset, ref m_velocity, m_followTime);
    }
}
