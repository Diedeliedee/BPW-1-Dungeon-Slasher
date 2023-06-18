using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool m_hovering = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Player player)) return;

        m_hovering = true;
        GameManager.instance.ui.DisplayInteractionButton(true);
    }

    private void Update()
    {
        if (!m_hovering) return;
        if (!Input.GetKeyDown(KeyCode.E)) return;
        GameManager.instance.OnWin();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent(out Player player)) return;

        m_hovering = false;
        GameManager.instance.ui.DisplayInteractionButton(false);
    }
}
