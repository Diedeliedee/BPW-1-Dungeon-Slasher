using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Health
{
    public event System.Action onDeath          = null; 

    [SerializeField] private int m_maxHealth    = 10;
    [SerializeField] private int m_health       = 10;

    #region Properties

    public int health { get => m_health; }
    public int maxHealth { get => m_maxHealth; }

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
        if (m_health == 0) onDeath?.Invoke();
        return health;
    }

    public int AddHealth(int health)
    {
        return SetHealth(m_health + health);
    }
}
