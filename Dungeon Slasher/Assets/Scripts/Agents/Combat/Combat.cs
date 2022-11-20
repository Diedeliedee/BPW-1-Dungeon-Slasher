using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonSlasher.Agents
{
    public partial class Combat
    {
        private Weapon[] m_weapons = null;
        private List<Agent> m_hitAgents = null;

        public Combat(Weapon[] weapons)
        {
            m_weapons = weapons;
            m_hitAgents = new List<Agent>();
        }

        public void Tick(float deltaTime)
        {
            foreach (var weapon in m_weapons) weapon.Tick();
        }
    }
}
