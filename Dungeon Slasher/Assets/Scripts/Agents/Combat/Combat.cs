using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonSlasher.Agents
{
    public partial class Combat
    {
        private Hurtbox[] m_hurtboxes = null;
        private List<HurtInstance> m_hurtInstances = null;

        public Combat(Hurtbox[] hurtboxes)
        {
            m_hurtboxes = hurtboxes;
            m_hurtInstances = new List<HurtInstance>();
        }

        public void Tick(float deltaTime)
        {
            foreach (var hurtBox in m_hurtboxes)
            {
                if (!hurtBox.gameObject.activeSelf) continue;
                if (!hurtBox.DetectedAgents(out Agent[] agents)) continue;
                foreach (var instances in collection)
                {

                }

            }
        }
    }
}
