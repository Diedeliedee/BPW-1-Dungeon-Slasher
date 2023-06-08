using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools.Utilities;

[System.Serializable]
public partial class Combat
{
    [Header("Properties:")]
    [SerializeField] private Health m_health = null;

    [Header("Weapons:")]
    [SerializeField] private Weapon m_weapon;
    [SerializeField] private LayerMask m_hitMask = new LayerMask();

    public Health health { get => m_health; }

    public void Setup(Entity agent)
    {
        m_weapon.Setup(agent, m_hitMask);
    }

    public void TickWeapon()
    {
        m_weapon.Tick();
    }

    public void ActivateWeapon()
    {
        m_weapon.Activate();
    }

    public void DeactivateWeapon()
    {
        m_weapon.Deactivate();
    }
}
