using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private RectTransform m_indicatorTransform;

    private float m_fullWidth = 0f;

    public void Setup()
    {
        m_fullWidth = m_indicatorTransform.sizeDelta.x;

        GameManager.instance.entities.player.onHealthChange += OnPlayerHit;
    }

    private void OnPlayerHit(int health, int maxHealth)
    {
        var size = m_indicatorTransform.sizeDelta;

        size.x = Mathf.Lerp(0f, m_fullWidth, (float)health / maxHealth);
        m_indicatorTransform.sizeDelta = size;
    }
}
