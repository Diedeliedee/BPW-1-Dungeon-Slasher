using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Health
{
    [SerializeField] private int m_maxHealth    = 10;
    [SerializeField] private int m_health       = 10;

    private HealthEvent m_onDeath = null;

    #region Properties

    public int health { get => m_health; }
    public int maxHealth { get => m_maxHealth; }
    public HealthEvent onDeath { get => m_onDeath; }

    #endregion

    public int SetMaxHealth(int maxHealth, bool heal)
    {
        m_maxHealth = maxHealth;
        if (heal) m_health = maxHealth;
        return maxHealth;
    }

    public int SetHealth(int health)
    {
        m_health = Mathf.Clamp(health, 0, m_maxHealth);
        return health;
    }

    public int AddHealth(int health)
    {
        return SetHealth(m_health + health);
    }

    public delegate void HealthEvent();
}
