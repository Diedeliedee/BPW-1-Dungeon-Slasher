using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaActivator : MonoBehaviour
{
    private event System.Action m_onPlayerEnter = null;

    private Collider m_collider = null;

    public void Setup(System.Action onEnter)
    {
        m_onPlayerEnter += onEnter;
        m_collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Player player)) return;
        m_onPlayerEnter.Invoke();
        m_collider.enabled = false;
    }
}
