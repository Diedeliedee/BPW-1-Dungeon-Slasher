using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonSlasher.Agents
{
    public partial class NPC : Agent
    {
        [Header("NPC States:")]
        [SerializeField] private ChaseAgent m_chaseAgent = null;
        [SerializeField] private AttackState m_attack = null;

        private void OnEnable()
        {
            Initialize();
        }

        private void OnDisable()
        {
            
        }

        public override void Initialize()
        {
            base.Initialize();
            SetStates(m_chaseAgent.GetType(), m_chaseAgent, m_attack);
        }
    }
}
