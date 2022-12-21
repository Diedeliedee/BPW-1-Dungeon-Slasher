using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonSlasher.Agents
{
    public partial class NPC : Agent
    {
        [Header("NPC Properties:")]
        [SerializeField] private NPCConcept.Type m_type     = 0;

        [Header("NPC States:")]
        [SerializeField] private ChasePlayer m_chaseAgent   = null;
        [SerializeField] private Attack m_attack            = null;

        private void Awake()
        {
            Initialize();
        }

        public override void Initialize()
        {
            base.Initialize();
            SetStates(m_chaseAgent.GetType(), m_chaseAgent, m_attack);
        }

        public override void Hit(int damage, Agent source)
        {
            base.Hit(damage, source);
            GameManager.instance.events.onEnemyHit.Invoke();
        }

        public override void OnDeath()
        {
            GameManager.instance.agents.TakeBack(m_type, this);
        }
    }
}
