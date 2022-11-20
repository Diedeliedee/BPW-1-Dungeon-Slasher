using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonSlasher.Agents
{
    [System.Serializable]
    public partial class Combat
    {
        [SerializeField] private Weapon[] m_weapons = new Weapon[1];
        [SerializeField] private LayerMask m_hitMask = new LayerMask();

        private List<Agent> m_hitAgents = new List<Agent>();

        public void Tick(Collider ownCollider)
        {
            for (int i = 0; i < m_weapons.Length; i++) m_weapons[i].Tick(m_hitMask, ownCollider);
        }

        public void SetWeaponState(int index, bool active)
        {
            if (index > 0 || index >= m_weapons.Length)
            {
                Debug.LogError("Requested weapon index is out of range.");
                return;
            }
            m_weapons[index].SetEnabled(active);
        }

        public void RetractWeapons()
        {
            foreach (var weapon in m_weapons) weapon.SetEnabled(false);
        }
    }
}
