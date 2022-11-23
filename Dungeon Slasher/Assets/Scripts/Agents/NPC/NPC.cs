using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonSlasher.Agents
{
    public partial class NPC : Agent
    {
        [Header("NPC States:")]
        [SerializeField] private ChaseAgent m_chaseAgent = null;
        [SerializeField] private Attack m_attack = null;

        public override void Initialize()
        {
            base.Initialize();
            SetStates(m_chaseAgent.GetType(), m_chaseAgent, m_attack);
        }
    }
}
