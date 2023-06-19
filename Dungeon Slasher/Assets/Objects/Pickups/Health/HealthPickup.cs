using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private float m_pickupTime = 0.5f;
    [SerializeField] private int m_healAmount = 1;

    private Vector3 m_startPosition = Vector3.zero;
    private Timer m_timer = null;
    private Player m_player = null;

    private void Start()
    {
        m_startPosition = transform.position;
    }

    private void Update()
    {
        if (m_player == null) return;

        transform.position = Vector3.Lerp(m_startPosition, m_player.transform.position, m_timer.percent);
        if (!m_timer.HasReached(Time.deltaTime)) return;

        m_player.Heal(m_healAmount);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_player != null) return;
        if (!other.TryGetComponent(out Player player)) return;

        m_timer = new Timer(m_pickupTime);
        m_player = player;
    }
}
